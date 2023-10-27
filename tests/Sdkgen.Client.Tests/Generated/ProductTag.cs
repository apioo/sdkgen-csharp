/**
 * ProductTag automatically generated by SDKgen please do not edit this file manually
 * @see https://sdkgen.app
 */


using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using RestSharp;
using Sdkgen.Client;
using Sdkgen.Client.Exception;

namespace Sdkgen.Client.Tests.Generated;

public class ProductTag : TagAbstract {
    public ProductTag(RestClient httpClient, Parser parser): base(httpClient, parser)
    {
    }


    /**
     * Returns a collection
     */
    public async Task<TestResponse> GetAll(int startIndex, int count, string search)
    {
        try
        {
            Dictionary<string, object> pathParams = new Dictionary<string, object>();

            Dictionary<string, object> queryParams = new Dictionary<string, object>();
            queryParams.Add("startIndex", startIndex);
            queryParams.Add("count", count);
            queryParams.Add("search", search);

            RestRequest request = new RestRequest(this.Parser.Url("/anything", pathParams), Method.Get);
            this.Parser.Query(request, queryParams);

            RestResponse response = await this.HttpClient.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return this.Parser.Parse<TestResponse>(response.Content);
            }

            switch ((int) response.StatusCode)
            {
                default:
                    throw new UnknownStatusCodeException("The server returned an unknown status code");
            }
        }
        catch (ClientException e)
        {
            throw e;
        }
        catch (System.Exception e)
        {
            throw new ClientException("An unknown error occurred: " + e.Message, e);
        }
    }

    /**
     * Creates a new product
     */
    public async Task<TestResponse> Create(TestRequest payload)
    {
        try
        {
            Dictionary<string, object> pathParams = new Dictionary<string, object>();

            Dictionary<string, object> queryParams = new Dictionary<string, object>();

            RestRequest request = new RestRequest(this.Parser.Url("/anything", pathParams), Method.Post);
            this.Parser.Query(request, queryParams);
            request.AddJsonBody(JsonSerializer.Serialize(payload));

            RestResponse response = await this.HttpClient.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return this.Parser.Parse<TestResponse>(response.Content);
            }

            switch ((int) response.StatusCode)
            {
                default:
                    throw new UnknownStatusCodeException("The server returned an unknown status code");
            }
        }
        catch (ClientException e)
        {
            throw e;
        }
        catch (System.Exception e)
        {
            throw new ClientException("An unknown error occurred: " + e.Message, e);
        }
    }

    /**
     * Updates an existing product
     */
    public async Task<TestResponse> Update(int id, TestRequest payload)
    {
        try
        {
            Dictionary<string, object> pathParams = new Dictionary<string, object>();
            pathParams.Add("id", id);

            Dictionary<string, object> queryParams = new Dictionary<string, object>();

            RestRequest request = new RestRequest(this.Parser.Url("/anything/:id", pathParams), Method.Put);
            this.Parser.Query(request, queryParams);
            request.AddJsonBody(JsonSerializer.Serialize(payload));

            RestResponse response = await this.HttpClient.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return this.Parser.Parse<TestResponse>(response.Content);
            }

            switch ((int) response.StatusCode)
            {
                default:
                    throw new UnknownStatusCodeException("The server returned an unknown status code");
            }
        }
        catch (ClientException e)
        {
            throw e;
        }
        catch (System.Exception e)
        {
            throw new ClientException("An unknown error occurred: " + e.Message, e);
        }
    }

    /**
     * Patches an existing product
     */
    public async Task<TestResponse> Patch(int id, TestRequest payload)
    {
        try
        {
            Dictionary<string, object> pathParams = new Dictionary<string, object>();
            pathParams.Add("id", id);

            Dictionary<string, object> queryParams = new Dictionary<string, object>();

            RestRequest request = new RestRequest(this.Parser.Url("/anything/:id", pathParams), Method.Patch);
            this.Parser.Query(request, queryParams);
            request.AddJsonBody(JsonSerializer.Serialize(payload));

            RestResponse response = await this.HttpClient.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return this.Parser.Parse<TestResponse>(response.Content);
            }

            switch ((int) response.StatusCode)
            {
                default:
                    throw new UnknownStatusCodeException("The server returned an unknown status code");
            }
        }
        catch (ClientException e)
        {
            throw e;
        }
        catch (System.Exception e)
        {
            throw new ClientException("An unknown error occurred: " + e.Message, e);
        }
    }

    /**
     * Deletes an existing product
     */
    public async Task<TestResponse> Delete(int id)
    {
        try
        {
            Dictionary<string, object> pathParams = new Dictionary<string, object>();
            pathParams.Add("id", id);

            Dictionary<string, object> queryParams = new Dictionary<string, object>();

            RestRequest request = new RestRequest(this.Parser.Url("/anything/:id", pathParams), Method.Delete);
            this.Parser.Query(request, queryParams);

            RestResponse response = await this.HttpClient.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return this.Parser.Parse<TestResponse>(response.Content);
            }

            switch ((int) response.StatusCode)
            {
                default:
                    throw new UnknownStatusCodeException("The server returned an unknown status code");
            }
        }
        catch (ClientException e)
        {
            throw e;
        }
        catch (System.Exception e)
        {
            throw new ClientException("An unknown error occurred: " + e.Message, e);
        }
    }


}
