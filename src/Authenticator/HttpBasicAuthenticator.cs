using RestSharp;

namespace Sdkgen.Client.Authenticator;

public class HttpBasicAuthenticator : AuthenticatorInterface 
{
    private Credentials.HttpBasic credentials;

    public HttpBasicAuthenticator(Credentials.HttpBasic credentials) {
        this.credentials = credentials;
    }
    
    public ValueTask Authenticate(RestClient client, RestRequest request)
    {
    }
}