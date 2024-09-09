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
        public async Task<IActionResult> PostClubPresidentAsync([FromBody] ClubPresident clubPresident)
        {

            ClubPresidentService service = new();
            var isSuccessful = await service.InsertClubPresidentAsync(clubPresident);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }
       
    }
}
