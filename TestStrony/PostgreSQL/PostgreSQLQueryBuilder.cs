using TestStrony.Services;
using TestStrony.Helpers;

namespace TestStrony.PostgreSQL
{
    public class PostgreSQLQueryBuilder
    {
        public static string CreateTable(string name, List<string> collumnNames)
        {
            string createTableQueryTest = $"CREATE TABLE {name} (" +
                              "ID SERIAL PRIMARY KEY,";
            for (int i = 0; i < collumnNames.Count; i++)
            {
                createTableQueryTest += (i != collumnNames.Count - 1) ? $"\"{collumnNames[i].ToLower()}\" DOUBLE PRECISION, " : $"\"{collumnNames[i].ToLower()}\" DOUBLE PRECISION );";
            }
            return createTableQueryTest;
        }
        public static string AddValuesToQuery(string name, List<string> collumnNames, List<double> tableValues)
        {
            string AddValues = $"INSERT INTO {name} (";
            for (int j = 0; j < collumnNames.Count; j++)
            {
                AddValues += (j < collumnNames.Count - 1) ? $" {collumnNames[j].ToLower()}, " : $" {collumnNames[j].ToLower()} ) VALUES ";
            }
            for (int i = 0; i < 20; i++)
            {
                AddValues += $"( {20 - i}, ";
                for (int j = 0; j < 17; j++)
                {
                    AddValues += (j < 16) ? $"{tableValues[(i * 17) + j],2}, " : $"{tableValues[(i * 17) + j],2} ";

                }
                AddValues += (i < 19) ? " ), " : " ); ";
            }
            return AddValues;
        }
        public static List<string> GetRudolphPointsQuery(Dictionary<string, double> records, string url)
        {
            List<string> queries = new List<string>();
            int adder = 0;
            foreach (var (key, value) in records)
            {
                queries.Add($"SELECT punkty FROM {TextCrawler.GetTableName(url)} WHERE {key} >= {value} LIMIT 1;");
                adder++;
            }
            return queries;
        }
    }
}
