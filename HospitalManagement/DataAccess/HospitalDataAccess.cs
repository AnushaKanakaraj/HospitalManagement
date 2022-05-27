using HospitalManagement.Helpers;
using HospitalManagement.Models;
using System.Data.SqlClient;

namespace HospitalManagement.DataAccess
{
    public class HospitalDataAccess
    {
        public string ErrorMessage { get; set; }
        public List<HospitalDataModel> GetAll()
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                List<HospitalDataModel> Hospitals = new List<HospitalDataModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select Id,Name,Location,PhoneNumber from dbo.Hospital";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            while (Reader.Read() == true)
                            {
                                HospitalDataModel hospital = new HospitalDataModel();
                                hospital.Id = Reader.GetInt32(0);
                                hospital.Name = Reader.GetString(1);
                                hospital.Location = Reader.GetString(2);
                                hospital.PhoneNumber = Reader.GetString(3);

                                Hospitals.Add(hospital);

                            }
                        }
                    }
                }
                return Hospitals;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //Get by id
        public HospitalDataModel GetById(int id)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                HospitalDataModel Hospital = new HospitalDataModel();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select Id,Name,Location,PhoneNumber, from dbo.Hospital where Id={id} ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            while (Reader.Read() == true)
                            {
                                HospitalDataModel hospital = new HospitalDataModel();
                                hospital.Id = Reader.GetInt32(0);
                                hospital.Name = Reader.GetString(1);
                                hospital.Location = Reader.GetString(2);
                                hospital.PhoneNumber = Reader.GetString(3);

                                

                            }
                        }
                    }
                }
                return Hospital;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}
