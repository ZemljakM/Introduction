using Autofac.Core;
using Introduction.Common;
using Introduction.Model;
using Introduction.Service;
using Introduction.Service.Common;
using Introduction.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using static System.Reflection.Metadata.BlobBuilder;


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


        [HttpGet]
        public async Task<IActionResult> GetAllClubPresidentsAsync(string searchQuery = "", int rpp = 3, int pageNumber = 1, string orderBy = "Id", string orderDirection = "DESC")
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

            ClubPresidentFilter filter = new ClubPresidentFilter
            {
                SearchQuery = searchQuery
            };
            var presidents = await _service.GetAllClubPresidentsAsync(sorting, paging, filter);
            if (presidents is null)
            {
                return NotFound();
            }
            return Ok(presidents);

        }


        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetClubPresidentByIdAsync(Guid id)
        {
            var president = await _service.GetClubPresidentByIdAsync(id);
            GetClubPresident clubPresident = new GetClubPresident();
            if (president != null)
            {
                clubPresident.Id = id;
                clubPresident.FirstName = president.FirstName;
                clubPresident.LastName = president.LastName;
                

                foreach(var club in president.Clubs)
                {
                    GetClub getClub = new GetClub
                    {
                        Id = club.Id,
                        Name = club.Name,
                        Sport = club.Sport
                    };
                    clubPresident.Clubs.Add(getClub);
                }

                return Ok(clubPresident);
            }
            
            return NotFound();

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


        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> UpdateClubPresidentAsync(Guid id, [FromBody] ClubPresidentUpdate presidentUpdate)
        {
            var clubPresident = new ClubPresident();

            clubPresident.FirstName = presidentUpdate.FirstName;
            

            var isSuccessful = await _service.UpdateClubPresidentAsync(id, clubPresident);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }

    }


}
