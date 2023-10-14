namespace Sdkgen.Client.Credentials;

public class HttpBearer : ICredentials
{
    public HttpBearer(string token)
    {
        Token = token;
    }

    public string Token { get; set; }
}