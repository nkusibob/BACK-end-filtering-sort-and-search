using DomainPsr03951.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication2.Helper
{
    public class AccountHelper
    {
        static string token = "e08d7f7e-662e-41ba-97f1-d454134aa033n";
        readonly string uri = "http://localhost:55739/Help/Api/POST-api-AccountApi/";

        public async Task<List<User>> GetMemberAsync(User ac)
        {

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {

                    return JsonConvert.DeserializeObject<List<User>>(
                        await httpClient.GetStringAsync(uri)
                    );
                }
            }
            catch (Exception EX)
            {

                throw EX;
            }
        }
    }
}

