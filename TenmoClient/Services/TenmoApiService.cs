using RestSharp;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;
        //protected static RestClient client;

        public TenmoApiService(string apiUrl) : base(apiUrl)
        {
            //if (client == null)
            //{
            //    client = new RestClient(apiUrl);
            //}
        }

        // Add methods to call api here...
        public List<ApiUser> GetAllUsers()
        {
            RestRequest request = new RestRequest("");
            IRestResponse<List<ApiUser>> response = client.Get<List<ApiUser>>(request);
            CheckForError(response);
            return response.Data;
        }
        public ApiUser GetUserById(int id)
        {
            RestRequest request = new RestRequest("id");
            IRestResponse<ApiUser> response = client.Get<ApiUser>(request);
            CheckForError(response);
            return response.Data;
        }
        public Account GetAccountByUserId(int id)
        {
            RestRequest request = new RestRequest("account/user/id");
            IRestResponse<Account> response = client.Get<Account>(request);
            CheckForError(response);
            return response.Data;
        }
        public decimal GetBalanceByUserId()
        {
            RestRequest request = new RestRequest("account/balance");
            IRestResponse<decimal> response = client.Get<decimal>(request);
            CheckForError(response);
            return (decimal)response.Data;
        }
        public ApiUser GetUserByUsername(string username)
        {
            RestRequest request = new RestRequest("login");
            IRestResponse<ApiUser> response = client.Get<ApiUser>(request);
            CheckForError(response);
            return response.Data;
        }
        //public ApiUser GetUserByUsername(string username)
        //{
        //    RestRequest request = new RestRequest("")
        //}

    }
}
