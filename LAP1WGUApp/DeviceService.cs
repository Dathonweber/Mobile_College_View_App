using System.IO;

namespace LAP1WGUApp
{
    public class DeviceService
    {

        private const string databaseName = "appDB.db";

        public static string StoragePath { get; set; }

        public static string GetSQLiteDatabasePath()
        {
            return Path.Combine(StoragePath, databaseName);
        }
    }
}
