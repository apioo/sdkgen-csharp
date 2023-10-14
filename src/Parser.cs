using System.Text.Json;
using RestSharp;
using Sdkgen.Client.Exception;

namespace Sdkgen.Client;

public class Parser
{
    private readonly string _baseUrl;

    public Parser(string baseUrl)
    {
        this._baseUrl = baseUrl;
    }

    public string Url(string path, IReadOnlyDictionary<string, object> parameters)
    {
        return this._baseUrl + "/" + this.SubstituteParameters(path, parameters);
    }

    public void Query(RestRequest request, IReadOnlyDictionary<string, object> parameters)
    {
        foreach (KeyValuePair<string, object> entry in parameters)
        {
            if (entry.Value == null)
            {
                continue;
            }

            request.AddParameter(entry.Key, this.ToString(entry.Value));
        }
    }

    public T Parse<T>(string? payload)
    {
        try
        {
            if (payload == null)
            {
                throw new ParserException("The provided JSON data is invalid");
            }

            T? val = JsonSerializer.Deserialize<T>(payload);
            if (val == null)
            {
                throw new ParserException("The provided JSON data is invalid");
            }

            return val;
        }
        catch (JsonException e)
        {
            throw new ParserException("The provided JSON data is invalid: " + e.Message, e);
        }
    }

    private string SubstituteParameters(string path, IReadOnlyDictionary<string, object> parameters)
    {
        string[] parts = path.Split("/");
        List<string> result = new List<string>();

        foreach (string part in parts)
        {
            if (String.IsNullOrEmpty(part))
            {
                continue;
            }

            string? name = null;
            if (part.StartsWith(":"))
            {
                name = part.Substring(1);
            }
            else if (part.StartsWith("$"))
            {
                int pos = part.IndexOf("<");
                name = pos != -1 ? part.Substring(1, pos) : part.Substring(1);
            }
            else if (part.StartsWith("{") && part.EndsWith("}"))
            {
                name = part.Substring(1, part.Length - 1);
            }

            if (name != null && parameters.ContainsKey(name))
            {
                result.Add(this.ToString(parameters[name]));
            }
            else
            {
                result.Add(part);
            }
        }

        return string.Join("/", result);
    }

    private string ToString(object value)
    {
        if (value is string)
        {
            return value.ToString();
        }
        else if (value is float || value is double)
        {
            return value.ToString();
        }
        else if (value is int)
        {
            return value.ToString();
        }
        else if (value is bool)
        {
            return value.Equals(true) ? "1" : "0";
        }
        else if (value is DateOnly)
        {
            return ((DateOnly)value).ToString("yyyy-MM-dd");
        }
        else if (value is DateTime)
        {
            return ((DateTime)value).ToString("O");
        }
        else if (value is TimeOnly)
        {
            return ((TimeOnly)value).ToString("HH:mm:ss");
        }
        else
        {
            return "";
        }
    }
}