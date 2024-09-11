using Introduction.Common;
using Introduction.Model;
using Introduction.Service;
using Introduction.Service.Common;
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
        private IClubService _service;

        public ClubController(IClubService service) 
        {
            _service = service;
        }



        [HttpGet]

        public async Task<IActionResult> GetAllClubsAsync(string name = "", string sport = "", DateOnly? dateFrom = null,
            DateOnly? dateTo = null, int membersFrom = 0, int membersTo = 0, string president = "", int rpp = 3, 
            int pageNumber = 1, string orderBy = "FirstName", string orderDirection = "ASC")
        {
            Sorting sorting = new Sorting
            {
                OrderBy = orderBy,
                OrderDirection = orderDirection
            };

            Paging paging = new Paging
            {
                Rpp = rpp,
                PageNumber = pageNumber
            };

            ClubFilter filter = new ClubFilter
            {
                Name = name,
                Sport = sport,
                DateFrom = dateFrom,
                DateTo = dateTo,
                MembersFrom = membersFrom,
                MembersTo = membersTo,
                President = president
            };

            var clubs = await _service.GetAllClubsAsync(sorting, paging, filter);
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
            var club = await _service.GetClubByIdAsync(id);
            if (club == null)
            {
                return NotFound();
            }
            return Ok(club);

        }


        [HttpPost]
        public async Task<IActionResult> PostClubAsync([FromBody] Club club)
        {
            var isSuccessful = await _service.InsertClubAsync(club);
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
            var isSuccessful = await _service.UpdateClubAsync(id, club);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClubAsync(Guid id)
        {
            var isSuccessful = await _service.DeleteClubAsync(id);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }


        

    }
}
