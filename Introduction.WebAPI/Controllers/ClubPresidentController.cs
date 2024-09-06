using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Introduction.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubPresidentController : ControllerBase
    {
        private const string connectionString = "Host=localhost:5432;" +
                "Username=postgres;" +
                "Password=postgres;" +
                "Database=WebDatabase";


        [HttpPost]
        public IActionResult PostClub([FromBody] ClubPresident clubPresident)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                string commandText = "INSERT INTO \"ClubPresident\" VALUES (@id, @firstName, @lastName);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@firstName", clubPresident.FirstName);
                command.Parameters.AddWithValue("@lastName", clubPresident.LastName);

                connection.Open();

                var numberOfCommits = command.ExecuteNonQuery();

                connection.Close();

                if (numberOfCommits == 0)
                {
                    return NotFound();
                }
                return Ok("Successfully added.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
