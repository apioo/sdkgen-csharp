/**
 * XmlException automatically generated by SDKgen please do not edit this file manually
 * @see https://sdkgen.app
 */


using Sdkgen.Client.Exception;

namespace Sdkgen.Client.Tests.Generated;

public class XmlException : KnownStatusCodeException
{
    public string Payload;

    public XmlException(string payload)
    {
        this.Payload = payload;
    }
}
