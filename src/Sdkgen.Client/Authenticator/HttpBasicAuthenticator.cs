using System.Text;
using RestSharp;

namespace Sdkgen.Client.Authenticator;

public class HttpBasicAuthenticator : IAuthenticator
{
    private readonly Credentials.HttpBasic _credentials;

    public HttpBasicAuthenticator(Credentials.HttpBasic credentials)
    {
        this._credentials = credentials;
    }

    public ValueTask Authenticate(RestClient client, RestRequest request)
    {
        var basic = Encoding.UTF8.GetBytes(this._credentials.UserName + ":" + this._credentials.Password);
        return new ValueTask(
            Task.FromResult(new HeaderParameter("Authorization", "Basic " + Convert.ToBase64String(basic))));
    }
}