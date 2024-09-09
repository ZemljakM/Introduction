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

        public async Task<IActionResult> GetAllClubsAsync()
        {
            ClubService service = new();
            var clubs = await service.GetAllClubsAsync();
            if (clubs is null)
            {
                return NotFound();
            }
            return Ok(clubs);

        }


        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetClubByIdAsync(Guid id)
        {
            ClubService service = new();
            var club = await service.GetClubByIdAsync(id);
            if (club == null)
            {
                return NotFound();
            }
            return Ok(club);

        }


        [HttpPost]
        public async Task<IActionResult> PostClubAsync([FromBody] Club club)
        {

            ClubService service = new();
            var isSuccessful = await service.InsertClubAsync(club);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> UpdateClubAsync(Guid id, [FromBody] ClubUpdate club)
        {
            ClubService service = new();
            var isSuccessful = await service.UpdateClubAsync(id, club);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClubAsync(Guid id)
        {
            ClubService service = new();
            var isSuccessful = await service.DeleteClubAsync(id);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }


        

    }
}
