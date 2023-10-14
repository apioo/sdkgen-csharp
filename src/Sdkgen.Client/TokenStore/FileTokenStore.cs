/*
 * SDKgen is a powerful code generator to automatically build client SDKs for your REST API.
 * For the current version and information visit <https://sdkgen.app>
 *
 * Copyright (c) Christoph Kappestein <christoph.kappestein@gmail.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

using System.Text.Json;

namespace Sdkgen.Client.TokenStore;

public class FileTokenStore : ITokenStore
{
    private readonly string? _cacheDir;
    private readonly string? _fileName;

    public FileTokenStore(string? cacheDir, string? fileName = "sdkgen_access_token")
    {
        this._cacheDir = cacheDir;
        this._fileName = fileName;
    }

    public AccessToken? Get()
    {
        var json = File.ReadAllText(this.GetFileName());
        return JsonSerializer.Deserialize<AccessToken>(json);
    }

    public void Persist(AccessToken token)
    {
        File.WriteAllText(this.GetFileName(), JsonSerializer.Serialize(token));
    }

    public void Remove()
    {
        File.Delete(this.GetFileName());
    }

    private string GetFileName()
    {
        return this._cacheDir + "/" + this._fileName + ".json";
    }
}