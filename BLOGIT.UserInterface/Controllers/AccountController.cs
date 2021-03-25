using BLOGIT.Models;
using BLOGIT.Repository;
using BLOGIT.UserInterface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace BLOGIT.UserInterface.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<BlogUser> _userMngr;
        private readonly SignInManager<BlogUser> _signInMngr;
        private readonly ILogger<AccountController> _logger;
        private readonly IImageProcessorService _imageServices;
        private readonly IMailService _mailService;

        public AccountController(UserManager<BlogUser> userManager, SignInManager<BlogUser> signInManager,
                                        ILogger<AccountController> logger, IImageProcessorService imageServices,
                                        IMailService mailService)
        {
            _userMngr = userManager;
            _signInMngr = signInManager;
            _logger = logger;
            _imageServices = imageServices;
            _mailService = mailService;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid) //if the modelstate returned is not valid
                return View(); //A view is returned showing the validation errors thrown by the models
            string imagePath = string.Empty;
            if (model.Image != null)
            {
                imagePath = _imageServices.ImageUpload(model);
            }

            if (await _userMngr.FindByEmailAsync(model.EmailAddress) != null)
            {
                //returns error message if email already exists in database
                ModelState.AddModelError("", "User with email already exists!");
                return View(model);
            }

            BlogUser user = new BlogUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.EmailAddress,
                UserName = model.UserName,
                ProfilePhoto = imagePath
            };

            try
            {
                var result = await _userMngr.CreateAsync(user, model.Password);
                //creates a user and sets password
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    return View(model);
                }
                await _userMngr.AddToRoleAsync(user, "Member");
                //assign member role to a new user by default

                //sends a congratulatory mail to the new user
                var mailDetails = new MailDetails
                {
                    MessageTitle = "CONGRATULATIONS",
                    MessageBody = $"Congratulations {user.UserName}, you are now a member of the elite community of readers and writers here at BLOGIT. Remember, Readers are leaders!!!",
                    Recievers = new List<string> { user.Email }
                };

                _mailService.SendMail(mailDetails);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            //signs in the new user and returns to the home index view
            await _signInMngr.PasswordSignInAsync(user, model.Password, false, false);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel model)
        {
            if (!ModelState.IsValid) //if the modelstate returned is not valid
                return View(); //A view is returned showing the validation errors thrown by the models

            var user = await _userMngr.FindByEmailAsync(model.EmailAddress); //The usermanager gets a user matching the specified username from the database
            if (user == null)
            {
                ModelState.AddModelError("", "Username or Password incorrect!");
                return View(model); //if no such user is found this error is throw back to the user interface at this same view
            }

            var result = await _signInMngr.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
            //usermanager checks if the password entered matches the password of the returned user
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password incorrect!");
                return View(model); //if it does not an error is thrown back to the UI
            }

            //when all entries are validated, the user existence is authenticated and the userId is returned to the given route
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            //Validates existense of user
            var user = await _userMngr.FindByEmailAsync(model.EmailAddress);
            if (user == null)
            {
                ModelState.AddModelError("", $"User with email address {model.EmailAddress} does not exist!");
                return View(model);
            }

            //Generates password recovery token
            var token = _userMngr.GeneratePasswordResetTokenAsync(user);

            //Generates an action link to route a user to a recovery page with token
            var resetLink = Url.Action("ResetPassword", "Account", new { token, user.Email }, Request.Scheme);

            //sends the reset link as a mail to the user
            var mailDetails = new MailDetails
            {
                MessageTitle = "PASSWORD RESET",
                MessageBody = $"Click the link to reset your password. {resetLink}",
                Recievers = new List<string> { user.Email }
            };

            try
            {
                _mailService.SendMail(mailDetails);
                ViewBag.SuccessMsg = "Email sent. Check your inbox";
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            //Validate the token and email returned
            if (token == null)
            {
                ViewBag.TokenError = "A valid token is required for this operation";
            }
            if (email == null)
            {
                ViewBag.EmailError = "A valid email address is required for this operation";
            }

            ViewBag.Email = email;
            ResetPasswordVM reset = new ResetPasswordVM { EmailAddress = email, Token = token };
            return View(reset);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            //checks if the modelstate is valid
            if (!ModelState.IsValid)
                return View(model);

            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match!");
                return View(model);
            }

            //gets the instance of the user to be updated
            var user = await _userMngr.FindByEmailAsync(model.EmailAddress);

            if (user == null)
            {
                ModelState.AddModelError("", $"Sorry! No user with {model.EmailAddress} was found.");
                return View(model);
            }

            //resets the password
            var result = await _userMngr.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }

            }

            //sign in the user and return a success message
            await _signInMngr.SignInAsync(user, false);

            return RedirectToAction("PasswordResetSuccess", "Account");
        }

        public IActionResult PasswordResetSuccess()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _signInMngr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}