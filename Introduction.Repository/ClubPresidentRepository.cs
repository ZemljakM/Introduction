using Introduction.Model;
using Introduction.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Repository
{
    public class ClubPresidentRepository: IClubPresidentRepository
    {

        private const string connectionString = "Host=localhost:5432;Username=postgres;Password=postgres;Database=WebDatabase";


        public async Task<List<ClubPresident>> GetAllClubPresidentsAsync()
        {
            try
            {
                List<ClubPresident> clubPresidents = new List<ClubPresident>();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"ClubPresident\";";

                using var command = new NpgsqlCommand(commandText, connection);

                connection.Open();

                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        ClubPresident clubPresident = new ClubPresident();

                        clubPresident.Id = Guid.Parse(reader["Id"].ToString());
                        clubPresident.FirstName = reader["FirstName"].ToString();
                        clubPresident.LastName = reader["LastName"].ToString();
                        clubPresidents.Add(clubPresident);
                    }
                }
                else
                {
                    return null;
                }
                return clubPresidents;


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ClubPresident> GetClubPresidentByIdAsync(Guid id)
        {
            try
            {
                ClubPresident clubPresident = new ClubPresident();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT c.\"Name\", c.\"Sport\", c.\"Id\" as \"ClubId\", cp.\"Id\", cp.\"FirstName\", cp.\"LastName\" " +
                    "FROM \"ClubPresident\" cp LEFT JOIN \"Club\" c " +
                    "ON c.\"ClubPresidentId\" = cp.\"Id\" WHERE cp.\"Id\" = @id;";

                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        Club club = new Club();
                        

                        clubPresident.Id = Guid.Parse(reader["Id"].ToString());
                        clubPresident.FirstName = reader["FirstName"].ToString();
                        clubPresident.LastName = reader["LastName"].ToString();

                        club.Id = Guid.TryParse(reader["ClubId"].ToString(), out var result) ? result: Guid.Empty;
                        club.Name = reader["Name"].ToString();
                        club.Sport = reader["Sport"].ToString();
                        clubPresident.Clubs.Add(club);
                        
                    }


                }
                else
                {
                    return null;
                }
                return clubPresident;


            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<bool> InsertClubPresidentAsync(ClubPresident clubPresident)
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

                var numberOfCommits = await command.ExecuteNonQueryAsync();

                connection.Close();

                if (numberOfCommits == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
