﻿using Introduction.Common;
using Introduction.Model;
using Introduction.Repository.Common;
using Npgsql;
using System.Text;

namespace Introduction.Repository
{
    public class ClubRepository: IClubRepository
    {

        private const string connectionString = "Host=localhost:5432;Username=postgres;Password=postgres;Database=WebDatabase";


        public async Task<bool> DeleteClubAsync(Guid id)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                string commandText = "DELETE FROM \"Club\" WHERE \"Id\" = @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);

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



        public async Task<bool> InsertClubAsync(Club club)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                string commandText = "INSERT INTO \"Club\" VALUES (@id, @name, @sport, @dateOfEstablishment, @numberOfMembers);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@name", club.Name);
                command.Parameters.AddWithValue("@sport", club.Sport);
                command.Parameters.AddWithValue("@dateOfEstablishment", club.DateOfEstablishment);
                command.Parameters.AddWithValue("@numberOfMembers", club.NumberOfMembers);

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


        public async Task<bool> UpdateClubAsync(Guid id, ClubUpdate club)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                StringBuilder stringBuilder = new StringBuilder("UPDATE \"Club\" SET ");
                using var command = new NpgsqlCommand();
                command.Connection = connection;

                if(club.Name != null)
                {
                    stringBuilder.Append("\"Name\"=@name, ");
                    command.Parameters.AddWithValue("@name", club.Name);
                }

                if (club.Sport != null)
                {
                    stringBuilder.Append("\"Sport\"=@sport, ");
                    command.Parameters.AddWithValue("@sport", club.Sport);
                }

                if (club.DateOfEstablishment.HasValue)
                {
                    stringBuilder.Append("\"DateOfEstablishment\"=@dateOfEstablishment, ");
                    command.Parameters.AddWithValue("@dateOfEstablishment", club.DateOfEstablishment.Value);
                }

                if (club.NumberOfMembers != null)
                {
                    stringBuilder.Append("\"NumberOfMembers\"=@numberOfMembers, ");
                    command.Parameters.AddWithValue("@numberOfMembers", club.NumberOfMembers);
                }

                if (club.ClubPresidentId.HasValue)
                {
                    stringBuilder.Append("\"ClubPresidentId\"=@clubPresidentId, ");
                    command.Parameters.AddWithValue("@clubPresidentId", club.ClubPresidentId.Value);
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

        public async Task<List<Club>> GetAllClubsAsync(Sorting sorting, Paging paging, ClubFilter filter)
        {
            try
            {
                List<Club> clubs = new List<Club>();

                using var connection = new NpgsqlConnection(connectionString);
                StringBuilder stringBuilder = new StringBuilder("SELECT c.\"Name\", c.\"Sport\", c.\"DateOfEstablishment\", " +
                    "c.\"NumberOfMembers\", c.\"ClubPresidentId\", cp.\"Id\", cp.\"FirstName\", cp.\"LastName\" " +
                    "FROM \"Club\" c LEFT JOIN \"ClubPresident\" cp ON c.\"ClubPresidentId\" = cp.\"Id\" WHERE 1=1 ");
                using var command = new NpgsqlCommand();
                command.Connection = connection;

                if (filter != null)
                {
                    if (!string.IsNullOrEmpty(filter.Name))
                    {
                        stringBuilder.Append(" AND \"Name\" ILIKE @name ");
                        command.Parameters.AddWithValue("@name", StringExtension.AddWildcardBoth(filter.Name));
                    }
                    if (!string.IsNullOrEmpty(filter.Sport))
                    {
                        stringBuilder.Append(" AND \"Sport\" = @sport ");
                        command.Parameters.AddWithValue("@sport", filter.Sport);
                    }
                    if (filter.DateFrom != null)
                    {
                        stringBuilder.Append(" AND \"DateOfEstablishment\" > @dateFrom ");
                        command.Parameters.AddWithValue("@dateFrom", filter.DateFrom);
                    }
                    if (filter.DateTo != null)
                    {
                        stringBuilder.Append(" AND \"DateOfEstablishment\" < @dateTo ");
                        command.Parameters.AddWithValue("@dateTo", filter.DateTo);
                    }
                    if (filter.MembersFrom != 0)
                    {
                        stringBuilder.Append(" AND \"NumberOfMembers\" > @membersFrom ");
                        command.Parameters.AddWithValue("@membersFrom", filter.MembersFrom);
                    }
                    if (filter.MembersTo != 0)
                    {
                        stringBuilder.Append(" AND \"NumberOfMembers\" < @membersTo ");
                        command.Parameters.AddWithValue("@membersTo", filter.MembersTo);
                    }
                    if (!string.IsNullOrEmpty(filter.President))
                    {
                        stringBuilder.Append(" AND (CONCAT(\"FirstName\", ' ', \"LastName\") ILIKE @president " +
                        "OR CONCAT(\"LastName\", ' ', \"FirstName\") ILIKE @president) ");
                        command.Parameters.AddWithValue("@president", StringExtension.AddWildcardBoth(filter.President));
                    }
                }




                stringBuilder.Append($" ORDER BY \"{sorting.OrderBy}\" {sorting.OrderDirection} " +
                    $"OFFSET @offset ROWS FETCH NEXT @nextRows ROWS ONLY;");
                command.Parameters.AddWithValue("@offset", paging.Rpp * (paging.PageNumber - 1));
                command.Parameters.AddWithValue("@nextRows", paging.Rpp);

                command.CommandText = stringBuilder.ToString();
                connection.Open();

                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        Club club = new Club();
                        ClubPresident clubPresident = new ClubPresident();
                        club.Id = Guid.Parse(reader["Id"].ToString());
                        club.Name = reader["Name"].ToString();
                        club.Sport = reader["Sport"].ToString();

                        DateOnly? dateResult = null;
                        if (DateTime.TryParse(reader["DateOfEstablishment"].ToString(), out DateTime dateTimeResult))
                        {
                            dateResult = DateOnly.FromDateTime(dateTimeResult);
                        }
                        club.DateOfEstablishment = dateResult;

                        club.NumberOfMembers = Int32.TryParse(reader["NumberOfMembers"].ToString(), out var numberResult) ? numberResult : null;
                        clubPresident.Id = Guid.Parse(reader["Id"].ToString());
                        club.ClubPresidentId = clubPresident.Id;
                        clubPresident.FirstName = reader["FirstName"].ToString();
                        clubPresident.LastName = reader["LastName"].ToString();
                        club.ClubPresident = clubPresident;

                        clubs.Add(club);
                    }
                }
                else
                {
                    return null;
                }
                return clubs;


            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public async Task<Club> GetClubByIdAsync(Guid id)
        {
            try
            {
                Club club = new Club();
                ClubPresident clubPresident = new ClubPresident();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT c.\"Name\", c.\"Sport\", c.\"DateOfEstablishment\", c.\"NumberOfMembers\", c.\"ClubPresidentId\"," +
                    " cp.\"Id\", cp.\"FirstName\", cp.\"LastName\" FROM \"Club\" c LEFT JOIN \"ClubPresident\" cp " +
                    "ON c.\"ClubPresidentId\" = cp.\"Id\" WHERE c.\"Id\" = @id;";

                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    await reader.ReadAsync();

                    club.Id = Guid.Parse(reader["Id"].ToString());
                    club.Name = reader["Name"].ToString();
                    club.Sport = reader["Sport"].ToString();

                    DateOnly? dateResult = null;
                    if (DateTime.TryParse(reader["DateOfEstablishment"].ToString(), out DateTime dateTimeResult))
                    {
                        dateResult = DateOnly.FromDateTime(dateTimeResult);
                    }
                    club.DateOfEstablishment = dateResult;

                    club.NumberOfMembers = Int32.TryParse(reader["NumberOfMembers"].ToString(), out var numberResult) ? numberResult : null;
                    
                    clubPresident.Id = Guid.Parse(reader["Id"].ToString());
                    club.ClubPresidentId = clubPresident.Id;
                    clubPresident.FirstName = reader["FirstName"].ToString();
                    clubPresident.LastName = reader["LastName"].ToString();
                    club.ClubPresident = clubPresident;

                }
                if (club == null)
                {
                    return null;
                }
                return club;


            }
            catch (Exception ex)
            {
                return null;
            }

        }

        
    }
}
