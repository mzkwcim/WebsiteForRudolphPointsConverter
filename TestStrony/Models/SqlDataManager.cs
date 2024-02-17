using TestStrony.Helpers;
using TestStrony.PostgreSQL;
using Npgsql;

namespace TestStrony.Models
{
    public class SqlDataManager
    {
        public static void Connection(string name, List<string> collumnNames, List<double> tableValues)
        {
            string connectionString = "Host=localhost;Username=postgres;password=Mzkwcim181099!;Database=RudolphTable";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    if (TableExists(connection, name))
                    {
                        using (NpgsqlCommand command2 = new NpgsqlCommand(PostgreSQLQueryBuilder.AddValuesToQuery(name, collumnNames, tableValues), connection))
                        {
                            command2.ExecuteNonQuery();
                            Console.WriteLine("Dane zostały dodane do tabeli");
                        }
                    }
                    else
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand(PostgreSQLQueryBuilder.CreateTable(name, collumnNames), connection))
                        {
                            command.ExecuteNonQuery();
                            Console.WriteLine("Baza Utworzona");
                        }
                        using (NpgsqlCommand command2 = new NpgsqlCommand(PostgreSQLQueryBuilder.AddValuesToQuery(name, collumnNames, tableValues), connection))
                        {
                            command2.ExecuteNonQuery();
                            Console.WriteLine("Dane zostały dodane do tabeli");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public static bool TableExists(NpgsqlConnection connection, string tableName)
        {
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.Connection = connection;
                command.CommandText = $"SELECT EXISTS (SELECT FROM information_schema.tables WHERE table_name = @tableName)";
                command.Parameters.AddWithValue("@tableName", tableName.ToLower());
                object? result = command.ExecuteScalar();
                return result != null && (bool)result;
            }
        }
        public static string DataBaseConnection(string query, string key)
        {
            object value1;
            string odp = "";
            string connectionString = "Host=localhost;Username=postgres;password=Mzkwcim181099!;Database=RudolphTable";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                value1 = reader["punkty"];
                                odp = $"{value1}";
                            }
                        }
                        else
                        {
                            odp = "0";
                        }
                    }
                }
            }
            return odp;
        }
    }
}
