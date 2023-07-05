using RestSharp;
using System.Collections.Generic;
using System.Reflection.Metadata;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;

        public TenmoApiService(string apiUrl) : base(apiUrl) { }

        // Add methods to call api here...
        public List<ApiUser> GetAllUsers()
        {
            RestRequest request = new RestRequest("tenmo_user");
            IRestResponse<List<ApiUser>> response = client.Get<List<ApiUser>>(request);
            CheckForError(response);
            return response.Data;
        }
        //public ApiUser GetUserById(int id)
        //{

        //}

    }
}
