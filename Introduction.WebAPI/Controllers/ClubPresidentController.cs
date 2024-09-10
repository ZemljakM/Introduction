using Introduction.Model;
using Introduction.Service;
using Introduction.Service.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Introduction.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubPresidentController : ControllerBase
    {

        private IClubPresidentService _service;

        public ClubPresidentController(IClubPresidentService service)
        {
            _service = service;
        }



        [HttpPost]
        public async Task<IActionResult> PostClubPresidentAsync([FromBody] ClubPresident clubPresident)
        {
            var isSuccessful = await _service.InsertClubPresidentAsync(clubPresident);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }
       
    }
}
