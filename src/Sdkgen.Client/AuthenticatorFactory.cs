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
        if (credentials is HttpBasic httpBasic)
        {
            return new HttpBasicAuthenticator(httpBasic);
        }
        else if (credentials is HttpBearer httpBearer)
        {
            return new HttpBearerAuthenticator(httpBearer);
        }
        else if (credentials is ApiKey apiKey)
        {
            return new ApiKeyAuthenticator(apiKey);
        }
        else if (credentials is OAuth2 oauth2)
        {
            return new OAuth2Authenticator(oauth2);
        }
        else if (credentials is Anonymous anonymous)
        {
            return new AnonymousAuthenticator(anonymous);
        }
        else
        {
            throw new InvalidCredentialsException("Could not find authenticator for credentials");
        }
    }
}