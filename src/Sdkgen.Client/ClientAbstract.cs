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

abstract public class ClientAbstract
{
    public const string USER_AGENT = "SDKgen";

    protected readonly IAuthenticator Authenticator;
    protected readonly RestClient HttpClient;
    protected readonly Parser Parser;

    public ClientAbstract(string baseUrl, ICredentials credentials, string? version = null)
    {
        this.Authenticator = AuthenticatorFactory.Factory(credentials);
        this.HttpClient = new HttpClientFactory(this.Authenticator, version).Factory();
        this.Parser = new Parser(baseUrl);
    }

    public ClientAbstract(string baseUrl, ICredentials credentials): this(baseUrl, credentials, null)
    {
    }

    public IAuthenticator GetAuthenticator()
    {
        return this.Authenticator;
    }
}
