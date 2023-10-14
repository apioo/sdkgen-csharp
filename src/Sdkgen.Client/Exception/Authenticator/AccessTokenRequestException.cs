namespace Sdkgen.Client.Exception.Authenticator;

public class AccessTokenRequestException : System.Exception
{
    public AccessTokenRequestException()
    {
    }

    public AccessTokenRequestException(string message) : base(message)
    {
    }

    public AccessTokenRequestException(string message, System.Exception inner) : base(message, inner)
    {
    }
}