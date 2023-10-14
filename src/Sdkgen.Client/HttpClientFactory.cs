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
        RestClient client = new RestClient();
        client.AddDefaultHeader("User-Agent", ClientAbstract.USER_AGENT);
        client.AddDefaultHeader("Accept", "application/json");
        client.Authenticator = this._authenticator;
        return client;
    }
}