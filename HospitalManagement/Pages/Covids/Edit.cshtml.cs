using HospitalManagement.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Pages.Covids
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty, Required]
        [Display(Name = "Result")]
        public string Result { get; set; }
        public List<SelectListItem> Results { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        private List<SelectListItem> GetResult()
        {
            var selectItem = new List<SelectListItem>();
            selectItem.Add(new SelectListItem { Text = "Positive", Value = "Positive" });
            selectItem.Add(new SelectListItem { Text = "Negative", Value = "Negative" });
            
            return selectItem;
        }
        public void OnGet()
        {
            Results = GetResult();
        }
        public void OnPost()
        {
            Results = GetResult();

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data. Please try again.";
                return;
            }
            Results = GetResult();
            var covidDataAccess = new CovidDataAccess();
            var result = covidDataAccess.Update(Id, Result);

            if (result)
            {
                SuccessMessage = "Result Updated Successfully";
                ErrorMessage = "";
            }
            else
            {
                ErrorMessage = $"Error! Updating Grade - {covidDataAccess.ErrorMessage}";
                SuccessMessage = "";
            }
        }
    }
}
