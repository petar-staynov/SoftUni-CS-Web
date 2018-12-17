using System.Reflection;

namespace TORSHIA.Data
{
    public class Connection
    {
        private static readonly string DbName = "TORSHIA";

        public static string ConnectionConfig()
        {
            return $"Server=.;Database={DbName};Integrated Security=True;";
        }
    }
}