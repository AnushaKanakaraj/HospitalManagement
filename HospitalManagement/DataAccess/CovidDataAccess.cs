using HospitalManagement.Helpers;
using HospitalManagement.Models;
using System.Data.SqlClient;

namespace HospitalManagement.DataAccess
{
    public class CovidDataAccess
    {
        public string ErrorMessage { get; set; }
        public List<CovidDataModel> GetAll()
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                List<CovidDataModel> CovidPatients = new List<CovidDataModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"select c.Id as CovidId,p.Id as PatientId,h.Id as HospitalId,p.FirstName, " +
                                   " p.LastName,p.AadharNo,P.MobileNumber,p.Gender,p.Street,p.City,p.State ,h.Name as HospitalName, " +
                                      " c.AppointmentDate,c.Status,c.Result " +
                                      " from covid as c " +
                                       " inner join patient AS p ON c.PatientId = p.Id " +
                                           " inner join hospital as h on c.HospitalId = h.Id ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            while (Reader.Read() == true)
                            {
                                CovidDataModel covid = new CovidDataModel();
                                covid.Id = Reader.GetInt32(0);
                                covid.PatientId = Reader.GetInt32(1);
                                covid.HospitalId = Reader.GetInt32(2);
                                covid.FirstName = Reader.GetString(3);
                                covid.LastName = Reader.GetString(4);
                                covid.AadharNo = Reader.GetString(5);
                                covid.MobileNumber = Reader.GetString(6);
                                covid.Gender = Reader.GetString(7);
                                covid.Street = Reader.GetString(8);
                                covid.City = Reader.GetString(9);
                                covid.State = Reader.GetString(10);
                                covid.HospitalName = Reader.GetString(11);
                                covid.AppointmentDate = Reader.GetDateTime(12);
                                covid.Status = Reader.GetString(13);
                                covid.Result = Reader.IsDBNull(14) ? null : Reader.GetString(14);



                                CovidPatients.Add(covid);

                            }
                        }
                    }
                }
                return CovidPatients;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// insert
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool Update(int id ,  string result)
        {
            try
            {
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Covid SET  " +
                        $"Result = '{result}' " +
                        $"where Id = {id}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();
                        if (numOfRows > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return false;
        }
        //insert

        public bool Insert(int patientId, int hospitalId, DateTime appointmentDate)
        {
            try
            {
                ErrorMessage = string.Empty;
                int idInserted = 0;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Covid ( PatientId, HospitalId, AppointmentDate) VALUES ( {patientId}, {hospitalId}, '{appointmentDate.ToString("yyyy-MM-dd")}') ; SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
                return false;
            }
        }
        //Get By Id
        public CovidDataModel GetCovidResultBy(int id)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                CovidDataModel covid = new CovidDataModel();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"select p.FirstName, p.LastName,p.AadharNo,P.MobileNumber,p.Gender,p.Street,p.City,h.Name as HospitalName, " +
                                       " c.AppointmentDate,c.Result " +
                                       " from covid as c " +
                                        " inner join patient AS p ON c.PatientId = p.Id " +
                                            " inner join hospital as h on c.HospitalId = h.Id " +
                                            $" where p.Id = { id} ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            while (Reader.Read() == true)
                            {
                                
                                
                                
                                
                                covid.FirstName = Reader.GetString(0);
                                covid.LastName = Reader.GetString(1);
                                covid.AadharNo = Reader.GetString(2);
                                covid.MobileNumber = Reader.GetString(3);
                                covid.Gender = Reader.GetString(4);
                                covid.Street = Reader.GetString(5);
                                covid.City = Reader.GetString(6);
                                
                                covid.HospitalName = Reader.GetString(7);
                                covid.AppointmentDate = Reader.GetDateTime(8);
                               
                                covid.Result = Reader.IsDBNull(9) ? null : Reader.GetString(9);



                                

                            }
                        }
                    }
                }
                return covid;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //show button
        public bool IsPatientRegistered(int patientId)
        {
            try
            {
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"select count(1) from dbo.Covid where PatientId = {patientId} ";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = Convert.ToInt32(cmd.ExecuteScalar());
                        if (numOfRows > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return false;
        }

    }
}
