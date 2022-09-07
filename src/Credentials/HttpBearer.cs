
namespace Sdkgen.Client.Credentials;

public class HttpBearer : CredentialsInterface 
{
    public HttpBearer(string token) {
        Token = token;
    }
    public string Token { get; set; }
}
