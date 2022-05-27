using HospitalManagement.DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Pages
{
    public class IndexModel : PageModel
    {
        public int PatientCount { get; set; }
        public int VaccinatedCount { get; set; }
        public int CovidPositiveCount { get; set; }
        public int CovidNegativeCount { get; set; }

        public string ErrorMessage { get; set; }
        [FromQuery(Name = "action")]
        public string Action { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if (!String.IsNullOrEmpty(Action) && Action.ToLower() == "logout")
            {
                Logout();
                return;
            }
            var dashBoardDataAccess = new DashBoardDataAccess();
            var dashboard = dashBoardDataAccess.GetAll();
            if (dashboard != null)
            {
                PatientCount = dashboard.PatientCount;
                VaccinatedCount = dashboard.VaccinatedCount;
                CovidPositiveCount = dashboard.CovidPositiveCount;
                CovidNegativeCount = dashboard.CovidNegativeCount;
            }
            else
            {
                ErrorMessage = $"No Dashboard Data Available - {dashBoardDataAccess.ErrorMessage}";
            }

        }
        public void OnPost()
        {
            Logout();
        }
        private void Logout()
        {
            HttpContext.SignOutAsync();
            Response.Redirect("/Index");
        }
    }
}