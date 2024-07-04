using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TestAPI.Helpers
{
    public class APIConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("Deployment", DefaultValue = "TEST", IsRequired = true)]
        public string Deployment
        {
            get
            {
                return (string)this["Deployment"];
            }
            set
            {
                this["Deployment"] = value;
            }
        }

        [ConfigurationProperty("Port", DefaultValue = "6536", IsRequired = false)]
        public string port
        {
            get
            {
                return this["Port"].ToString();
            }

            set
            {
                this["Port"] = value;
            }
        }

        [ConfigurationProperty("ApiUri", DefaultValue = "http://localhost", IsRequired = true)]
        public string ApiUri
        {
            get
            {
                return this["ApiUri"].ToString();
            }
            set
            {
                this["ApiUri"] = value;
            }
        }

        [ConfigurationProperty("ApiKey", DefaultValue = "", IsRequired = true)]
        public string ApiKey
        {
            get
            {
                return this["ApiKey"].ToString();
            }
            set
            {
                this["ApiKey"] = value;
            }
        }

        [ConfigurationProperty("ApiUserId", DefaultValue = "", IsRequired = true)]
        public string ApiUserId
        {
            get
            {
                return this["ApiUserId"].ToString();
            }
            set
            {
                this["ApiUserId"] = value;
            }
        }

        [ConfigurationProperty("ApiResource", DefaultValue = "", IsRequired = false)]
        public string ApiResource
        {
            get
            {
                return this["ApiResource"].ToString();
            }
            set
            {
                this["ApiResource"] = value;
            }
        }

        [ConfigurationProperty("SenderId", DefaultValue = "", IsRequired = true)]
        public string SenderId
        {
            get
            {
                return this["SenderId"].ToString();
            }
            set
            {
                this["SenderId"] = value;
            }
        }
        [ConfigurationProperty("ReceiverId", DefaultValue = "", IsRequired = false)]
        public string ReceiverId
        {
            get
            {
                return this["ReceiverId"].ToString();
            }
            set
            {
                this["ReceiverId"] = value;
            }
        }
        [ConfigurationProperty("PayerId", DefaultValue = "", IsRequired = false)]
        public string PayerId
        {
            get
            {
                return this["PayerId"].ToString();
            }
            set
            {
                this["PayerId"] = value;
            }
        }

        [ConfigurationProperty("UserName", DefaultValue = "", IsRequired = true)]
        public string UserName
        {
            get
            {
                return this["UserName"].ToString();
            }
            set
            {
                this["UserName"] = value;
            }
        }

        [ConfigurationProperty("Password", DefaultValue = "", IsRequired = true)]
        public string Password
        {
            get
            {
                return this["Password"].ToString();
            }
            set
            {
                this["Password"] = value;
            }
        }

        [ConfigurationProperty("Token", DefaultValue = "", IsRequired = true)]
        public char[] Token
        {
            get
            {
                char[] token = null;
                string temp = (string)this["Token"];
                for (int i = 0; i < temp.Length; i++)
                {
                    token[i] = Convert.ToChar(temp[i]);
                }  
                
                return token;
            }
            set
            {
                this["Token"] = value;
            }
        }
    }
}