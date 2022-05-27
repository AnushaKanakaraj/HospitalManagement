using HospitalManagement.DataAccess;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Patients
{
    [Authorize(Roles = "User")]
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public string AadharNo { set; get; }

        [BindProperty]
        public string Dob { set; get; }

        public int PatientId { set; get; }

        public bool ShowTestButton { get; set; }

        public bool ShowVaccineButton { get; set; }

        public PatientDataModel Profile { get; set; }

        public ProfileModel()
        {
            ShowTestButton = true;
            ShowVaccineButton = true;
        }
        public void OnGet()
        {
            var aadharNo = User.FindFirst("AadharNo");
            AadharNo = aadharNo != null ? aadharNo.Value.ToString() : string.Empty;
            var dob = User.FindFirst("Dob");
            Dob = dob != null ? dob.Value.ToString() : string.Empty;
            var userId = User.FindFirst("UserId");
            PatientId = userId != null ? Convert.ToInt32(userId.Value.ToString()) : 0;

            //var aadharNo = HttpContext.Session.GetString("AadharNo");
            //AadharNo = aadharNo != null ? aadharNo : string.Empty;
            //var dob = HttpContext.Session.GetString("Dob");
            //Dob = dob != null ? dob : string.Empty;

            //var patientId = HttpContext.Session.GetInt32("PatientId");
            //PatientId = patientId != null ? patientId.Value : 0;

            var patientDataAccess = new PatientDataAccess();
            Profile = patientDataAccess.GetProfileByAadhar(AadharNo, Dob);

            var vaccineDataAccess = new VaccineDataAccess();
            ShowVaccineButton = !vaccineDataAccess.IsPatientRegistered(PatientId);
            //ShowTestButton = covidDataAccess.GetTestStatus(userId);


            var covidDataAccess = new CovidDataAccess();
            ShowTestButton = !covidDataAccess.IsPatientRegistered(PatientId);
        }

    }
}
