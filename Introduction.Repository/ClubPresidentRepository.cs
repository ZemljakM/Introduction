using Introduction.Common;
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


        public async Task<List<ClubPresident>> GetAllClubPresidentsAsync(Sorting sorting, Paging paging, ClubPresidentFilter filter)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                StringBuilder stringBuilder = new StringBuilder("SELECT * FROM \"ClubPresident\" ");
                using var command = new NpgsqlCommand();
                command.Connection = connection;

                if (!string.IsNullOrEmpty(filter.SearchQuery))
                {
                    stringBuilder.Append(" WHERE CONCAT(\"FirstName\", ' ', \"LastName\") ILIKE @searchQuery " +
                        "OR CONCAT(\"LastName\", ' ', \"FirstName\") ILIKE @searchQuery ");
                    command.Parameters.AddWithValue("@searchQuery", StringExtension.AddWildcardSuffix(filter.SearchQuery));
                }


                stringBuilder.Append($" ORDER BY \"{sorting.OrderBy}\" {sorting.OrderDirection} " +
                    $"OFFSET @offset ROWS FETCH NEXT @nextRows ROWS ONLY;" );
                command.Parameters.AddWithValue("@offset", paging.Rpp * (paging.PageNumber - 1));
                command.Parameters.AddWithValue("@nextRows", paging.Rpp);

                List<ClubPresident> clubPresidents = new List<ClubPresident>();

                command.CommandText = stringBuilder.ToString();
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


        public async Task<bool> UpdateClubPresidentAsync(Guid id, ClubPresident clubPresident)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                StringBuilder stringBuilder = new StringBuilder("UPDATE \"ClubPresident\" SET ");
                using var command = new NpgsqlCommand();
                command.Connection = connection;

                if (clubPresident.FirstName != null)
                {
                    stringBuilder.Append("\"FirstName\"=@firstName, ");
                    command.Parameters.AddWithValue("@firstName", clubPresident.FirstName);
                }

                if (clubPresident.LastName != null)
                {
                    stringBuilder.Append("\"LastName\"=@lastName, ");
                    command.Parameters.AddWithValue("@lastName", clubPresident.LastName);
                }


                stringBuilder.Length -= 2;
                stringBuilder.Append(" WHERE \"Id\" = @id;");
                command.Parameters.AddWithValue("@id", id);
                command.CommandText = stringBuilder.ToString();

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
