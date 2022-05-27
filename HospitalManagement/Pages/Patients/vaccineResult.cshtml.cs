using HospitalManagement.DataAccess;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Patients
{
    [Authorize(Roles = "User")]
    public class vaccineResultModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public VaccineDataModel Vaccine { get; set; }
        public void OnGet()
        {
            var vaccineDataAccess = new VaccineDataAccess();
            Vaccine = vaccineDataAccess.GetVaccineResultBy(Id);

        }
    }
}
