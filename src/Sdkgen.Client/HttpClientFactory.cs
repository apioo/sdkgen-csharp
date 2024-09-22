/*
 * SDKgen is a powerful code generator to automatically build client SDKs for your REST API.
 * For the current version and information visit <https://sdkgen.app>
 *
 * Copyright (c) Christoph Kappestein <christoph.kappestein@gmail.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

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
        var options = new RestClientOptions() {
            ThrowOnAnyError = false,
            Authenticator = this._authenticator,
            UserAgent = ClientAbstract.USER_AGENT
        };

        RestClient client = new RestClient(options);
        client.AddDefaultHeader("Accept", "application/json");
        return client;
    }
}