using ProductsBrowsing_backend.Models;
using ProductsBrowsing_backend.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProductsBrowsing_backend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {

        public IHttpActionResult Login(MDUser user)
        {
            bool valid = MDUser.IsValid(user);
            if (!valid) return Unauthorized();
            string token = TokenManager.GenerateToken();
            return Ok(token);
        }
    }
}
