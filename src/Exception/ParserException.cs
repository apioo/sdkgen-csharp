namespace Sdkgen.Client.Exception;

public class ParserException : ClientException
{
    public ParserException()
    {
    }

    public ParserException(string message) : base(message)
    {
    }

    public ParserException(string message, System.Exception inner) : base(message, inner)
    {
    }
}