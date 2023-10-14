using System.Collections.Specialized;
using System.Net;
using RestSharp;
using Sdkgen.Client.Credentials;
using Sdkgen.Client.Exception.Authenticator;

namespace Sdkgen.Client.Authenticator;

public class OAuth2Authenticator : IAuthenticator
{
    private const int EXPIRE_THRESHOLD = 60 * 10;

    private readonly Credentials.OAuth2 _credentials;
    private readonly ITokenStore _tokenStore;
    private readonly List<String>? _scopes;

    public OAuth2Authenticator(Credentials.OAuth2 credentials)
    {
        this._credentials = credentials;
        this._tokenStore = credentials.TokenStore;
        this._scopes = credentials.Scopes;
    }

    public ValueTask Authenticate(RestClient client, RestRequest request)
    {
        return new ValueTask(Task.FromResult(new HeaderParameter("Authorization", "Bearer " + this.GetAccessToken())));
    }

    public String BuildRedirectUrl(string redirectUrl, List<string>? scopes, string? state)
    {
        NameValueCollection builder = System.Web.HttpUtility.ParseQueryString(string.Empty);
        builder.Add("response_type", "code");
        builder.Add("client_id", this._credentials.ClientId);

        if (!String.IsNullOrEmpty(redirectUrl))
        {
            builder.Add("redirect_uri", redirectUrl);
        }

        if (scopes != null && scopes.Count > 0)
        {
            builder.Add("scope", String.Join(",", scopes));
        }
        else if (this._scopes != null && this._scopes.Count > 0)
        {
            builder.Add("scope", String.Join(",", this._scopes));
        }

        if (!String.IsNullOrEmpty(state))
        {
            builder.Add("state", state);
        }

        UriBuilder uri = new UriBuilder(redirectUrl);
        uri.Query = builder.ToString();

        return uri.ToString();
    }

    protected AccessToken FetchAccessTokenByCode(string code)
    {
        HttpBasic auth = new HttpBasic(this._credentials.ClientId, this._credentials.ClientSecret);

        RestRequest request = new RestRequest(this._credentials.TokenUrl, Method.Post);
        request.AddParameter("grant_type", "authorization_code", ParameterType.RequestBody);
        request.AddParameter("code", code, ParameterType.RequestBody);

        return this.Request(auth, request);
    }

    protected AccessToken FetchAccessTokenByClientCredentials()
    {
        HttpBasic auth = new HttpBasic(this._credentials.ClientId, this._credentials.ClientSecret);

        RestRequest request = new RestRequest(this._credentials.TokenUrl, Method.Post);
        request.AddParameter("grant_type", "client_credentials", ParameterType.RequestBody);

        if (this._scopes != null && this._scopes.Count() > 0)
        {
            request.AddParameter("scope", String.Join(",", this._scopes), ParameterType.RequestBody);
        }

        return this.Request(auth, request);
    }

    protected async Task<AccessToken> FetchAccessTokenByRefresh(string refreshToken)
    {
        HttpBearer auth = new HttpBearer(await this.GetAccessToken(false, 0));

        RestRequest request = new RestRequest(this._credentials.TokenUrl, Method.Post);
        request.AddParameter("grant_type", "refresh_token", ParameterType.RequestBody);
        request.AddParameter("refresh_token", refreshToken, ParameterType.RequestBody);

        return this.Request(auth, request);
    }

    protected async Task<string> GetAccessToken(bool automaticRefresh = true, int expireThreshold = EXPIRE_THRESHOLD)
    {
        long timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

        AccessToken? accessToken = this._tokenStore.Get();
        if ((accessToken == null || accessToken.ExpiresIn < timestamp))
        {
            accessToken = this.FetchAccessTokenByClientCredentials();
        }

        if (accessToken == null)
        {
            throw new FoundNoAccessTokenException("Found no access token, please obtain an access token before making a request");
        }

        if (accessToken.ExpiresIn > (timestamp + expireThreshold))
        {
            return accessToken.Token;
        }

        if (automaticRefresh && !String.IsNullOrEmpty(accessToken.RefreshToken))
        {
            accessToken = await this.FetchAccessTokenByRefresh(accessToken.RefreshToken);
        }

        return accessToken.Token;
    }

    private AccessToken ParseTokenResponse(RestResponse<AccessToken> response)
    {
        try
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidAccessTokenException("Could not obtain access token");
            }

            AccessToken? accessToken = response.Data;
            if (accessToken == null)
            {
                throw new InvalidAccessTokenException("Could not obtain access token");
            }

            this._tokenStore.Persist(accessToken);

            return accessToken;
        }
        catch (IOException e)
        {
            throw new AccessTokenRequestException("Could not parse access token response " + e.Message, e);
        }
    }

    private RestClient NewHttpClient(ICredentials credentials)
    {
        return (new HttpClientFactory(AuthenticatorFactory.Factory(credentials))).Factory();
    }

    private AccessToken Request(ICredentials credentials, RestRequest request)
    {
        RestClient httpClient = this.NewHttpClient(credentials);
        RestResponse<AccessToken> response = httpClient.Execute<AccessToken>(request);

        AccessToken token = this.ParseTokenResponse(response);

        return token;
    }
}
