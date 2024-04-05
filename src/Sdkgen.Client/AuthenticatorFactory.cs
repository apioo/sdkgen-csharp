/*
 * SDKgen is a powerful code generator to automatically build client SDKs for your REST API.
 * For the current version and information visit <https://sdkgen.app>
 *
 * Copyright (c) Christoph Kappestein <christoph.kappestein@gmail.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

using Sdkgen.Client.Authenticator;
using Sdkgen.Client.Credentials;
using Sdkgen.Client.Exception.Authenticator;

namespace Sdkgen.Client;

public class AuthenticatorFactory
{
    public static IAuthenticator Factory(ICredentials credentials)
    {
        if (credentials is HttpBasic)
        {
            return new HttpBasicAuthenticator((HttpBasic)credentials);
        }
        else if (credentials is HttpBearer)
        {
            return new HttpBearerAuthenticator((HttpBearer)credentials);
        }
        else if (credentials is ApiKey)
        {
            return new ApiKeyAuthenticator((ApiKey)credentials);
        }
        else if (credentials is OAuth2)
        {
            return new OAuth2Authenticator((OAuth2)credentials);
        }
        else if (credentials is Anonymous)
        {
            return new AnonymousAuthenticator((Anonymous)credentials);
        }
        else
        {
            throw new InvalidCredentialsException("Could not find authenticator for credentials");
        }
    }
}