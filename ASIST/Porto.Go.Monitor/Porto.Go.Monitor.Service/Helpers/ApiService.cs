using Porto.Go.Monitor.Service.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Porto.Go.Monitor.Service.Helpers
{
    public class ApiService : IApiService
    {
        private readonly HttpClient client;
        private readonly HttpContent content;
        private readonly string username;
        private readonly string password;

        public TokenResponse TokenReponse { get; private set; }

        public ApiService()
        {
            username = ConfigurationManager.AppSettings["Api.User"];
            password = ConfigurationManager.AppSettings["Api.Password"];

            client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Api.Url"]);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            content = new StringContent("grant_type=password&username=" + username + "&password=" + password, Encoding.UTF8, "application/x-www-form-urlencoded");
        }

        public void Login()
        {
            HttpResponseMessage response = client.PostAsync("/Token", content).Result;

            if (response.IsSuccessStatusCode)
            {
                TokenResponse tokenResponse = response.Content.ReadAsAsync<TokenResponse>().Result;

                this.TokenReponse = tokenResponse;
            }
            else
            {
                throw new HttpException("Authentication failed!");
            }
        }

        public IEnumerable<UserDto> GetUsers()
        {
            if (this.TokenReponse == null)
            {
                Login();
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.TokenReponse.AccessToken);

            HttpResponseMessage response = client.GetAsync("api/account/users").Result;

            if (response.IsSuccessStatusCode)
            {
                IEnumerable<UserDto> users = response.Content.ReadAsAsync<IEnumerable<UserDto>>().Result;

                return users;
            }
            else
            {
                throw new ApplicationException("Error while retriving users from WebApi");
            }
        }
    }
}
