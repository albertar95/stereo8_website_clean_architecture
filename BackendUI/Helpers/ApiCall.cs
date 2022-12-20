using System.Net;

namespace BackendUI.Helpers
{
    public class ApiCall
    {
        public enum HttpMethods
        {
            Get, Post, Put, Patch, Delete, Option
        }
        public static async Task<ApiReturnResult> Call(HttpMethods method, string actionUrl,StringContent content = null) 
        {
            var result = new ApiReturnResult();
            switch (method)
            {
                case HttpMethods.Get:
                    using (var client = new HttpClient())
                    {
                        var response = client.GetAsync(actionUrl);
                        result.ResultCode = response.Result.StatusCode;
                        result.Content = await response.Result.Content.ReadAsStringAsync();
                    }
                    break;
                case HttpMethods.Post:
                    break;
                case HttpMethods.Put:
                    break;
                case HttpMethods.Patch:
                    break;
                case HttpMethods.Delete:
                    break;
                case HttpMethods.Option:
                    break;
            }
            return result;
        }
    }
    public class ApiReturnResult 
    {
        public string Content { get; set; } = null!;
        public HttpStatusCode ResultCode { get; set; }
    }
}
