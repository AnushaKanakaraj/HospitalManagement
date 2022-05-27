using HospitalManagement.DataAccess;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Medicines
{
    [Authorize(Roles = "Admin")]
    public class ListModel : PageModel
    {
        public List<MedicineDataModel> Medicines { get; set; }
        public void OnGet()
        {
            var medicineDataAccess=new MedicineDataAccess();
            Medicines = medicineDataAccess.GetAll();
        }
    }
}
