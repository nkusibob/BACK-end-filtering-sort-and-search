using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
    public class UserApi
    {
        public  HttpClient Initial()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:54254/");
            return client;
        }
    }
}
