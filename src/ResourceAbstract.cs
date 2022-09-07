
namespace Sdkgen.Client;

using System.Net.Http;

abstract public class ResourceAbstract
{
    private string baseUrl;
    private HttpClient httpClient;
    ResourceAbstract(string baseUrl, HttpClient httpClient)
    {
        this.baseUrl = baseUrl;
        this.httpClient = httpClient;
    }
}
