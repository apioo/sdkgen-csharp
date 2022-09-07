
namespace Sdkgen.Client.Credentials;

public class ClientCredentials : OAuth2Abstract 
{
    public ClientCredentials(string clientId, string clientSecret, string tokenUrl, string authorizationUrl) : base(clientId, clientSecret, tokenUrl, authorizationUrl) {
    }
}
