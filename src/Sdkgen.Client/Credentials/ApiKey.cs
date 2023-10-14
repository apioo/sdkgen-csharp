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

public class ApiKey : ICredentials
{
    public ApiKey(string token, string name, string in_)
    {
        Token = token;
        Name = name;
        In = in_;
    }

    public string Token { get; set; }
    public string Name { get; set; }
    public string In { get; set; }
}