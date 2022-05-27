using HospitalManagement.Helpers;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;

namespace HospitalManagement.DataAccess
{
    [Authorize(Roles = "User")]
    public class VaccineDataAccess
    {
        public string ErrorMessage { get; set; }

        public bool IsPatientRegistered(int patientId)
        {
            try
            {
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"select count(1) from dbo.Vaccine where PatientId = {patientId} ";

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
        public List<VaccineDataModel> GetAll()
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                List<VaccineDataModel> VaccineRecords = new List<VaccineDataModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"select v.Id as VaccineId,p.Id as PatientId,h.Id as HospitalId,p.FirstName, " + 
                                   " p.AadharNo,P.MobileNumber,p.Gender,p.State ,h.Name as HospitalName,m.VaccineName, " +
                                       " v.AppointmentDate, v.VaccinatedDate , v.Status " +
                                      " from vaccine as v " +
                                       " inner join patient AS p ON v.PatientId = p.Id " +
                                           " inner join hospital as h on v.HospitalId = h.Id " +

                                           " inner join medicine as m on v.MedicineId = m.Id " ;
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            while (Reader.Read() == true)
                            {
                                VaccineDataModel v = new VaccineDataModel();
                                v.Id = Reader.GetInt32(0);
                                v.PatientId = Reader.GetInt32(1);
                                v.HospitalId = Reader.GetInt32(2);
                                v.FirstName = Reader.GetString(3);
                                v.AadharNo = Reader.GetString(4);

                                v.MobileNumber = Reader.GetString(5);
                                v.Gender = Reader.GetString(6);
                                v.State = Reader.GetString(7);
                                v.HospitalName = Reader.GetString(8);
                                v.VaccineName = Reader.GetString(9);
                                v.AppointmentDate = Reader.GetDateTime(10);
                                v.VaccinatedDate = Reader.IsDBNull(11) ? null : Reader.GetDateTime(11);
                                v.Status = Reader.IsDBNull(12) ? null : Reader.GetString(12);
                          



                                VaccineRecords.Add(v);

                            }
                        }
                    }
                }
                return VaccineRecords;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //insert
        public bool Insert(int patientId, int hospitalId, int medicineId, DateTime appointmentDate)
        {
            try
            {
                ErrorMessage = string.Empty;
                int idInserted = 0;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Vaccine ( PatientId, HospitalId, MedicineId, AppointmentDate) VALUES ( {patientId}, {hospitalId}, {medicineId} , {appointmentDate.ToString("yyyy-MM-dd")}) ; SELECT SCOPE_IDENTITY();";

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
        //update
        public bool Update(int id, string status)
        {
            try
            {
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Vaccine SET Status = '{status}', " +
                        $"VaccinatedDate = GETDATE() " +
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
        //get by result by id
        public VaccineDataModel GetVaccineResultBy(int id)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                VaccineDataModel vaccine = new VaccineDataModel();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $" select p.FirstName,  " +
                                  " p.AadharNo,P.MobileNumber,p.Gender,p.State ,h.Name as HospitalName,m.VaccineName, " +
                                       " v.AppointmentDate, v.VaccinatedDate , v.Status " +
                                     " from vaccine as v " +
                                       " inner join patient AS p ON v.PatientId = p.Id " +
                                       "    inner join hospital as h on v.HospitalId = h.Id " +

                                         "  inner join medicine as m on v.MedicineId = m.Id "  +

                                         $"  where p.Id = {id} ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            while (Reader.Read() == true)
                            {

                                vaccine.FirstName = Reader.GetString(0);
                                vaccine.AadharNo = Reader.GetString(1);
                                vaccine.MobileNumber = Reader.GetString(2);
                                vaccine.Gender = Reader.GetString(3);
                                vaccine.State = Reader.GetString(4);
                                vaccine.HospitalName = Reader.GetString(5);
                                vaccine.VaccineName = Reader.GetString(6);
                                vaccine.AppointmentDate = Reader.GetDateTime(7);
                                vaccine.VaccinatedDate = Reader.IsDBNull(8) ? null : Reader.GetDateTime(8);
                                vaccine.Status = Reader.IsDBNull(9) ? null : Reader.GetString(9);


                            }
                        }
                    }
                }
                return vaccine;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}
