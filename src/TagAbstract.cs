namespace Sdkgen.Client;

using System.Net.Http;

abstract public class TagAbstract
{
    protected readonly HttpClient HttpClient;
    protected readonly Parser Parser;

    public TagAbstract(HttpClient httpClient, Parser parser)
    {
        this.HttpClient = httpClient;
        this.Parser = parser;
    }
}