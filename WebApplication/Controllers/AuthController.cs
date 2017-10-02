using System.Web.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Contracts.Services;
using Models.Auth;
using Models.Entities;
using Models.Enums;

namespace WebApplication.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthenticationServices _authenticationServices;

        public AuthController(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _authenticationServices.LoginAttempt(model);

            if (user != null)
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Email), //replace with whatever 
                }, "ApplicationCookie");
             
                var owinContext = Request.GetOwinContext();
                var authManager = owinContext.Authentication;

                authManager.SignIn(identity);
                
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("LoginError", "Password and/or Username incorrect");
            
            return View(model);
        }
    
        public ActionResult Logout()
        {
            var owinContext = Request.GetOwinContext();
            var authManager = owinContext.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegistrationModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPasswordError", "Please match passwords");
                return View();
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Error with that email/password");
                return View();
            }

            var registrationStatus = await _authenticationServices.RegisterUser(model.Email, model.Password);

            switch (registrationStatus)
            {
                case RegistrationStatus.EmailAlreadyInUse:
                    ModelState.AddModelError("EmailInUse", "That email is already registered");
                    return View();
                case RegistrationStatus.Success:
                    return RedirectToAction("Index", "Home");
                case RegistrationStatus.ServerError:
                    ModelState.AddModelError("RegistrationError", "There was an error in registering");
                    return View();
                default:
                    ModelState.AddModelError("RegistrationError", "There was an error in registering");
                    return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //Helpers
        private string GetRedirectUrl(string modelReturnString)
        {
            if (string.IsNullOrEmpty(modelReturnString)
                || !Url.IsLocalUrl(modelReturnString))
            {
                return Url.Action("index", "home");
            }

            return modelReturnString;
        }
    }
}