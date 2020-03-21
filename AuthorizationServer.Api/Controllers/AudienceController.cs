using AuthorizationServer.Api.Models;
using System.Web.Http;

namespace AuthorizationServer.Api.Controllers
{
    [RoutePrefix("/api/audience")]
    public class AudienceController : ApiController
    {
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(AudienceModel audienceModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newAudience = AudiencesStore.AddAudience(audienceModel.Name);

            return Ok(newAudience);
        }
    }
}
