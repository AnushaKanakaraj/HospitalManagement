using HospitalManagement.DataAccess;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Vaccine
{
   
    public class ListModel : PageModel
    {
        
        public List<VaccineDataModel> VaccineRecords { get; set; }
        public void OnGet()
        {
            var vaccineDataAccess = new VaccineDataAccess();
            VaccineRecords = vaccineDataAccess.GetAll();
        }
    }
}
