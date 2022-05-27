using System.Data.SqlClient;

namespace HospitalManagement.Helpers
{
    public class DataBase
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=ENWIN-525\\SQLEXPRESS;Initial Catalog=HospitalDB;Integrated Security=True;";
            return new SqlConnection(connectionString);
        }

    }
}
