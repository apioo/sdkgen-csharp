/**
 * MultipartException automatically generated by SDKgen please do not edit this file manually
 * @see https://sdkgen.app
 */


using Sdkgen.Client.Exception;

namespace Sdkgen.Client.Tests.Generated;

public class MultipartException : KnownStatusCodeException
{
    public Sdkgen.Client.Multipart Payload;

    public MultipartException(Sdkgen.Client.Multipart payload)
    {
        this.Payload = payload;
    }
}
