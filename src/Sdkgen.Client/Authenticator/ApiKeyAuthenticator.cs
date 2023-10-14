using RestSharp;

namespace Sdkgen.Client.Authenticator;

public class ApiKeyAuthenticator : IAuthenticator
{
    private readonly Credentials.ApiKey _credentials;

    public ApiKeyAuthenticator(Credentials.ApiKey credentials)
    {
        this._credentials = credentials;
    }

    public ValueTask Authenticate(RestClient client, RestRequest request)
    {
        return new ValueTask(Task.FromResult(new HeaderParameter(this._credentials.Name, this._credentials.Token)));
    }
}