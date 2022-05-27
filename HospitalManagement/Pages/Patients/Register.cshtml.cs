using HospitalManagement.DataAccess;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Pages.Patient
{
    
    public class AddModel : PageModel
    {
        [BindProperty]
        [Display(Name="First Name")]
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [BindProperty]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [BindProperty]
        [Display(Name = "Gender")]
        [Required]
        public string Gender { get; set; }
        public string[] Genders = new[] { "Male", "Female", "Unspecified" };

        
        [BindProperty]
        [Display(Name = "Aadhar Number")]
        [Required]
        [MaxLength(12)]
        public string AadharNo { get; set; }
        
        [BindProperty]
        [Display(Name="Date Of Birth")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [BindProperty]
        [Display(Name = "Mobile Number")]
        [Required]
        [MaxLength(10)]
        public string MobileNumber { get; set; }

        [BindProperty]
        [Display(Name = "Street")]
        [Required]
        [MaxLength(30)]
        public string Street { get; set; }

        [BindProperty]
        [Display(Name = "City")]
        [Required]
        [MaxLength(30)]
        public string City { get; set; }

        [BindProperty]
        [Display(Name = "State")]
        [Required]
        [MaxLength(30)]
        public string State { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public void OnGet()
        {
            ErrorMessage = "";
            SuccessMessage = "";
            ModelState.Clear();
            Dob= DateTime.Now.AddYears(-50);
        }
        public  AddModel()
        {
            ErrorMessage = "";
            SuccessMessage = "";
            ModelState.Clear();
            Dob = DateTime.Now.AddYears(-50);

        }
        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = $"Invalid Data...";
                return;
            }
            var patientDataAccess = new PatientDataAccess();
            var newPatient = new PatientDataModel
            {
                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender,
                AadharNo = AadharNo,
                Dob=Dob,
                MobileNumber=MobileNumber,
                Street=Street,
                City=City,
                State=State
            };
            var insertedPatient=patientDataAccess.Insert(newPatient);
            if(insertedPatient != null)
            {
                SuccessMessage = $"'{insertedPatient.FirstName}' - Registered SuccessFully.....Login  Using your " +
                    $" Aadhar Number and Dob";
                ModelState.Clear();
            }
            else
            {
                ErrorMessage =$"Failed to Register  Please Try Again -{ patientDataAccess.ErrorMessage}" ;
            }
        }
    }
}
