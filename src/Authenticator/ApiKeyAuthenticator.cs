using RestSharp;

namespace Sdkgen.Client.Authenticator;

public class ApiKeyAuthenticator : AuthenticatorInterface 
{
    private Credentials.ApiKey credentials;

    public ApiKeyAuthenticator(Credentials.ApiKey credentials) {
        this.credentials = credentials;
    }
    
    public ValueTask Authenticate(RestClient client, RestRequest request)
    {
        request.AddHeader(this.credentials.Name, this.credentials.Token);
    }
}