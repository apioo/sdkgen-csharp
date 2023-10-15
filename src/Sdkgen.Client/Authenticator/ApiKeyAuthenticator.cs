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

public class ApiKeyAuthenticator : IAuthenticator
{
    private readonly Credentials.ApiKey _credentials;

    public ApiKeyAuthenticator(Credentials.ApiKey credentials)
    {
        this._credentials = credentials;
    }

    public ValueTask Authenticate(RestClient client, RestRequest request)
    {
        request.AddHeader(this._credentials.Name, this._credentials.Token);

        return new ValueTask(Task.FromResult(request));
    }
}