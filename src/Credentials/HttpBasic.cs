namespace Sdkgen.Client.Credentials;

public class HttpBasic : ICredentials
{
    public HttpBasic(string username, string password)
    {
        UserName = username;
        Password = password;
    }

    public string UserName { get; set; }
    public string Password { get; set; }
}