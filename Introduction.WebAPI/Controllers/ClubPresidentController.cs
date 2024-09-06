using Introduction.Model;
using Introduction.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Introduction.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubPresidentController : ControllerBase
    {
        


        [HttpPost]
        public IActionResult PostClubPresident([FromBody] ClubPresident clubPresident)
        {

            ClubPresidentService service = new();
            var isSuccessful = service.InsertClubPresident(clubPresident);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }
       
    }
}
