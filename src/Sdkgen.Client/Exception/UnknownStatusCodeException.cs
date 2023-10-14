/*
 * SDKgen is a powerful code generator to automatically build client SDKs for your REST API.
 * For the current version and information visit <https://sdkgen.app>
 *
 * Copyright (c) Christoph Kappestein <christoph.kappestein@gmail.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

namespace Sdkgen.Client.Exception;

public class UnknownStatusCodeException : ClientException
{
    public UnknownStatusCodeException()
    {
    }

    public UnknownStatusCodeException(string message) : base(message)
    {
    }

    public UnknownStatusCodeException(string message, System.Exception inner) : base(message, inner)
    {
    }
}