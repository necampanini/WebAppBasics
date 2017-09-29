using System.Web.Mvc;
using WebApplication.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Contracts.Services;
using Models.Auth;
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
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Email == "admin@admin.com" && model.Password == "123456")
            {
                //otherwise we go get from db. populate claims
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "Nick"),
                    new Claim(ClaimTypes.Email, "necampanini@gmail.com"),
                    new Claim(ClaimTypes.Country, "USA")
                }, "ApplicationCookie");

//                Request.GetOwinContext().Authentication.SignIn(identity);
                var owinContet = Request.GetOwinContext();
                var authManager = owinContet.Authentication;

                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            ModelState.AddModelError("", "Invalid email or password");
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