namespace Sdkgen.Client;

public class Parser
{
    private readonly string _baseUrl;

    public Parser(string baseUrl)
    {
        this._baseUrl = baseUrl;
    }

    public string Url(string path, Dictionary<string, Object> parameters)
    {
        return this._baseUrl + "/" + this.SubstituteParameters(path, parameters);
    }

    public Dictionary<string, string> Query(Dictionary<string, Object> parameters)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        foreach (KeyValuePair<string, Object> entry in parameters)
        {
            if (entry.Value == null)
            {
                continue;
            }

            result.Add(entry.Key, this.ToString(entry.Value));
        }

        return result;
    }

    public Object Parse(string data, string className)
    {
        return null;
    }

    private string SubstituteParameters(string path, Dictionary<string, Object> parameters)
    {
        return "";
    }

    private string ToString(Object value)
    {
        return "";
    }
}