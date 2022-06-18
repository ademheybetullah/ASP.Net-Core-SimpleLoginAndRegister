using BackendTask.Concrete;
using BackendTask.EmailSender;
using BackendTask.Identity;
using BackendTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackendTask.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        
        public AccountController(IEmailSender emailSender, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _emailSender = emailSender;
            this._userManager = userManager;
            this._signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel register)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    RegistrationDate=DateTime.Now,
                    UserName=register.Name,
                    Name=register.Name,
                    Surname=register.Surname,
                    Email = register.Email
                };
                Random rastgele = new Random();
                int confirmCode = rastgele.Next(1000, 10000);
                user.ConfirmCode = confirmCode;
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    await _emailSender.SendEmailAsync(user.Email, "Hesap Onayı", $"Hesap onay kodnuz:{confirmCode} kodu girmek için <a href='https://{HttpContext.Request.Host}/Account/ConfirmEmail'> tıklayınız <a/>");

                    return RedirectToAction("ConfirmEmail", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "An unknown error occurred, please try again");
                    return View(register);
                }

            }
            return View(register);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            DateTime Date1 = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (result.Succeeded)
            {
                DateTime Date2 = DateTime.Now;
                var mSecconds = (Date2 - Date1).TotalMilliseconds;
                user.IsOnline = true;
                await _userManager.UpdateAsync(user);
                var contextOptions = new DbContextOptionsBuilder<IdentityContext>().UseSqlServer(@"data source=(localdb)\MSSQLLocalDB; initial catalog=DBBackendTask; integrated security=true;").Options;
                using (var context = new IdentityContext(contextOptions))
                {
                    var login = new Login
                    {
                        LoginDate = Date1,
                        LoginTime = Convert.ToInt32(mSecconds)
                    };
                    context.Add(login);
                    context.SaveChanges();
                }
                return base.RedirectToAction("Home", "User");

            }
            ModelState.AddModelError("", "Email or password is incorrect");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(int code,string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View();
            }
            else
            {
                if (user.ConfirmCode == code)
                {
                    user.EmailConfirmed = true;
                    user.EmailConfirmDate = DateTime.Now;
                    var userRole = await _roleManager.FindByNameAsync("User");
                    if (userRole == null)
                    {
                        await _roleManager.CreateAsync(new IdentityRole
                        {
                            Name = "User"
                        });
                    }
                    await _userManager.AddToRoleAsync(user,userRole.Name);
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "verification code is wrong");
                    return View();
                }
            }
        }
        [HttpGet]
        public IActionResult ConfirmEmail()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            user.IsOnline = false;
            await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email cannot be empty");
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email not found");
                return View();
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = code
            });
            await _emailSender.SendEmailAsync(email, "Şifre Değişikliği", $"Yeni şifre oluşturmak için aşağıdaki linke <a href='https://{HttpContext.Request.Host}{url}'> tıklayınız <a/>");
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string userId,string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("register", "Account");
            }
            var model = new ResetPasswordModel
            {
                Token = token
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("register", "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("login", "Account");
            }
            return View(model);
        }
    }
}

