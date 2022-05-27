using HospitalManagement.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Pages.Patients
{
    [Authorize(Roles = "User")]
    public class CovidRegisterModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int PatientId { get; set; }

        [BindProperty]
        [Display(Name = "Hospital Name")]
        public int HospitalId { get; set; }

        [BindProperty, Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }
        public List<SelectListItem> HospitalList { get; set; }

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
        public void OnGet()
        {
            HospitalList = GetHospital();
            AppointmentDate = DateTime.Now;
        }
        public void OnPost()
        {

            HospitalList = GetHospital();
            //validation
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Register failed...Try Again";
            }

            var covidDataAccess = new CovidDataAccess();
            var result = covidDataAccess.Insert( PatientId, HospitalId,AppointmentDate);
            //Check Result
            if (result)
            {
                SuccessMessage = "Successfully Register!";
                ErrorMessage = "";
            }
            else
            {
                ErrorMessage = $"Error registering  for covid test - {covidDataAccess.ErrorMessage}";
                SuccessMessage = "";
            }
           
        }
        public void AddModel()
        {
            AppointmentDate = DateTime.Now;
        }
    }
}
