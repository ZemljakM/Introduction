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

        public bool InsertClubPresident(ClubPresident clubPresident)
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
