using HospitalManagement.Helpers;
using HospitalManagement.Models;
using System.Data.SqlClient;

namespace HospitalManagement.DataAccess
{
    public class DashBoardDataAccess
    {
        public string ErrorMessage { get; private set; }
        public DashBoardDataModel GetAll()
        {
            try
            {

                ErrorMessage = String.Empty;
                ErrorMessage = "";
                var d = new DashBoardDataModel();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "select count(*) as PatientCount from Patient";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.PatientCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    sqlStmt = "select count(*) as VaccinatedCount from Vaccine where status = 'Vaccinated' ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.VaccinatedCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    sqlStmt = "select count(*) as CovidPositiveCount from Covid where Result = 'Positive' ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.CovidPositiveCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    sqlStmt = "select count(*) as CovidPositiveCount from Covid where Result = 'Negative' ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.CovidNegativeCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                }

                return d;


            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }
    }
}
