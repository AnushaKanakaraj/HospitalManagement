using HospitalManagement.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Pages.Patients
{
    [Authorize(Roles = "User")]
    public class VaccineRegistrationModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int PatientId { get; set; }

        [BindProperty]
        [Display(Name = "Hospital Name")]
        public int HospitalId { get; set; }

        [BindProperty]
        [Display(Name = "Medicine Name")]
        public int MedicineId { get; set; }

        [BindProperty, Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }
        public List<SelectListItem> HospitalList { get; set; }
        public List<SelectListItem> MedicineList { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        private List<SelectListItem> GetHospital()
        {
            var hospitalDataAccess = new HospitalDataAccess();
            var hospital = hospitalDataAccess.GetAll();
            var HospitalList = new List<SelectListItem>();

            foreach (var h in hospital)
            {
                HospitalList.Add(new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString()
                });
            }
            return HospitalList;
        }
        private List<SelectListItem> GetMedicine()
        {
            var medicineDataAccess = new MedicineDataAccess();
            var medicine = medicineDataAccess.GetAll();
            var MedicineList = new List<SelectListItem>();

            foreach (var m in medicine)
            {
                MedicineList.Add(new SelectListItem
                {
                    Text = m.VaccineName,
                    Value = m.Id.ToString()
                });
            }
            return MedicineList;
        }

        public void OnGet()
        {
            HospitalList = GetHospital();
            MedicineList = GetMedicine();
            AppointmentDate=DateTime.Now;
        }
        public void OnPost()
        {

            HospitalList = GetHospital();
            MedicineList = GetMedicine();
            //validation
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Register failed...Try Again";
            }

            var vaccineDataAccess = new VaccineDataAccess();
            var result = vaccineDataAccess.Insert(PatientId, HospitalId, MedicineId, AppointmentDate);
            //Check Result
            if (result!=null)
            {
                SuccessMessage = "Successfully Register! for Vaccination";
                ErrorMessage = "";
                
            }
            else
            {
                ErrorMessage = $"Error registering  for Vaccination - {vaccineDataAccess.ErrorMessage}";
                SuccessMessage = "";
            }
        }
    }
}
