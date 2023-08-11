using RestSharp;

namespace Sdkgen.Client.Authenticator;

public class AnonymousAuthenticator : IAuthenticator
{
    private readonly Credentials.Anonymous _credentials;

    public AnonymousAuthenticator(Credentials.Anonymous credentials)
    {
        this._credentials = credentials;
    }

    public ValueTask Authenticate(RestClient client, RestRequest request)
    {
        return new ValueTask();
    }
}