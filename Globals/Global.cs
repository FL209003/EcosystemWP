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

        public static HttpResponseMessage PostAsJson(string url, Object model)
        {
            HttpClient client = new();

            Task<HttpResponseMessage> task = client.PostAsJsonAsync(url, model);
            task.Wait();

            HttpResponseMessage response = task.Result;
            return response;
        }

        public static HttpResponseMessage PutAsJson(string url, string str)
        {
            HttpClient client = new();

            Task<HttpResponseMessage> task = client.PutAsJsonAsync(url, str);
            task.Wait();

            HttpResponseMessage response = task.Result;
            return response;
        }

        public static HttpResponseMessage Delete(string url)
        {
            HttpClient client = new();

            Task<HttpResponseMessage> task = client.DeleteAsync(url);
            task.Wait();

            HttpResponseMessage response = task.Result;
            return response;
        }
    }
}
