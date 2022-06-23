using Microsoft.AspNetCore.Http;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility.HTTP.Helper
{
    public class HttpHelper
    {
        private static IHttpContextAccessor _context;

        public static void SetHttpContextAccessor(IHttpContextAccessor context)
        {
            _context = context;
        }

        public static async Task<IRestResponse> PostDataFromAPI(string url, dynamic param, string token = "")
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    token = _context.HttpContext.Items["jwtToken"].ToString();
                }

                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);

                if (param is not null)
                {
                    request.AddJsonBody(param);
                }

                request.AddHeader("Authorization", $"Bearer {token}");
                IRestResponse response = await client.ExecuteAsync(request);

                return response;
            }catch(Exception)
            {
                throw;
            }
        }


        public static async Task<IRestResponse> GetDataFromAPI(string url, dynamic param, string token = "")
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    token = _context.HttpContext.Items["jwtToken"].ToString();
                }

                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);


                if (param is not null)
                {
                    request.AddJsonBody(param);
                }

                request.AddHeader("Authorization", $"Bearer {token}");
                IRestResponse response = await client.ExecuteAsync(request);

                return response;
            }catch(Exception)
            {
                throw;
            }
        }


    }
}
