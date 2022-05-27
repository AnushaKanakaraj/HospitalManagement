using HospitalManagement.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Pages.Vaccine
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty, Required]
        [Display(Name = "Status")]
        public string Status { get; set; }
        public List<SelectListItem> Statuses { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        private List<SelectListItem> GetStatus()
        {
            var selectItem = new List<SelectListItem>();
            selectItem.Add(new SelectListItem { Text = "Vaccinated", Value = "Vaccinated" });
            selectItem.Add(new SelectListItem { Text = "Not vaccinated", Value = "Not Vaccinated" });

            return selectItem;
        }
        public void OnGet()
        {
            Statuses = GetStatus();
        }
        public void OnPost()
        {
            Statuses = GetStatus();

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data. Please try again.";
                return;
            }
            Statuses = GetStatus();
            var vaccineDataAccess = new VaccineDataAccess();
            var result = vaccineDataAccess.Update(Id, Status);

            if (result)
            {
                SuccessMessage = "Result Updated Successfully";
                ErrorMessage = "";
            }
            else
            {
                ErrorMessage = $"Error! Updating Result - {vaccineDataAccess.ErrorMessage}";
                SuccessMessage = "";
            }
        }
    }
}
