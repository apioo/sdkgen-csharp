using RestSharp;

namespace Sdkgen.Client;

abstract public class ClientAbstract
{
    public const string USER_AGENT = "SDKgen Client v1.0";

    protected readonly IAuthenticator Authenticator;
    protected readonly RestClient HttpClient;
    protected readonly Parser Parser;

    public ClientAbstract(string baseUrl, ICredentials credentials)
    {
        this.Authenticator = AuthenticatorFactory.Factory(credentials);
        this.HttpClient = (new HttpClientFactory(this.Authenticator)).Factory();
        this.Parser = new Parser(baseUrl);
    }

    public IAuthenticator GetAuthenticator()
    {
        return this.Authenticator;
    }
}