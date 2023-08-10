using RestSharp;

namespace Sdkgen.Client.Authenticator;

public class AnonymousAuthenticator : AuthenticatorInterface
{
    private Credentials.Anonymous credentials;

    public AnonymousAuthenticator(Credentials.Anonymous credentials) {
        this.credentials = credentials;
    }

    public ValueTask Authenticate(RestClient client, RestRequest request)
    {
    }
}