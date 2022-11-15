using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Identity.Client;
//using System.Web.Http.Cors;

//using Microsoft.Identity.Client;
//using Microsoft.PowerPlatform.Dataverse.Client;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http;
//using System.Web.Mvc;

namespace GetTokenAPI.Controllers
{
    public class TokenController : ApiController
    {

        [Route("api/TokenController/GetToken")]
        [HttpPost]
        public string GetToken([FromBody] RequestParameter requestParameter)
        //public string GetToken()
        {
            string environmentType = requestParameter.environmentType;
            var token = string.Empty;
            string url = string.Empty;
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            if (environmentType == "Dev")
            {
                url = ConfigurationManager.AppSettings["DevURL"];
                clientId = ConfigurationManager.AppSettings["DevClientId"];
                clientSecret = ConfigurationManager.AppSettings["DevClientSecret"];
            }
            else if (environmentType == "Prod")
            {

            }

            string connectionString = $@"
                                        AuthType = ClientSecret;
                                        Url = {url};
                                        ClientId = {clientId};
                                        ClientSecret = {clientSecret};";

            using (ServiceClient serviceClient = new ServiceClient(connectionString))
            {
                if (serviceClient.IsReady)
                {
                    token = serviceClient.CurrentAccessToken;
                }
            }
            return token;
            //return "invoked";
        }

        [Route("api/TokenController/GetToken1")]
        [HttpPost]
        public string GetToken1()
        {
            return "invoked";
        }
    }
    public class RequestParameter
    {
        //[JsonProperty("Source")]
        public Dictionary<string, string> Source { get; set; }

        public Dictionary<string, string> CrmData { get; set; }
        public string environmentType { get; set; }
    }
}
