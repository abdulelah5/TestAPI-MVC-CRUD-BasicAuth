using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace TestAPI.Filters
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                var authHeader = actionContext.Request.Headers.Authorization;            

                if (authHeader != null)
                {
                    var authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                    var decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                    var usernamePasswordArray = decodedAuthenticationToken.Split(':');
                    var userName = usernamePasswordArray[0];
                    var password = usernamePasswordArray[1];

                    if (IsAuthorizedUser(userName, password))
                    {
                        var principal = new GenericPrincipal(new GenericIdentity(userName), null);
                        Thread.CurrentPrincipal = principal;
                        return;
                    }
                }                

                HandleUnathorized(actionContext);

            }
            catch (Exception ex)
            {
                ex = new Exception("Authentication Error");                
                throw;
            }
        }
        private static void HandleUnathorized(HttpActionContext actionContext)
        {
            var response = new
            {
                Code = "401",
                Error = "Access to the API services is not authorized.",
                Result = "Unauthorized Access",
            };


            string jsonString = JsonConvert.SerializeObject(response, Formatting.Indented);

            actionContext.Response = new HttpResponseMessage()
            {
                Content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.Unauthorized
            };
        }
        public static bool IsAuthorizedUser(string userName, string password)
        {
            if (!userName.Equals("Your_APIaccount")) return false;
            return AuthenticateUser(userName, password);
        }

        public static bool AuthenticateUser(string userName, string password)
        {
            bool ret = false;

            string ldp = ConfigurationManager.ConnectionStrings["ADConnectionString"].ConnectionString;

            try
            {
                DirectoryEntry de = new DirectoryEntry(ldp, userName, password);
                DirectorySearcher dsearch = new DirectorySearcher(de);
                SearchResult results = null;

                results = dsearch.FindOne();

                ret = true;
            }
            catch
            {
                ret = false;
            }

            return ret;
        }
    }
}