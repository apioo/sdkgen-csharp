using System.Text.Json;
using System.Transactions;
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

    public string Url(string path, Dictionary<string, Object> parameters)
    {
        return this._baseUrl + "/" + this.SubstituteParameters(path, parameters);
    }

    public void Query(RestRequest request, Dictionary<string, Object> parameters)
    {
        foreach (KeyValuePair<string, Object> entry in parameters)
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

    private string SubstituteParameters(string path, Dictionary<string, Object> parameters)
    {
        return "";
    }

    private string ToString(Object value)
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