using HospitalManagement.DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace HospitalManagement.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Display(Name ="UserName")]
        public string AadharNo { get; set; }
        [BindProperty]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Dob { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }



        public void OnGet()
        {
            ErrorMessage = "";
            SuccessMessage = "";
        }
        public async void OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Login or Password";
                return;
            }

            //Admin
            if (AadharNo == "admin" && Dob == "123456")
            {
                var userClaims = new List<Claim>()
                {
                    new Claim("UserId","0"),
                    new Claim(ClaimTypes.Name,"Administrator"),
                    new Claim(ClaimTypes.Role,"Admin")
                };
                var userIdentity = new ClaimsIdentity(userClaims, "user Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });

                await HttpContext.SignInAsync(userPrincipal);
                Response.Redirect("/Index");
                return;
            }

            //Patient
            var patientDataAccess = new PatientDataAccess();
            var patient = patientDataAccess.GetProfileByAadhar(AadharNo, Dob);

            if (patient != null && patient.Id > 0)
            {
                var userClaims = new List<Claim>()
                {
                    new Claim("UserId",patient.Id.ToString()),
                    new Claim(ClaimTypes.Name,patient.FirstName),
                    new Claim("AadharNo",patient.AadharNo),
                    new Claim("DOB",patient.Dob.ToString("yyyy-MM-dd")),
                    new Claim(ClaimTypes.Role,"User")
                };
                var userIdentity = new ClaimsIdentity(userClaims, "user Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });

                await HttpContext.SignInAsync(userPrincipal);

                HttpContext.Session.SetInt32("PatientId", patient.Id);
                HttpContext.Session.SetString("AadharNo", patient.AadharNo);
                HttpContext.Session.SetString("DOB", patient.Dob.ToString("yyyy-MM-dd"));

                Response.Redirect("/Patients/Profile");
                return;
            }
            SuccessMessage = "";
            ErrorMessage = "Invalid Login or Password";


        }
       
    }
}
