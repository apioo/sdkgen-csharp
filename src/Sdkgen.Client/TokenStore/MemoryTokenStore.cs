/*
 * SDKgen is a powerful code generator to automatically build client SDKs for your REST API.
 * For the current version and information visit <https://sdkgen.app>
 *
 * Copyright (c) Christoph Kappestein <christoph.kappestein@gmail.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

namespace Sdkgen.Client.TokenStore;

public class MemoryTokenStore : ITokenStore
{
    private AccessToken? _token;

    public AccessToken? Get()
    {
        return this._token;
    }

    public void Persist(AccessToken token)
    {
        this._token = token;
    }

    public void Remove()
    {
        this._token = null;
    }
}