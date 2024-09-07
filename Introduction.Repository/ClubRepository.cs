using Introduction.Model;
using Introduction.Repository.Common;
using Npgsql;

namespace Introduction.Repository
{
    public class ClubRepository: IClubRepository
    {

        private const string connectionString = "Host=localhost:5432;Username=postgres;Password=postgres;Database=WebDatabase";


        public bool DeleteClub(Guid id)
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
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public bool InsertClub(Club club)
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


        public bool UpdateClub(Guid id, ClubUpdate club)
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
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<Club> GetAllClubs()
        {
            try
            {
                List<Club> clubs = new List<Club>();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Club\";";

                using var command = new NpgsqlCommand(commandText, connection);

                connection.Open();

                using NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Club club = new Club();
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
                        club.ClubPresidentId = Guid.TryParse(reader["ClubPresidentId"].ToString(), out var result) ? result : null;

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

        public Club GetClubById(Guid id)
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
                    
                    club.ClubPresidentId = Guid.TryParse(reader["ClubPresidentId"].ToString(), out var result) ? result : null;
                    

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
