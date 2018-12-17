using System.Reflection;

namespace TORSHIA_NEW.Data
{
    public class Connection
    {
        private static readonly string DbName = Assembly.GetExecutingAssembly().GetName().Name;

        public static string ConnectionConfig()
        {
            return $"Server=.;Database={DbName};Integrated Security=True;";
        }
    }
}