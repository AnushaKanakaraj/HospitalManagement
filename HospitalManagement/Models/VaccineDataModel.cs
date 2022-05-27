namespace HospitalManagement.Models
{
    public class VaccineDataModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int HospitalId { get; set; }
        public int MedicineId { get; set; }
        public string VaccineName { get; set; }
        public string FirstName { get; set; }
       
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string HospitalName { get; set; }

        public DateTime? VaccinatedDate { get; set; }
        public string State { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Status { get; set; }
       
        public string AadharNo { get; set; }
        
    }
}
