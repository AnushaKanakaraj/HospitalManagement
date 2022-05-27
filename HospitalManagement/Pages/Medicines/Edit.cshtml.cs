using HospitalManagement.DataAccess;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Pages.Medicines
{
   
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public int Id { get; set; }
        [BindProperty]
        [Display(Name ="Status")]
        public string Status { get; set; }
        public List<SelectListItem> Statuses { get; set; }

        [BindProperty]
        [Display(Name = "Vaccine Name")]
        public string VaccineName { get; set; }
        
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        private List<SelectListItem> GetStatus()
        {
            var SelectItem = new List<SelectListItem>();
            SelectItem.Add(new SelectListItem { Text = "Available", Value = " Available" });
            SelectItem.Add(new SelectListItem { Text = "Not Available", Value = "Not Available" });
           
            return SelectItem;
        }
        public void OnGet(int id)
        {
            Statuses = GetStatus();
            Id = id;
            if (id <= 0)
            {
                ErrorMessage = $"{Id} Not Found";
                return;
            }
            var medicineDataAccess = new MedicineDataAccess();
            var m = medicineDataAccess.GetMedicineById(id);
            if(m != null)
            {
                VaccineName = m.VaccineName;
                Status = m.Status;
            }
            else
            {
                ErrorMessage = "No Records found with Id";
            }
        }
        public void OnPost()
        {
            Statuses= GetStatus();
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid data. Please Try Again";
                return;
            }
            var medicineDataAccess = new MedicineDataAccess();
            var medicineToUpdate = new MedicineDataModel
            {
                Id = Id,
                VaccineName = VaccineName,
                Status = Status,
            };
            var updMedicine = medicineDataAccess.Update(medicineToUpdate);
            if (updMedicine != null)
            {
                SuccessMessage = $"{updMedicine.Id} Updated successfully";
            }
            else
            {
                ErrorMessage = $"Updating {updMedicine.Id} failed.{medicineDataAccess.ErrorMessage}";
            }
        }
    }
}
