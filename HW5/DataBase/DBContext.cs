using HW5.Domain;
using Newtonsoft.Json;

namespace HW5.DataBase
{
    public class DBContext<T> where T : class
    {
        public List<T>? db;
        private string? jsonFilePath;
        private string? jsonString;

        public DBContext()
        {
            string? projectPath = Directory.GetParent
                              (AppDomain.CurrentDomain.BaseDirectory)?
                              .Parent?.Parent?.Parent?.FullName;
            jsonFilePath = Path.Combine(projectPath, $"DataBase/{typeof(T).Name}.json");
            jsonString = File.ReadAllText(jsonFilePath);
            db = JsonConvert.DeserializeObject<List<T>>(jsonString);

        }

        public void SetData()
        {
            jsonString = JsonConvert.SerializeObject(db);
            File.WriteAllText(jsonFilePath, jsonString);
           
        }
    }
}
