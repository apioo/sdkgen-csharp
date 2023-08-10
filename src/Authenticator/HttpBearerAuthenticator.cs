using RestSharp;

namespace Sdkgen.Client.Authenticator;

public class HttpBearerAuthenticator : AuthenticatorInterface 
{
    private Credentials.HttpBearer credentials;

    public HttpBearerAuthenticator(Credentials.HttpBearer credentials) {
        this.credentials = credentials;
    }
    
    public ValueTask Authenticate(RestClient client, RestRequest request)
    {
    }
}