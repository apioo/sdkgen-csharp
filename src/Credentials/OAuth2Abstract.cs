
namespace Sdkgen.Client.Credentials;

abstract public class OAuth2Abstract : CredentialsInterface 
{
    public OAuth2Abstract(string clientId, string clientSecret, string tokenUrl, string authorizationUrl) {
        ClientId = clientId;
        ClientSecret = clientSecret;
        TokenUrl = tokenUrl;
        AuthorizationUrl = authorizationUrl;
    }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string TokenUrl { get; set; }
    public string AuthorizationUrl { get; set; }
}
