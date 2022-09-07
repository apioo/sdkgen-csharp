
namespace Sdkgen.Client.Credentials;

public class ApiKey : CredentialsInterface 
{
    public ApiKey(string token, string name, string in_) {
        Token = token;
        Name = name;
        In = in_;
    }
    public string Token { get; set; }
    public string Name { get; set; }
    public string In { get; set; }
}
