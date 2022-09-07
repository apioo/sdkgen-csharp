
namespace Sdkgen.Client.Credentials;

public class AuthorizationCode : OAuth2Abstract
{
    public AuthorizationCode(string clientId, string clientSecret, string tokenUrl, string authorizationUrl) : base(clientId, clientSecret, tokenUrl, authorizationUrl) {
    }
}
