using HospitalManagement.Helpers;
using HospitalManagement.Models;
using System.Data.SqlClient;

namespace HospitalManagement.DataAccess
{
    internal class PatientDataAccess


    {
        public string ErrorMessage { get; private set; }
        //get all Patient Details
        public List<PatientDataModel> GetAll()
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                List<PatientDataModel> Patients = new List<PatientDataModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select Id,FirstName,LastName,Gender,AadharNo,Dob,MobileNumber,Street,City,State from dbo.Patient";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            while (Reader.Read() == true)
                            {
                                PatientDataModel patient = new PatientDataModel();
                                patient.Id = Reader.GetInt32(0);
                                patient.FirstName = Reader.GetString(1);
                                patient.LastName = Reader.GetString(2);
                                patient.Gender = Reader.GetString(3);
                                patient.AadharNo = Reader.GetString(4);
                                patient.Dob = Reader.GetDateTime(5);
                                patient.MobileNumber = Reader.GetString(6);
                                patient.Street = Reader.GetString(7);
                                patient.City = Reader.GetString(8);
                                patient.State = Reader.GetString(9);
                                Patients.Add(patient);

                            }
                        }
                    }
                }
                return Patients;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //Insert Patient:
        public PatientDataModel Insert(PatientDataModel newPatient)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Patient(FirstName,LastName,Gender,AadharNo,Dob,MobileNumber,Street,City,State) VALUES('{newPatient.FirstName}','{newPatient.LastName}','{newPatient.Gender}','{newPatient.AadharNo}','{newPatient.Dob.ToString("yyyy-MM-dd")}','{newPatient.MobileNumber}','{newPatient.Street}','{newPatient.City}','{newPatient.State}');SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newPatient.Id = idInserted;
                            return newPatient;
                        }
                    }

                }
                return null;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }


        }
        // Get profile By Aadhar and Dob
        public PatientDataModel GetProfileByAadhar(string aadhar,string dob)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                PatientDataModel patient = new PatientDataModel();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select Id,FirstName,LastName,Gender,AadharNo,Dob,MobileNumber,Street,City,State from dbo.Patient where AadharNo = '{aadhar}' and Dob = '{dob}' ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            if (Reader.Read() == true)
                            {
                                patient = new PatientDataModel();
                                patient.Id = Reader.GetInt32(0);
                                patient.FirstName = Reader.GetString(1);
                                patient.LastName = Reader.GetString(2);
                                patient.Gender = Reader.GetString(3);
                                patient.AadharNo = Reader.GetString(4);
                                patient.Dob = Reader.GetDateTime(5);
                                patient.MobileNumber = Reader.GetString(6);
                                patient.Street = Reader.GetString(7);
                                patient.City = Reader.GetString(8);
                                patient.State = Reader.GetString(9);
                                

                            }
                        }
                    }
                }
                return patient;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}
