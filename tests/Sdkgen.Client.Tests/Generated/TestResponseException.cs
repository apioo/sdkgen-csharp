/**
 * TestResponseException automatically generated by SDKgen please do not edit this file manually
 * @see https://sdkgen.app
 */


using Sdkgen.Client.Exception;

namespace Sdkgen.Client.Tests.Generated;

public class TestResponseException : KnownStatusCodeException
{
    public TestResponse Payload;

    public TestResponseException(TestResponse payload)
    {
        this.Payload = payload;
    }
}
