
namespace Sdkgen.Client.Exception;

public class InvalidCredentialsException : System.Exception
{
    public InvalidCredentialsException() { }
    public InvalidCredentialsException(string message) : base(message) { }
    public InvalidCredentialsException(string message, System.Exception inner) : base(message, inner) { }
}
