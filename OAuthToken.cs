using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Penguin.Web
{
    public class OAuthToken
    {
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
                if (this.tokenType is null)
                {
                    return null;
                }

                string toReturn = string.Empty;


                for (int i = 0; i < this.tokenType.Length; i++)
                {
                    if (i == 0)
                    {
                        toReturn += char.ToUpper(this.tokenType[i]);
                    }
                    else
                    {
                        toReturn += this.tokenType[i];
                    }
                }

                return toReturn;
            }
            set => this.tokenType = value;
        }

        private string tokenType;
    }
}
