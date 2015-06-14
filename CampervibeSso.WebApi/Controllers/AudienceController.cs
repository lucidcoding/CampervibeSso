using CampervibeSso.WebApi.Entities;
using CampervibeSso.WebApi.Models;
using CampervibeSso.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CampervibeSso.WebApi.Controllers
{
    [RoutePrefix("api/audience")]
    public class AudienceController : ApiController
    {
        [Route("")]
        public IHttpActionResult Post(AudienceModel audienceModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using(var audienceRepository = new AudienceRepository())
            {
                var audience = Audience.Create(audienceModel.Name);
                audienceRepository.Add(audience);

            //Audience newAudience = AudiencesStore.AddAudience(audienceModel.Name);

                return Ok<Audience>(audience);
            }
        }
    }
}
