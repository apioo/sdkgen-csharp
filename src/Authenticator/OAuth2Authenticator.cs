using RestSharp;

namespace Sdkgen.Client.Authenticator;

public class OAuth2Authenticator : AuthenticatorInterface 
{
    private const int EXPIRE_THRESHOLD = 60 * 10;

    private Credentials.OAuth2 credentials;

    public OAuth2Authenticator(Credentials.OAuth2 credentials) {
        this.credentials = credentials;
    }
    
    public ValueTask Authenticate(RestClient client, RestRequest request)
    {
    }
    
    /*
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

        RestRequest request = new RestRequest(credentials.TokenUrl);
        request.AddHeader("User-Agent", USER_AGENT);
        request.AddParameter("grant_type", "authorization_code");
        request.AddParameter("code", code);

        RestClient client = await this.newHttpClient(auth);
        AccessToken? accessToken = await client.PostAsync<AccessToken>(request);

        return this.parseTokenResponse(accessToken);
    }

    public async Task<AccessToken> fetchAccessTokenByClientCredentials()
    {
        if (!(this.credentials is ClientCredentials)) {
            throw new InvalidCredentialsException("The configured credentials do not support the OAuth2 authorization code flow");
        }

        ClientCredentials credentials = (ClientCredentials) this.credentials;

        HttpBasic auth = new HttpBasic(credentials.ClientId, credentials.ClientSecret);

        RestRequest request = new RestRequest(credentials.TokenUrl);
        request.AddHeader("User-Agent", USER_AGENT);
        request.AddParameter("grant_type", "client_credentials");

        if (this.scopes != null && this.scopes.Count() > 0) {
            request.AddParameter("scope", String.Join(",", this.scopes));
        }

        RestClient client = await this.newHttpClient(auth);
        AccessToken? accessToken = await client.PostAsync<AccessToken>(request);

        return this.parseTokenResponse(accessToken);
    }

    public async Task<AccessToken> fetchAccessTokenByRefresh(string refreshToken)
    {
        if (!(this.credentials is OAuth2)) {
            throw new InvalidCredentialsException("The configured credentials do not support the OAuth2 flow");
        }

        ClientCredentials credentials = (ClientCredentials) this.credentials;

        HttpBearer auth = new HttpBearer(await this.getAccessToken(false, 0));

        RestRequest request = new RestRequest(credentials.TokenUrl);
        request.AddHeader("User-Agent", USER_AGENT);
        request.AddParameter("grant_type", "refresh_token");
        request.AddParameter("refresh_token", refreshToken);

        RestClient client = await this.newHttpClient(auth);
        AccessToken? accessToken = await client.PostAsync<AccessToken>(request);

        return this.parseTokenResponse(accessToken);
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
    */

}