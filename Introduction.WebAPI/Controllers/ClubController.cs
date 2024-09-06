using Introduction.Model;
using Introduction.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using System.Linq;

namespace Introduction.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        [HttpGet]

        public IActionResult GetAllClubs()
        {
            ClubService service = new();
            var clubs = service.GetAllClubs();
            if (clubs is null)
            {
                return NotFound();
            }
            return Ok(clubs);

        }


        [HttpGet]
        [Route("{id}")]

        public IActionResult GetClubById(Guid id)
        {
            ClubService service = new();
            var club = service.GetClubById(id);
            if (club == null)
            {
                return NotFound();
            }
            return Ok(club);

        }


        [HttpPost]
        public IActionResult PostClub([FromBody] Club club)
        {

            ClubService service = new();
            var isSuccessful = service.InsertClub(club);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpPut]
        [Route("{id}")]

        public IActionResult UpdateClub(Guid id, [FromBody] ClubUpdate club)
        {
            ClubService service = new();
            var isSuccessful = service.UpdateClub(id, club);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteClub(Guid id)
        {
            ClubService service = new();
            var isSuccessful = service.DeleteClub(id);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }


        

    }
}
