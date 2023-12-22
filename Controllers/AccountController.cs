//using Microsoft.AspNetCore.Mvc;
using Azure;
using FoodDonationAPI.Common;
using FoodDonationAPI.Helpers;
using FoodDonationAPI.Models;
using FoodDonationAPI.Services;
using FoodDonationAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
//using System.Web.Http;

namespace FoodDonationAPI.Controllers
{
    //[RoutePrefix("api/Account")]
    //[Route("api/[Account]")] If we use like this, we can't change the method names in the Route. By default method names will be shown in swagger
    //Eg: We have given Index1 in the route
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IUserService _userService;
        private readonly ITest1 _test;

        public AccountController(UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHasher,
            IConfiguration configurationRoot, IUserService userService, ITest1 test)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _configurationRoot = (IConfigurationRoot)configurationRoot;
            _userService = userService;
            _test = test;
        }

        //[Authorize]
        /// <summary>
        /// Registers the new user
        /// Add the roles
        /// Send the email
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser(ApplicationUser userModel)
        {
            if (ModelState.IsValid)
            {
                var userExists = await _userManager.FindByEmailAsync(userModel.Email);
                if (userExists != null)
                {
                    return BadRequest(new { StatusCode = HttpStatusCode.InternalServerError, message = "Email or UserName already exists" });
                }

                var user = new ApplicationUser
                {
                    //UserId = Guid.NewGuid(),
                    Id= Guid.NewGuid().ToString(),
                    UserName = userModel.Email,
                    Email = userModel.Email,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    PhoneNumber = userModel.PhoneNumber,
                    NormalizedEmail = userModel.Email.ToUpper(),
                    NormalizedUserName = userModel.Email.ToUpper(),
                    Status = 1,
                    CreatedDate = DateTime.UtcNow,
                    City=userModel.City,
                    State = userModel.State,
                    Country = userModel.Country,
                    IsNGO = userModel.IsNGO
                    //PasswordHash = userModel.PasswordHash
                };

                IdentityResult result = await _userManager.CreateAsync(user, userModel.PasswordHash);
                if (!result.Succeeded)
                    return BadRequest(new { StatusCode = HttpStatusCode.InternalServerError, message = "User creation failed! Please check user details and try again." });

                //Add Email
                string message = null;
                string subject = null;
                List<EmailAddress> aud = new List<EmailAddress>();
                List<EmailAddress> audBCC = new List<EmailAddress>();

                aud.Add(new EmailAddress { Email = "TestEmail01Dec@yopmail.com", Name = "TestEmail" });
                audBCC.Add(new EmailAddress { Email = "TestEmail01Dec@yopmail.com", Name = "TestEmail" });
                message = EmailTemplates.GetTestEmail();
                subject = "Account activation email";

                bool emailStatus = await SendEmail(subject, message, aud, audBCC);

                return Ok(new { Status = "Success", Message = "User created successfully!" });
                //Add Roles code
                
            }
            else
            {
                return BadRequest("Model Error");
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel loginDetails)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //check whether user is present or not
                    var userDetails = await _userManager.FindByEmailAsync(loginDetails.Email);
                    if (userDetails == null) { return BadRequest(new { StatusCode = HttpStatusCode.InternalServerError, message = "Please enter valid email address." }); }

                    //create the token
                    //Validate the password
                    if(_passwordHasher.VerifyHashedPassword(userDetails,userDetails.PasswordHash, loginDetails.Password) == PasswordVerificationResult.Success)
                    {
                        //Creating a new JWT token with 2FA status of the logged-in user set to false.
                        var jwtSecurityToken = _userService.GetUserToken(_configurationRoot, userDetails);
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                            expiration = jwtSecurityToken.ValidTo,
                            UserId = userDetails.Id,
                            FirstName = userDetails.FirstName,
                            LastName = userDetails.LastName
                        });
                    }
                    else
                    {
                        return BadRequest(new { StatusCode = HttpStatusCode.InternalServerError, message = "Invalid email address or password." });
                    }
                }
                catch(Exception ex)
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("Model Error");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Index1")]
        public IActionResult Index(string userName)
        {
            string userID = User.GetUserId();
            IEnumerable<UserDetailsModel> usrDetails = _test.Register(userName);
            return Ok(usrDetails);
            //return Ok("Test");
        }

        //Send Email for new client
        //Email not working, need to check
        private async Task<bool> SendEmail(string sub, string msgBody, List<EmailAddress> audience, List<EmailAddress> audienceBCC)
        {
            var apiKey = _configurationRoot["SendGridKey"];
            var FromEmail = _configurationRoot["FromEmail"];

            var sgClient = new SendGridClient(apiKey);
            var message = new SendGridMessage()
            {
                From = new EmailAddress(FromEmail, "Do not reply"),
                Subject = sub,
                HtmlContent = msgBody,
            };
            message.AddTos(audience);
            message.AddBccs(audienceBCC);
            try
            {
                var response = await sgClient.SendEmailAsync(message);
                return true;
            }
            catch
            {
                return false;
            }
        }





        //private readonly ITest _test;
        //private readonly ITest1 _test1;
        //private readonly UserManager<ApplicationUser> _userManager;
        //public AccountController(ITest test, ITest1 test1, UserManager<ApplicationUser> userManager)
        //{
        //    _test = test;
        //    _test1 = test1;
        //    _userManager = userManager;
        //}

        ////[Authorize]
        //[HttpGet]
        //[Route("Index1")]
        //public IActionResult Index()
        //{
        //    var res = _test.GetGuid();
        //    return Ok(res);
        //    //return Ok("Test");
        //}

        //[HttpGet]
        //[Route("Index2")]
        //public IActionResult Index1()
        //{
        //    var res = _test1.Testing();
        //    return Ok(res);
        //    //return Ok("Test");
        //}

        //[HttpPost]
        //[Route("Register")]
        //public async Task<IActionResult> RegisterUser(ApplicationUser userModel)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //IdentityResult userStatus = _test1.Register(userModel);
        //    //var emailExists = _userManager.FindByEmailAsync(userModel.Email);
        //    //IdentityResult userRegisterStatus = _userService.Register(userModel);
        //    var user = await _userManager.FindByEmailAsync(userModel.Email);
        //    if(user != null)
        //    {
        //        return Ok("success");
        //    }
        //    return Unauthorized();
        //    //}
        //    //else
        //    //{
        //    //    return BadRequest("Model Error");
        //    //}
        //}
    }
}
