/**
 * BinaryException automatically generated by SDKgen please do not edit this file manually
 * @see https://sdkgen.app
 */


using Sdkgen.Client.Exception;

namespace Sdkgen.Client.Tests.Generated;

public class BinaryException : KnownStatusCodeException
{
    public byte[] Payload;

    public BinaryException(byte[] payload)
    {
        this.Payload = payload;
    }
}
