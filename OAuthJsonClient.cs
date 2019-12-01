using Penguin.Web;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Penguin.Web
{
    

    public class OAuthJsonClient : JsonClient
    {
        private OAuthToken Token { get; set; }

        public OAuthJsonClient(string Key, string Secret, string AccessUrl, string Username = null, string Password = null)
        {
            string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Key + ":" + Secret));

            WebClient client = new WebClient();

            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
            client.Headers.Add("Authorization", "Basic " + encoded);

            string data;

            if(!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                data = $"grant_type=password&username={Username}&password={Password}";
            }
            else
            {
                data = $"grant_type=client_credentials";
            }

            string response = client.UploadString(AccessUrl, data);

            Token = Newtonsoft.Json.JsonConvert.DeserializeObject<OAuthToken>(response);

            this.Headers.Add("Authorization", $"{Token.token_type} {Token.access_token}");
        }


    }
}
