using MAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Net.Mail;
using System.Net;
using MAI.Models.DataBase;

namespace MAI.Controllers.Usuarios
{
    [Authorize]
    public class UsuarioController : Controller
    {

        public UsuarioController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public UsuarioController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/IniciarSesion
        [AllowAnonymous]
        public ActionResult IniciarSesion(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [AllowAnonymous]
        public ActionResult Prueba()
        {
            var usuario = AspNetUsers.GetUserId(User.Identity.Name);
            return View(usuario);
        }

        //
        // POST: /Account/IniciarSesion
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IniciarSesion(LoginViewModel model, string returnUrl)
        {
            investicEntities db = new investicEntities();

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    ViewBag.mensaje = "no es";

                    

                    if (AspNetUsersRoles.IsUserInRole("Editor", user.UserName))
                    {
                        return RedirectToAction("Editor", "Admin", new { nombreu = user.UserName });
                    }
                    else if (AspNetUsersRoles.IsUserInRole("Contratista", user.UserName))
                    {
                        return RedirectToAction("Contratista", "Admin", new { nombreu = user.UserName });
                    }
                    else if (AspNetUsersRoles.IsUserInRole("Maestro", user.UserName))
                    {
                        return RedirectToAction("Maestro", "Admin", new { nombreu = user.UserName });
                    }
                    else if (AspNetUsersRoles.IsUserInRole("Estudiante", user.UserName))
                    {
                        return RedirectToAction("Estudiante", "Admin", new { nombreu = user.UserName });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Usuario no registrado.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Registrar
        [AllowAnonymous]
        public ActionResult Registrar()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registrar(RegisterViewModel model)
        {
            model.UserName = model.PersonalID;

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Name = model.Name,
                    SureName = model.SureName,
                    PersonalID = model.PersonalID,
                    Genre = model.Genre,
                    Email = model.Mail,
                    PhoneNumber = model.Phone,
                    Address = model.Address,
                    BirthDay = model.BirthDay
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var m = new MailMessage(
                       new MailAddress("investicudenar@gmail.com", "Registro Plataforma Investic"),
                       new MailAddress(user.Email));
                    m.Subject = "Plataforma Investic - Confirmación de correo electronico";
                    m.Body =
                        string.Format(
                            "Estimado {0}<BR/>Gracias por su registro en la plataforma Investic,<BR/>Sus datos de acceso:<BR/>Usuario: " +
                            model.PersonalID + "<BR/>Contraseña: " + model.Password +
                            "<BR/>Por favor haga clic en el siguiente vinculo, para confirmar su registro: <a href=\"{1}\" title=\"Confirmación Usuario\">{1}</a>",
                            user.UserName,
                            Url.Action("ConfirmarCorreo", "Usuario", new { Token = user.Id, user.Email }, Request.Url.Scheme));
                    m.IsBodyHtml = true;
                    var smtp = new SmtpClient("smtp.gmail.com");
                    smtp.Credentials = new NetworkCredential("investicudenar@gmail.com", "Investic666");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                    //await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("EmailEnviado", "Usuario");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Confirmar(string Email)
        {
            AuthenticationManager.SignOut();
            ViewBag.Email = Email;
            return View();
        }

        // GET: /Account/ConfirmEmail

        public async Task<ActionResult> ConfirmarCorreo(string Token, string Email)
        {
            ApplicationUser user = UserManager.FindById(Token);
            if (user != null)
            {
                if (user.Email == Email)
                {
                    user.EmailConfirmed = true;
                    await UserManager.UpdateAsync(user);
                    await SignInAsync(user, isPersistent: false);
                    AuthenticationManager.SignOut();

                    return RedirectToAction("Confirmado", "Usuario", new { ConfirmedEmail = user.Email });
                }
                AuthenticationManager.SignOut();
                return RedirectToAction("Confirmar", "Usuario", new { user.Email });
            }
            AuthenticationManager.SignOut();
            return RedirectToAction("Confirmar", "Usuario", new { Email = "" });
        }

        public ActionResult Confirmado()
        {
            return RedirectToAction("IniciarSesion", "Usuario");
        }

        [AllowAnonymous]
        public ActionResult EmailEnviado()
        {
            return View();
        }

        //
        // GET: /Account/AdministrarCuenta
        public ActionResult AdministrarCuenta(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Su contraseña ha sido cambiada."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "Ha ocurrido un error."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("AdministrarCuenta");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AdministrarCuenta(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("AdministrarCuenta");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("AdministrarCuenta", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("AdministrarCuenta", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Salir()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                if (error.StartsWith("Name") && error.Contains("is already taken."))
                {
                    ModelState.AddModelError("", "El usuario ya existe");
                }
                else
                {
                    ModelState.AddModelError("", error);
                }
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Inicio");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }

}
