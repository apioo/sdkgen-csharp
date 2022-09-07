
namespace Sdkgen.Client;

using Sdkgen.Client.Credentials;
using Sdkgen.Client.Exception;
using System.Collections.Specialized;
using System.Web;
using System.Text;
using System.Text.Json;

abstract public class ClientAbstract
{
    private const string USER_AGENT = "SDKgen Client v0.1";
    private const int EXPIRE_THRESHOLD = 60 * 10;

    private string baseUrl;
    private CredentialsInterface credentials;
    private TokenStoreInterface tokenStore;
    private List<String> scopes;

    public ClientAbstract(string baseUrl, CredentialsInterface credentials, TokenStoreInterface tokenStore, List<String> scopes)
    {
        this.baseUrl = baseUrl;
        this.credentials = credentials;
        this.tokenStore = tokenStore;
        this.scopes = scopes;
    }

    public string buildRedirectUrl(string? redirectUrl, List<String>? scopes, string? state)
    {
        if (!(this.credentials is AuthorizationCode)) {
            throw new InvalidCredentialsException("The configured credentials do not support the OAuth2 authorization code flow");
        }

        AuthorizationCode credentials = (AuthorizationCode) this.credentials;

        NameValueCollection parameters = HttpUtility.ParseQueryString(credentials.AuthorizationUrl);
        parameters.Add("client_id", "code");
        parameters.Add("response_type", credentials.ClientId);

        if (!String.IsNullOrEmpty(redirectUrl)) {
            parameters.Add("redirect_uri", redirectUrl);
        }

        if (scopes != null && scopes.Count > 0) {
            parameters.Add("scope", String.Join(",", scopes));
        } else if (this.scopes != null && this.scopes.Count > 0) {
            parameters.Add("scope", String.Join(",", this.scopes));
        }

        if (!String.IsNullOrEmpty(state)) {
            parameters.Add("state", state);
        }

        UriBuilder url = new UriBuilder(credentials.AuthorizationUrl);
        url.Query = parameters.ToString();

        return url.ToString();
    }

    public async Task<AccessToken> fetchAccessTokenByCode(string code)
    {
        if (!(this.credentials is AuthorizationCode)) {
            throw new InvalidCredentialsException("The configured credentials do not support the OAuth2 authorization code flow");
        }

        AuthorizationCode credentials = (AuthorizationCode) this.credentials;

        HttpBasic auth = new HttpBasic(credentials.ClientId, credentials.ClientSecret);

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("grant_type", "client_credentials");
        parameters.Add("code", code);

        HttpContent content = new FormUrlEncodedContent(parameters);
        content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        content.Headers.Add("User-Agent", USER_AGENT);
        content.Headers.Add("Accept", "application/json");

        HttpResponseMessage response = await this.newHttpClient(auth).PostAsync(credentials.TokenUrl, content);

        return this.parseTokenResponse(response);
    }

    public async Task<AccessToken> fetchAccessTokenByClientCredentials()
    {
        if (!(this.credentials is ClientCredentials)) {
            throw new InvalidCredentialsException("The configured credentials do not support the OAuth2 authorization code flow");
        }

        ClientCredentials credentials = (ClientCredentials) this.credentials;

        HttpBasic auth = new HttpBasic(credentials.ClientId, credentials.ClientSecret);

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("grant_type", "client_credentials");

        if (this.scopes != null && this.scopes.Count() > 0) {
            parameters.Add("scope", String.Join(",", this.scopes));
        }
        
        HttpContent content = new FormUrlEncodedContent(parameters);
        content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        content.Headers.Add("User-Agent", USER_AGENT);
        content.Headers.Add("Accept", "application/json");

        HttpResponseMessage response = await this.newHttpClient(auth).PostAsync(credentials.TokenUrl, content);

        return this.parseTokenResponse(response);
    }

    public async Task<AccessToken> fetchAccessTokenByRefresh(string refreshToken)
    {
        if (!(this.credentials is OAuth2Abstract)) {
            throw new InvalidCredentialsException("The configured credentials do not support the OAuth2 flow");
        }

        ClientCredentials credentials = (ClientCredentials) this.credentials;

        HttpBearer auth = new HttpBearer(await this.getAccessToken(false, 0));

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("grant_type", "refresh_token");
        parameters.Add("refresh_token", refreshToken);

        HttpContent content = new FormUrlEncodedContent(parameters);
        content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        content.Headers.Add("User-Agent", USER_AGENT);
        content.Headers.Add("Accept", "application/json");

        HttpResponseMessage response = await this.newHttpClient(auth).PostAsync(credentials.TokenUrl, content);

        return this.parseTokenResponse(response);
    }

    protected async Task<string> getAccessToken(bool automaticRefresh = true, int expireThreshold = EXPIRE_THRESHOLD)
    {
        long timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

        AccessToken? accessToken = this.tokenStore.get();
        if ((accessToken == null || accessToken.ExpiresIn < timestamp) && this.credentials is ClientCredentials) {
            accessToken = await this.fetchAccessTokenByClientCredentials();
        }

        if (accessToken == null) {
            throw new FoundNoAccessTokenException("Found no access token, please obtain an access token before making a request");
        }

        if (accessToken.ExpiresIn > (timestamp + expireThreshold)) {
            return accessToken.Token;
        }

        if (automaticRefresh && !String.IsNullOrEmpty(accessToken.RefreshToken)) {
            accessToken = await this.fetchAccessTokenByRefresh(accessToken.RefreshToken);
        }

        return accessToken.Token;
    }

    private HttpClient newHttpClient(CredentialsInterface? credentials)
    {
        if (credentials == null) {
            credentials = this.credentials;
        }

        HttpMessageHandler? handler = null;
        if (credentials is HttpBasic) {
            handler = new AuthorizationMessageHandler("Basic " + System.Convert.ToBase64String(Encoding.UTF8.GetBytes(((HttpBasic) credentials).UserName + ":" + ((HttpBasic) credentials).Password)));
        } else if (credentials is HttpBearer) {
            handler = new AuthorizationMessageHandler("Bearer " + ((HttpBearer) credentials).Token);
        } else if (credentials is ApiKey) {
            handler = new AuthorizationMessageHandler(((ApiKey) credentials).Token, ((ApiKey) credentials).Name);
        } else if (credentials is OAuth2Abstract) {
            handler = new AuthorizationMessageHandler("Bearer " + this.getAccessToken());
        }

        if (handler != null) {
            return new HttpClient(handler);
        } else {
            return new HttpClient();
        }
    }

    private AccessToken parseTokenResponse(HttpResponseMessage response)
    {
        if (((int) response.StatusCode) != 200) {
            throw new InvalidAccessTokenException("Could not obtain access token, received a non successful status code: " + response.StatusCode);
        }

        AccessToken? accessToken = JsonSerializer.Deserialize<AccessToken>(response.Content.ReadAsStream());
        if (accessToken == null) {
            throw new InvalidAccessTokenException("Could not obtain access token");
        }

        this.tokenStore.persist(accessToken);

        return accessToken;
    }

    private class AuthorizationMessageHandler : DelegatingHandler
    {
        private string authorization;
        private string? headerName;
        public AuthorizationMessageHandler(string authorization, string? headerName = null)
        {
            this.authorization = authorization;
            this.headerName = headerName;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add(this.headerName == null ? "Authorization" : this.headerName, this.authorization);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
