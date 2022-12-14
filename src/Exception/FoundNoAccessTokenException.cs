
namespace Sdkgen.Client.Exception;

public class FoundNoAccessTokenException : System.Exception
{
    public FoundNoAccessTokenException() { }
    public FoundNoAccessTokenException(string message) : base(message) { }
    public FoundNoAccessTokenException(string message, System.Exception inner) : base(message, inner) { }
}
