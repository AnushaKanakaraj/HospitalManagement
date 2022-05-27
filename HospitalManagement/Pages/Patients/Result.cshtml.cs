using HospitalManagement.DataAccess;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Patients
{
    [Authorize(Roles = "User")]
    public class ResultModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public CovidDataModel result { get; set; }
        public void OnGet()
        {
            var covidDataAccess = new CovidDataAccess();
            result = covidDataAccess.GetCovidResultBy(Id);

        }
    }
}
