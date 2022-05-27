using HospitalManagement.Helpers;
using HospitalManagement.Models;
using System.Data.SqlClient;

namespace HospitalManagement.DataAccess
{
    public class MedicineDataAccess
    {
        public string ErrorMessage { get; set; }
        public MedicineDataModel Update(MedicineDataModel updStatus)
        { 
            try
            {
                
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Medicine SET " +

                        $"Status= '{updStatus.Status}' " +
                        $"where Id = {updStatus.Id}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();
                        if (numOfRows > 0)
                        {
                            return updStatus;
                        }
                    }
                }
                return updStatus;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }
        public List<MedicineDataModel> GetAll()
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                List<MedicineDataModel> Medicines = new List<MedicineDataModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select Id,VaccineName,Status from dbo.Medicine";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            while (Reader.Read() == true)
                            {
                                MedicineDataModel medicine = new MedicineDataModel();
                                medicine.Id = Reader.GetInt32(0);
                                medicine.VaccineName = Reader.GetString(1);
                                medicine.Status = Reader.GetString(2);
                               
                                Medicines.Add(medicine);

                            }
                        }
                    }
                }
                return Medicines;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //Get by id
        public MedicineDataModel GetMedicineById(int id)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                MedicineDataModel medicine = null;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select Id,VaccineName,Status from dbo.Medicine where Id={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            while (Reader.Read() == true)
                            {
                                medicine = new MedicineDataModel();
                                medicine.Id = Reader.GetInt32(0);
                                medicine.VaccineName = Reader.GetString(1);
                                medicine.Status = Reader.GetString(2);

                               

                            }
                        }
                    }
                }
                return medicine;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}
