/*
 * SDKgen is a powerful code generator to automatically build client SDKs for your REST API.
 * For the current version and information visit <https://sdkgen.app>
 *
 * Copyright (c) Christoph Kappestein <christoph.kappestein@gmail.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using RestSharp;
using Sdkgen.Client.Exception;

namespace Sdkgen.Client;

public class Parser
{
    private readonly string _baseUrl;

    public Parser(string baseUrl)
    {
        this._baseUrl = this.NormalizeBaseUrl(baseUrl);
    }

    public string Url(string path, IReadOnlyDictionary<string, object?> parameters)
    {
        return this._baseUrl + "/" + this.SubstituteParameters(path, parameters);
    }

    public void Query(RestRequest request, IReadOnlyDictionary<string, object?> parameters)
    {
        this.Query(request, parameters, new List<string>());
    }

    public void Query(RestRequest request, IReadOnlyDictionary<string, object?> parameters, List<string> structNames)
    {
        foreach (KeyValuePair<string, object?> entry in parameters)
        {
            if (entry.Value is null)
            {
                continue;
            }

            if (structNames.Contains(entry.Key))
            {
                Dictionary<string, object?> nestedProperties = new();
                var properties = entry.Value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

                foreach (var propertyInfo in properties)
                {
                    var name = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name;
                    var value = propertyInfo.GetValue(entry.Value, null);

                    if (name is null) {
                        continue;
                    }

                    nestedProperties.Add(name, value);
                }

                this.Query(request, nestedProperties);
            }
            else
            {
                request.AddParameter(entry.Key, this.ToString(entry.Value));
            }
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

    private string SubstituteParameters(string path, IReadOnlyDictionary<string, object?> parameters)
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
                name = pos != -1 ? part.Substring(1, pos - 1) : part.Substring(1);
            }
            else if (part.StartsWith("{") && part.EndsWith("}"))
            {
                name = part.Substring(1, part.Length - 2);
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

    private string ToString(object? value)
    {
        if (value is null)
        {
            return "";
        }
        else if (value is string)
        {
            return value.ToString() ?? "";
        }
        else if (value is float)
        {
            return ((float) value).ToString("0.00", CultureInfo.InvariantCulture);
        }
        else if (value is double)
        {
            return ((double) value).ToString("0.00", CultureInfo.InvariantCulture);
        }
        else if (value is int)
        {
            return value.ToString() ?? "";
        }
        else if (value is bool)
        {
            return value.Equals(true) ? "1" : "0";
        }
        else if (value is DateOnly)
        {
            return ((DateOnly) value).ToString("yyyy-MM-dd");
        }
        else if (value is DateTime)
        {
            return ((DateTime) value).ToString("yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture);
        }
        else if (value is TimeOnly)
        {
            return ((TimeOnly) value).ToString("HH:mm:ss");
        }
        else
        {
            return "";
        }
    }

    private string NormalizeBaseUrl(string baseUrl) {
        if (baseUrl.EndsWith("/")) {
            baseUrl = baseUrl.Substring(0, baseUrl.Length - 1);
        }
        return baseUrl;
    }

}