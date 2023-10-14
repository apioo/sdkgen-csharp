using RestSharp;

namespace Sdkgen.Client;

public class HttpClientFactory
{
    private readonly IAuthenticator _authenticator;

    public HttpClientFactory(IAuthenticator authenticator)
    {
        this._authenticator = authenticator;
    }

    public RestClient Factory()
    {
        RestClient client = new RestClient();
        client.AddDefaultHeader("User-Agent", ClientAbstract.USER_AGENT);
        client.AddDefaultHeader("Accept", "application/json");
        client.Authenticator = this._authenticator;
        return client;
    }
}