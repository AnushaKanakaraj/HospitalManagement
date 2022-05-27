using HospitalManagement.DataAccess;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Covids
{
    [Authorize(Roles = "Admin")]
    public class ListModel : PageModel
    {
        public List<CovidDataModel> CovidPatients { get; set; }
        public void OnGet()
        {
            var covidDataAccess = new CovidDataAccess();
            CovidPatients = covidDataAccess.GetAll();
        }
    }
}
