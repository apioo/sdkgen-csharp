
using Sdkgen.Client.Authenticator;
using Sdkgen.Client.Credentials;
using Sdkgen.Client.Exception.Authenticator;

namespace Sdkgen.Client;

using System.Net.Http;

public class AuthenticatorFactory
{
    public static AuthenticatorInterface factory(CredentialsInterface credentials)
    {
        if (credentials is HttpBasic) {
            return new HttpBasicAuthenticator((HttpBasic) credentials);
        } else if (credentials is HttpBearer) {
            return new HttpBearerAuthenticator((HttpBearer) credentials);
        } else if (credentials is ApiKey) {
            return new ApiKeyAuthenticator((ApiKey) credentials);
        } else if (credentials is OAuth2) {
            return new OAuth2Authenticator((OAuth2) credentials);
        } else if (credentials is Anonymous) {
            return new AnonymousAuthenticator((Anonymous) credentials);
        }
        else
        {
            throw new InvalidCredentialsException("Could not find authenticator for credentials");
        }

    }
}


