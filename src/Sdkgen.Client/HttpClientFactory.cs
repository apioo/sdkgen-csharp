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
    private readonly string? _version;

    public HttpClientFactory(IAuthenticator authenticator, string? version = null)
    {
        this._authenticator = authenticator;
        this._version = version;
    }

    public RestClient Factory()
    {
        string userAgent;
        if (!String.IsNullOrEmpty(this._version)) {
            userAgent = ClientAbstract.USER_AGENT + "/" + this._version;
        } else {
            userAgent = ClientAbstract.USER_AGENT;
        }

        var options = new RestClientOptions() {
            ThrowOnAnyError = false,
            Authenticator = this._authenticator,
            UserAgent = userAgent
        };

        RestClient client = new RestClient(options);
        client.AddDefaultHeader("Accept", "application/json");
        return client;
    }
}