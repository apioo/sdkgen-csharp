/**
 * MultipartException automatically generated by SDKgen please do not edit this file manually
 * @see https://sdkgen.app
 */


using Sdkgen.Client.Exception;

namespace Sdkgen.Client.Tests.Generated;

public class MultipartException : KnownStatusCodeException
{
    public System.Collections.Generic.Dictionary<string, string> Payload;

    public MultipartException(System.Collections.Generic.Dictionary<string, string> payload)
    {
        this.Payload = payload;
    }
}
