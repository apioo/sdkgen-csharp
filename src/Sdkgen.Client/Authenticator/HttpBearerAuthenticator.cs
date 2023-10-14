using RestSharp;

namespace Sdkgen.Client.Authenticator;

public class HttpBearerAuthenticator : IAuthenticator
{
    private readonly Credentials.HttpBearer _credentials;

    public HttpBearerAuthenticator(Credentials.HttpBearer credentials)
    {
        this._credentials = credentials;
    }

    public ValueTask Authenticate(RestClient client, RestRequest request)
    {
        return new ValueTask(Task.FromResult(new HeaderParameter("Authorization", "Bearer " + this._credentials.Token)));
    }
}