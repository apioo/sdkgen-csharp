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
using RestSharp.Authenticators;

namespace Sdkgen.Client.Authenticator;

public class HttpBearerAuthenticator : AuthenticatorBase
{
    private readonly Credentials.HttpBearer _credentials;

    public HttpBearerAuthenticator(Credentials.HttpBearer credentials) : base("")
    {
        this._credentials = credentials;
    }

    protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
    {
        return new HeaderParameter(KnownHeaders.Authorization, "Bearer " + this._credentials.Token);
    }
}