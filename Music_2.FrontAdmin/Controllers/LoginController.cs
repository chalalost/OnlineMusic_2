using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Music_2.ApiIntegration;
using Music_2.Data.Models;
using Music_2.Data.Models.Utils;
using Music_2.FrontAdmin.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.FrontAdmin.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        public LoginController(IUserApiClient userApiClient, IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "");
                return View(request);
            }
            var token = await _userApiClient.Authenticate(request);
            if (token.ResultObj == null)
            {
                ModelState.AddModelError("", token.Message);
                return View();
            }
            var userPrincipal = this.ValidateToken(token.ResultObj);
            var authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true
            };
            HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId, _configuration[SystemConstants.AppSettings.DefaultLanguageId]);
            HttpContext.Session.SetString(SystemConstants.AppSettings.Token, token.ResultObj);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipal,
                authProperties
                );
            return RedirectToAction("Index", "Home");
        }
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;
            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.ValidateLifetime = true;
            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);
            return principal;
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPasswordConfirmation(ForgotPassword forgotPassword)
        {
            return View(forgotPassword);
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordConfirmation(InputModel Input)
        {
            if (ModelState.IsValid)
            {
                // Tìm user theo email gửi đến
                var kq = await _userApiClient.GetTokenForgotPass(Input);

                /*var callbackUrl = Url.Action(
                    "/Login/ResetPasswordConfirm",
                    pageHandler: null,
                    values: new { email = Input.Email, token = kq.ResultObj },
                    protocol: Request.Scheme);*/
                var callbackUrl = Url.Action("ForgotPasswordConfirmation", "Login",
                    new { email = Input.Email, token = kq.ResultObj }, Request.Scheme
                    );
                var str = "Forgot Password Confirmation";
                await new EmailSender().SendEmailAsync(Input.Email, str, callbackUrl);
                return RedirectToAction("Text");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPasswordConfirmation(string email, string token, string newpassword)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            else
            {
                if (email == null || token == null || newpassword == null)
                {
                    return View();
                }
                var kq = await _userApiClient.ResetPasswordConfirm(email, token, newpassword);
                return RedirectToAction("Index");
            }
        }
    }
}
