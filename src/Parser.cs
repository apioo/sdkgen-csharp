
namespace Sdkgen.Client;

using System.Net.Http;

public class Parser
{
    private string baseUrl;

    Parser(string baseUrl)
    {
        this.baseUrl = baseUrl;
    }

    public string url(string path, Dictionary<string, Object> parameters)
    {
        return this.baseUrl + "/" + this.substituteParameters(path, parameters);
    }

    public Dictionary<string, string> query(Dictionary<string, Object> parameters)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        foreach (KeyValuePair<string, Object> entry in parameters) {
            if (entry.Value == null) {
                continue;
            }

            result.Add(entry.Key, this.toString(entry.Value));
        }

        return result;
    }

    public Object parse(string data, string className)
    {
        return null;
    }

    private string substituteParameters(string path, Dictionary<string, Object> parameters)
    {
        return "";
    }
    
    private string toString(Object value)
    {
        return "";
    }
}
