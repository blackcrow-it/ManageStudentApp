using ManageStudentApp.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ManageStudentApp.Service
{
    class APIHandle
    {
        public async static Task<HttpResponseMessage> Sign_In(string username, string password)
        {
            Dictionary<String, String> memberLogin = new Dictionary<string, string>();
            memberLogin.Add("username", username);
            memberLogin.Add("password", password);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(memberLogin), System.Text.Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(APIUrl.API_LOGIN, content);
            Debug.WriteLine(response.Result.StatusCode);
            return response.Result;
        }
        public async static Task<HttpResponseMessage> GetData(string api_url, string scheme, string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, token);
            var response = httpClient.GetAsync(api_url);
            return response.Result;
        }

    }
}
