using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using apsys.project.web.Infraestructure;

namespace apsys.project.web.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet, Route("api/noauth")]
        public IHttpActionResult NoAuthenticated()
        {
            return Ok("Hello from a no authenticated endpoint");
        }

        [HttpGet, Route("api/auth")]
        [Authorize]
        public IHttpActionResult Authenticated()
        {
            var allClaims = this.User.GetClaims();
            var dic = from claim in allClaims
                      select (new
                      {
                          type = claim.Type,
                          value = claim.Value,
                      });
            return Ok(dic);
        }

    }
}
