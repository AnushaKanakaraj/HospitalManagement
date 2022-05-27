using HospitalManagement.DataAccess;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Patient
{
    [Authorize(Roles = "Admin")]
    public class ListModel : PageModel
    {
        
        public List<PatientDataModel>Patients { get; set; }
        public void OnGet()
        {
            var patientDataAccess=new PatientDataAccess();
            Patients = patientDataAccess.GetAll();
        }
        
    }
}
