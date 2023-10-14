namespace Sdkgen.Client.Exception;

public class ClientException : System.Exception
{
    public ClientException()
    {
    }

    public ClientException(string message) : base(message)
    {
    }

    public ClientException(string message, System.Exception inner) : base(message, inner)
    {
    }
}