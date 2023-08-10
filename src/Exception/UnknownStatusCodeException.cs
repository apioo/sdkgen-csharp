
namespace Sdkgen.Client.Exception;

public class UnknownStatusCodeException : ClientException
{
    public UnknownStatusCodeException() { }
    public UnknownStatusCodeException(string message) : base(message) { }
    public UnknownStatusCodeException(string message, System.Exception inner) : base(message, inner) { }
}
