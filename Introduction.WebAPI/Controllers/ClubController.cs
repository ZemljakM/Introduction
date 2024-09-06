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
        private const string connectionString = "Host=localhost:5432;Username=postgres;Password=postgres;Database=WebDatabase";


        [HttpGet]
        [Route("{id}")]

        public IActionResult Get(Guid id)
        {
            try
            {
                Club club = new Club();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Club\" WHERE \"Id\" = @id;";
                
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                using NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    club.Id = Guid.Parse(reader["Id"].ToString());
                    club.Name = reader["Name"].ToString();
                    club.Sport = reader["Sport"].ToString();

                    DateOnly? dateResult = null;
                    if (DateTime.TryParse(reader[3].ToString(), out DateTime dateTimeResult))
                    {
                        dateResult = DateOnly.FromDateTime(dateTimeResult);
                    }
                    club.DateOfEstablishment = dateResult;

                    club.NumberOfMembers = Int32.TryParse(reader[4].ToString(), out var numberResult) ? numberResult : null;
                    club.ClubPresidentId = Guid.Parse(reader["ClubPresidentId"].ToString());
                }
                if(club == null)
                {
                    return NotFound();
                }
                return Ok(club);


            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        public IActionResult PostClub([FromBody] Club club)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                /*string commandText = "INSERT INTO \"Club\" VALUES (@id, @name, @sport, @dateOfEstablishment, @numberOfMembers, @clubPresidentId);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@name", club.Name);
                command.Parameters.AddWithValue("@sport", club.Sport);
                command.Parameters.AddWithValue("@dateOfEstablishment", club.DateOfEstablishment);
                command.Parameters.AddWithValue("@numberOfMembers", club.NumberOfMembers);
                command.Parameters.AddWithValue("@clubPresidentId", club.ClubPresidentId);*/

                string commandText = "INSERT INTO \"Club\" VALUES (@id, @name, @sport, @dateOfEstablishment, @numberOfMembers);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@name", club.Name);
                command.Parameters.AddWithValue("@sport", club.Sport);
                command.Parameters.AddWithValue("@dateOfEstablishment", club.DateOfEstablishment);
                command.Parameters.AddWithValue("@numberOfMembers", club.NumberOfMembers);

                connection.Open();

                var numberOfCommits = command.ExecuteNonQuery();

                connection.Close();

                if(numberOfCommits == 0)
                {
                    return BadRequest("Invalid request.");
                }
                return Ok("Successfully added.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("{id}")]

        public IActionResult UpdateClub(Guid id, [FromBody] ClubUpdate club)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                string commandText = "UPDATE \"Club\" SET \"NumberOfMembers\" = @numberOfMembers WHERE \"Id\" = @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@numberOfMembers", club.NumberOfMembers);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                var numberOfCommits = command.ExecuteNonQuery();

                connection.Close();
                if (numberOfCommits == 0)
                {
                    return NotFound();
                }
                return Ok("Updated!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteClub(Guid id)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                string commandText = "DELETE FROM \"Club\" WHERE \"Id\" = @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                var numberOfCommits = command.ExecuteNonQuery();

                connection.Close();

                if (numberOfCommits == 0)
                {
                    return NotFound();
                }
                return Ok("Deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        

    }
}
