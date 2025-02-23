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
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sdkgen.Client.Tests;

public class AccessTokenTest
{
    [Test]
    public async Task TestParse()
    {
        var payload = "{\"access_token\": \"my_token\", \"token_type\": \"bearer\"}";
        AccessToken? token = JsonSerializer.Deserialize<AccessToken>(payload);

        Assert.That(token?.Token, Is.EqualTo("my_token"));
        Assert.That(token?.TokenType, Is.EqualTo("bearer"));
    }
}
