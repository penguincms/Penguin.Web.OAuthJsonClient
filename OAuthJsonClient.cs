﻿using System;
using System.Linq;
using System.Net;

namespace Penguin.Web
{
    public class OAuthJsonClient : JsonClient
    {
        private OAuthToken Token { get; set; }

        public OAuthJsonClient(string Key, string Secret, string AccessUrl, string Username = null, string Password = null)
        {
            string encoded = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Key + ":" + Secret));

            using WebClient client = new();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
            client.Headers.Add("Authorization", "Basic " + encoded);

            string data = !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password)
                ? $"grant_type=password&username={Username}&password={Password}"
                : $"grant_type=client_credentials";

            string response = client.UploadString(AccessUrl, data);

            Token = Newtonsoft.Json.JsonConvert.DeserializeObject<OAuthToken>(response);

            ApplyAuthHeader(this, Token);
        }

        public static void ApplyAuthHeader(WebClient client, OAuthToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (token is null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            client.Headers.Add("Authorization", $"{token.TokenType} {token.AccessToken}");
        }

        protected override void PreRequest(Uri url)
        {
            if (!Headers.AllKeys.Contains("Authorization"))
            {
                ApplyAuthHeader(this, Token);
            }
        }
    }
}