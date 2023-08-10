
using RestSharp;
using RestSharp.Authenticators;

namespace Sdkgen.Client;

using System.Net.Http;

public class HttpClientFactory
{
    private AuthenticatorInterface authenticator;

    HttpClientFactory(AuthenticatorInterface authenticator)
    {
        this.authenticator = authenticator;
    }

    public RestClient factory()
    {
        RestClient client = new RestClient();
        client.AddDefaultHeader("User-Agent", ClientAbstract.USER_AGENT);
        client.AddDefaultHeader("Accept", "application/json");
        client.Authenticator = this.authenticator;
        return client;
    }
}
