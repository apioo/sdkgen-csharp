namespace Sdkgen.Client.Credentials;

abstract public class OAuth2 : ICredentials
{
    public OAuth2(string clientId, string clientSecret, string tokenUrl, string authorizationUrl,
        ITokenStore tokenStore, List<string> scopes)
    {
        ClientId = clientId;
        ClientSecret = clientSecret;
        TokenUrl = tokenUrl;
        AuthorizationUrl = authorizationUrl;
        TokenStore = tokenStore;
        Scopes = scopes;
    }

    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string TokenUrl { get; set; }
    public string AuthorizationUrl { get; set; }
    public ITokenStore TokenStore { get; set; }
    public List<string> Scopes { get; set; }
}