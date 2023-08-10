
namespace Sdkgen.Client;

using Sdkgen.Client.Credentials;
using Sdkgen.Client.Exception;
using System.Collections.Specialized;
using System.Web;
using System.Text;
using System.Text.Json;
using RestSharp;
using RestSharp.Authenticators;

abstract public class ClientAbstract
{
    public const string USER_AGENT = "SDKgen Client v1.0";

    private string baseUrl;
    private CredentialsInterface credentials;
    private TokenStoreInterface tokenStore;
    private List<String> scopes;

    public ClientAbstract(string baseUrl, CredentialsInterface credentials, TokenStoreInterface tokenStore, List<String> scopes)
    {
        this.baseUrl = baseUrl;
        this.credentials = credentials;
        this.tokenStore = tokenStore;
        this.scopes = scopes;
    }

}
