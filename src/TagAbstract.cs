
namespace Sdkgen.Client;

using System.Net.Http;

abstract public class TagAbstract
{
    private HttpClient httpClient;
    private Parser parser;
    
    TagAbstract(HttpClient httpClient, Parser parser)
    {
        this.httpClient = httpClient;
        this.parser = parser;
    }
}
