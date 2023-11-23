using NuGet.Common;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading;

namespace EcosystemApp.Globals
{
    public class Global
    {
        public static HttpResponseMessage GetResponse(string url)
        {
            HttpClient client = new();

            Task<HttpResponseMessage> task = client.GetAsync(url);
            task.Wait();

            HttpResponseMessage response = task.Result;
            return response;
        }

        public static string GetContent(HttpResponseMessage response)
        {
            HttpContent content = response.Content;

            Task<string> task = content.ReadAsStringAsync();
            task.Wait();

            return task.Result;
        }

        public static HttpResponseMessage PostAsJson(string url, Object model, string token)
        {
            HttpClient client = new();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Task<HttpResponseMessage> task = client.PostAsJsonAsync(url, model);
            task.Wait();

            HttpResponseMessage response = task.Result;
            return response;
        }

        public static HttpResponseMessage PutAsJson(string url, Object model, string token)
        {
            HttpClient client = new();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Task<HttpResponseMessage> task = client.PutAsJsonAsync(url, model);
            task.Wait();

            HttpResponseMessage response = task.Result;
            return response;
        }

        public static HttpResponseMessage Delete(string url, string token)
        {
            HttpClient client = new();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Task<HttpResponseMessage> task = client.DeleteAsync(url);
            task.Wait();

            HttpResponseMessage response = task.Result;
            return response;
        }
    }
}
