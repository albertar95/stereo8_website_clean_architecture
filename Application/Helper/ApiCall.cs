using IdentityModel.Client;
using System.Net;

namespace Application.Helpers
{
    public class ApiCall
    {
        private static string TokenBaseAddress = "http://localhost:8077/connect/token";
        public enum HttpMethods
        {
            Get, Post, Put, Patch, Delete, Option
        }
        public enum ConsumerType 
        {
            BackendUI, FrontendUI
        }
        public static async Task<ApiReturnResult> Call(ConsumerType consumer, HttpMethods method, string actionUrl,StringContent content = null) 
        {
            var result = new ApiReturnResult();
            switch (consumer)
            {
                case ConsumerType.BackendUI:
                    switch (method)
                    {
                        case HttpMethods.Get:
                            using (var client = new HttpClient())
                            {
                                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                                {
                                    Address = TokenBaseAddress,
                                    ClientId = "BackendUI",
                                    ClientSecret = "secret",
                                    Scope = "BackendApi"
                                });
                                client.SetBearerToken(tokenResponse.AccessToken);
                                var response = client.GetAsync(actionUrl);
                                result.ResultCode = response.Result.StatusCode;
                                result.Content = await response.Result.Content.ReadAsStringAsync();
                            }
                            break;
                        case HttpMethods.Post:
                            using (var client = new HttpClient())
                            {
                                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                                {
                                    Address = TokenBaseAddress,
                                    ClientId = "BackendUI",
                                    ClientSecret = "secret",
                                    Scope = "BackendApi"
                                });
                                client.SetBearerToken(tokenResponse.AccessToken);
                                var response = client.PostAsync(actionUrl, content);
                                result.ResultCode = response.Result.StatusCode;
                                result.Content = await response.Result.Content.ReadAsStringAsync();
                            }
                            break;
                        case HttpMethods.Put:
                            using (var client = new HttpClient())
                            {
                                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                                {
                                    Address = TokenBaseAddress,
                                    ClientId = "BackendUI",
                                    ClientSecret = "secret",
                                    Scope = "BackendApi"
                                });
                                client.SetBearerToken(tokenResponse.AccessToken);
                                var response = client.PutAsync(actionUrl, content);
                                result.ResultCode = response.Result.StatusCode;
                                result.Content = await response.Result.Content.ReadAsStringAsync();
                            }
                            break;
                        case HttpMethods.Patch:
                            using (var client = new HttpClient())
                            {
                                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                                {
                                    Address = TokenBaseAddress,
                                    ClientId = "BackendUI",
                                    ClientSecret = "secret",
                                    Scope = "BackendApi"
                                });
                                client.SetBearerToken(tokenResponse.AccessToken);
                                var response = client.PatchAsync(actionUrl, content);
                                result.ResultCode = response.Result.StatusCode;
                                result.Content = await response.Result.Content.ReadAsStringAsync();
                            }
                            break;
                        case HttpMethods.Delete:
                            using (var client = new HttpClient())
                            {
                                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                                {
                                    Address = TokenBaseAddress,
                                    ClientId = "BackendUI",
                                    ClientSecret = "secret",
                                    Scope = "BackendApi"
                                });
                                client.SetBearerToken(tokenResponse.AccessToken);
                                var response = client.DeleteAsync(actionUrl);
                                result.ResultCode = response.Result.StatusCode;
                                result.Content = await response.Result.Content.ReadAsStringAsync();
                            }
                            break;
                        case HttpMethods.Option:
                            using (var client = new HttpClient())
                            {
                                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                                {
                                    Address = TokenBaseAddress,
                                    ClientId = "BackendUI",
                                    ClientSecret = "secret",
                                    Scope = "BackendApi"
                                });
                                client.SetBearerToken(tokenResponse.AccessToken);
                                var response = client.GetAsync(actionUrl);
                                result.ResultCode = response.Result.StatusCode;
                                result.Content = await response.Result.Content.ReadAsStringAsync();
                            }
                            break;
                    }
                    break;
                case ConsumerType.FrontendUI:
                    switch (method)
                    {
                        case HttpMethods.Get:
                            using (var client = new HttpClient())
                            {
                                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                                {
                                    Address = TokenBaseAddress,
                                    ClientId = "FrontendUI",
                                    ClientSecret = "secret",
                                    Scope = "FrontendApi"
                                });
                                client.SetBearerToken(tokenResponse.AccessToken);
                                var response = client.GetAsync(actionUrl);
                                result.ResultCode = response.Result.StatusCode;
                                result.Content = await response.Result.Content.ReadAsStringAsync();
                            }
                            break;
                        case HttpMethods.Post:
                            using (var client = new HttpClient())
                            {
                                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                                {
                                    Address = TokenBaseAddress,
                                    ClientId = "FrontendUI",
                                    ClientSecret = "secret",
                                    Scope = "FrontendApi"
                                });
                                client.SetBearerToken(tokenResponse.AccessToken);
                                var response = client.PostAsync(actionUrl, content);
                                result.ResultCode = response.Result.StatusCode;
                                result.Content = await response.Result.Content.ReadAsStringAsync();
                            }
                            break;
                        case HttpMethods.Put:
                            using (var client = new HttpClient())
                            {
                                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                                {
                                    Address = TokenBaseAddress,
                                    ClientId = "FrontendUI",
                                    ClientSecret = "secret",
                                    Scope = "FrontendApi"
                                });
                                client.SetBearerToken(tokenResponse.AccessToken);
                                var response = client.PutAsync(actionUrl, content);
                                result.ResultCode = response.Result.StatusCode;
                                result.Content = await response.Result.Content.ReadAsStringAsync();
                            }
                            break;
                        case HttpMethods.Patch:
                            using (var client = new HttpClient())
                            {
                                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                                {
                                    Address = TokenBaseAddress,
                                    ClientId = "FrontendUI",
                                    ClientSecret = "secret",
                                    Scope = "FrontendApi"
                                });
                                client.SetBearerToken(tokenResponse.AccessToken);
                                var response = client.PatchAsync(actionUrl, content);
                                result.ResultCode = response.Result.StatusCode;
                                result.Content = await response.Result.Content.ReadAsStringAsync();
                            }
                            break;
                        case HttpMethods.Delete:
                            using (var client = new HttpClient())
                            {
                                var response = client.DeleteAsync(actionUrl);
                                result.ResultCode = response.Result.StatusCode;
                                result.Content = await response.Result.Content.ReadAsStringAsync();
                            }
                            break;
                        case HttpMethods.Option:
                            using (var client = new HttpClient())
                            {
                                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                                {
                                    Address = TokenBaseAddress,
                                    ClientId = "FrontendUI",
                                    ClientSecret = "secret",
                                    Scope = "FrontendApi"
                                });
                                client.SetBearerToken(tokenResponse.AccessToken);
                                var response = client.GetAsync(actionUrl);
                                result.ResultCode = response.Result.StatusCode;
                                result.Content = await response.Result.Content.ReadAsStringAsync();
                            }
                            break;
                    }
                    break;
            }
            return result;
        }
    }
    public class ApiReturnResult 
    {
        public string Content { get; set; } = null!;
        public HttpStatusCode ResultCode { get; set; }
        public bool IsSuccessfulResult() 
        {
            if ((int)ResultCode >= 200 && (int)ResultCode < 300)
                return true;
            else
                return false;
        }
    }
}
