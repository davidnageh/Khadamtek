using _mvcproject_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace _mvcproject_.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private IWebHostEnvironment env;
        public AccountController(UserManager<ApplicationUser> _userManager , SignInManager<ApplicationUser> _signInManager , IWebHostEnvironment hostingEnvironment)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            env = hostingEnvironment;
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(ProviderRegisterViewModel model)
        {
            string profilepath = null;
            if(model.profile != null)
            {
                var path_1 = Path.Combine(env.WebRootPath, "Images");
                profilepath = Guid.NewGuid().ToString() + "_" + model.profile.FileName;
                var filepath = Path.Combine(path_1, profilepath);
                using(var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    model.profile.CopyTo(fileStream);
                }
               
            }
            ApplicationUser apuser = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber,
                discriminator = 0,
                UserName = model.Email,
                NationalId = "00000000000000",
                IsAccepted = 1,
                ProfilePhoto = profilepath,
                latitude = model.latitude,
                longitude =model.longitude,
                //NationalIdPhoto= null,
               
            };
           
           var result = await userManager.CreateAsync(apuser, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(apuser, "CLIENT");
                await signInManager.SignInAsync(apuser, isPersistent: false);
                return RedirectToAction("login");
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            ClientLoginViewModel model = new ClientLoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(ClientLoginViewModel model)
        {
         var result = await signInManager.PasswordSignInAsync(model.UserName, model.password, false, false);
         var  user  = await userManager.FindByEmailAsync(model.UserName);
            if (result.Succeeded)
            {
                if (user.discriminator == 0)
                { if (user.FirstName == null)
                        return RedirectToAction("Edit", "Client");
                   return RedirectToAction("getall", "service");
                }
                      
                else if (user.discriminator == 2)
                    return RedirectToAction("Index", "Admin");
            }
            ModelState.AddModelError(string.Empty, result.ToString());
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("getall", "service");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
                                        new { returnUrl = returnUrl });

            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            ClientLoginViewModel clientLoginView = new ClientLoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", clientLoginView);
            }

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", clientLoginView);
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                                            info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email != null)
                {
                    var user = await userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                        };

                     
                        await userManager.CreateAsync(user);
                       await userManager.AddToRoleAsync(user, "CLIENT");
                    }

                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = $"Please contact support on admin@gmail.com";

                return View("Error");
            }
        }

    }
}
