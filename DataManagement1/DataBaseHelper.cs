using System.Data.SQLite;

public class DataBaseHelper<T>
{
    public List<T> GetItems(string tableName, Func<SQLiteDataReader, T> mapFunction)
    {
        try
        {
            using (var connection = new SQLiteConnection("Data Source=DataBase.bd;Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($"SELECT * FROM {tableName};", connection))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        var items = new List<T>();
                        while (rdr.Read())
                        {
                            items.Add(mapFunction(rdr));
                        }

                        return items;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void InsertItem(string tableName, Dictionary<string, object> values)
    {
        try
        {
            using (var connection = new SQLiteConnection("Data Source=DataBase.bd;Version=3;"))
            {
                connection.Open();
                var columns = string.Join(", ", values.Keys);
                var parameters = string.Join(", ", values.Keys.Select(k => $"@{k}"));
                var query = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters});";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    foreach (var kvp in values)
                    {
                        cmd.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteItem(string tableName, string primaryKey, object primaryKeyValue)
    {
        try
        {
            using (var connection = new SQLiteConnection("Data Source=DataBase.bd;Version=3;"))
            {
                connection.Open();
                var query = $"DELETE FROM {tableName} WHERE {primaryKey} = @{primaryKey};";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue($"@{primaryKey}", primaryKeyValue);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}