/*
 * SDKgen is a powerful code generator to automatically build client SDKs for your REST API.
 * For the current version and information visit <https://sdkgen.app>
 *
 * Copyright (c) Christoph Kappestein <christoph.kappestein@gmail.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

namespace Sdkgen.Client.Credentials;

abstract public class OAuth2 : ICredentials
{
    public OAuth2(string clientId, string clientSecret, string tokenUrl, string authorizationUrl,
        ITokenStore tokenStore, List<string>? scopes)
    {
        ClientId = clientId;
        ClientSecret = clientSecret;
        TokenUrl = tokenUrl;
        AuthorizationUrl = authorizationUrl;
        TokenStore = tokenStore;
        Scopes = scopes;
    }

    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string TokenUrl { get; set; }
    public string AuthorizationUrl { get; set; }
    public ITokenStore TokenStore { get; set; }
    public List<string>? Scopes { get; set; }
}