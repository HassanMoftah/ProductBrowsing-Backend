using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ProductsBrowsing_backend.Security
{
    public class AuthAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {

            string token = "";
            try
            {
                token = actionContext.Request.Headers.GetValues("Authorization").First().ToString();

                var claimsPrincipal = TokenManager.IsValidUser(token);
               
                if(claimsPrincipal==null)
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized);
                }


            }

            catch
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);

            }
        }
    }
}