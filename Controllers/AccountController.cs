using Humanizer;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using xilopro2.Data.Entities;
using xilopro2.Helpers.Interfaces;
using xilopro2.Models;
using static System.Net.Mime.MediaTypeNames;

namespace xilopro2.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserHelper _userHelper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
           private readonly IEmailSender _emailSender;
        private readonly IMailService _emailService;

        public AccountController(IUserHelper userHelper, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IEmailSender emailSender, IMailService emailService)
        {
            _userHelper = userHelper;
            _roleManager = roleManager;
            _userManager = userManager;
            _emailSender = emailSender;
            _emailService = emailService;
        }

        //inicio de sesion
        [HttpGet]
        public IActionResult Login()
        {
            // 
          //  var userEmailClaim = ((ClaimsIdentity)User.Identity).FindFirst("Email");
           // var user = _userManager.FindByEmailAsync(model.Username);
            if (User.Identity.IsAuthenticated )
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginViewModel());
        }
       

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Username);
                if (user != null) {
                    if (user.User_Status)
                    {
                        Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                        if (result.Succeeded)
                        {
                            // El usuario está desactivado, no permitir el inicio de sesión.
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos.");
                        }
                    }
                    else
                    {
                        //  if (Request.Query.Keys.Contains("ReturnUrl")){  return Redirect(Request.Query["ReturnUrl"].First()); }
                        ModelState.AddModelError(string.Empty, "Su cuenta ha sido desactivada. Contacte al administrador.");
                        return View(model);
                       // return RedirectToAction("Login", "Account");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El usuario no existe. Contacte al administrador.");
                    return View(model);
                   // return RedirectToAction("Login", "Account");
                } 
            }
            else
            {
                return View(model);
            }
           // return View(model);
            return RedirectToAction("Login", "Account");
        }


        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");

        }


        //Roles

        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(IdentityRole model)
        {
            try
            {
                if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
                }
                TempData["successRole"] = "Rol " + model.Name + " creado!!";
                return RedirectToAction("ListRoles");
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(string id, string name)
        {

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            role.Name = name;
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            if (isExist)
            {
                TempData["warningRole"] = "El Rol " + role.Name + " ya existe!!";
                ViewBag.name = name;
                return View();
            }
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                TempData["successRole"] = "Rol " + role.Name + " editado!!";
                return RedirectToAction(nameof(ListRoles));
            }
            return View();
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View(role);
        }

        [HttpPost]
        [ActionName("DeleteRole")]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["successRole"] = "Rol " + role.Name + " eliminado!!";
                return RedirectToAction(nameof(ListRoles));
            }
            return View();
        }

        //Metodos de REcuperacion o reseteo de contraseña


        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(OlvidoPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByEmailAsync(model.Email);

                if (usuario is null)
                {
                    ModelState.AddModelError(string.Empty, "El email o usuario no se encuentra registrado");
                    return View(model);
                }

                var codigo = await _userManager.GeneratePasswordResetTokenAsync(usuario);

                var urlRetorno = Url.Action("ResetPassword", "Account", new { userId = usuario.Id, code = codigo }, HttpContext.Request.Scheme);

               // await _emailSender.SendEmailAsync(usuario.Email, "Crear Contraseña -  Sistema Xilotepelt", "Por favor recupere su contraseña en el siguiente enlace: <a style='border:1px solid #d9d9d9;padding:0.4rem 1rem;' href=\"" + urlRetorno + "\"> Crear Nueva Contraseña </a>");
                await _emailService.SendEmailServiceAsync(usuario.Email, "Crear Contraseña -  Sistema Xilotepelt", "Por favor recupere su contraseña en el siguiente enlace: <a style='text-align: center;text-decoration:none;text-transform:uppercase;color: #fff;padding:12px 10px;background-color:#853aff;' href=\"" + urlRetorno + "\"> Crear Nueva Contraseña </a>");


                TempData["successAccount"] = "Por favor verifique su email para recuperar su contraseña.";
                return RedirectToAction("Login");
            }


            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByEmailAsync(model.Email);

                if (usuario is null)
                {
                    ModelState.AddModelError(string.Empty, "El email o usuario no se encuentra registrado");
                    return View(model);
                }

                var result = await _userManager.ResetPasswordAsync(usuario, model.Code, model.Password);

                if (result.Succeeded)
                {
                    var urlRetorno = Url.Action("Login", "Account", null, HttpContext.Request.Scheme);

                    // await _emailSender.SendEmailAsync(usuario.Email, "Crear Contraseña - Sistema Xilotepelt", "Su contraseña ha sido cambiada exitosamente, puede ingresar a travéz del siguiente enlace: <a style='border:1px solid #d9d9d9;padding:0.4rem 1rem;' href=\"" + urlRetorno + "\"> enlace </a>");
                    await _emailService.SendEmailServiceAsync(usuario.Email, "Crear Contraseña - Sistema Xilotepelt", "Su contraseña ha sido cambiada exitosamente, puede ingresar a travéz del siguiente enlace: <a style='text-align:center;text-decoration:none;text-transform:uppercase;min-width:130px;color: #fff;padding:12px 10px;font-weight:bold;font-size:20px;cursor:pointer;border-radius:6px;background-color:#3a86ff;' href=\"" + urlRetorno + "\"> Click Aqui </a>");
                    TempData["successAccount"] = "Contraseña ha sido cambiada exitosamente";
                    return RedirectToAction("Login");
                }


                ValidarErrores(result);

            }


            return View(model);
        }


        private void ValidarErrores(IdentityResult resultado)
        {
            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }


    }
}
