namespace Sdkgen.Client.Exception;

public class KnownStatusCodeException : ClientException
{
    public KnownStatusCodeException()
    {
    }

    public KnownStatusCodeException(string message) : base(message)
    {
    }

    public KnownStatusCodeException(string message, System.Exception inner) : base(message, inner)
    {
    }
}