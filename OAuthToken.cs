﻿using Newtonsoft.Json;

namespace Penguin.Web
{
    public class OAuthToken
    {
        private string tokenType;

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("token_type")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "<Pending>")]
        public string TokenType
        {
            get
            {
                if (tokenType is null)
                {
                    return null;
                }

                string toReturn = string.Empty;

                for (int i = 0; i < tokenType.Length; i++)
                {
                    if (i == 0)
                    {
                        toReturn += char.ToUpper(tokenType[i]);
                    }
                    else
                    {
                        toReturn += tokenType[i];
                    }
                }

                return toReturn;
            }
            set => tokenType = value;
        }
    }
}