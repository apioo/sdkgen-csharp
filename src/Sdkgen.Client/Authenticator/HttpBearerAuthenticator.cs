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