using RestSharp;

namespace Sdkgen.Client;

abstract public class TagAbstract
{
    protected readonly RestClient HttpClient;
    protected readonly Parser Parser;

    public TagAbstract(RestClient httpClient, Parser parser)
    {
        this.HttpClient = httpClient;
        this.Parser = parser;
    }
}