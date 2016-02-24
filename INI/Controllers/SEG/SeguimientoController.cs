using INI.Models;
using INI.Models.DataBase;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace INI.Controllers.SEG
{
    //[Authorize(Roles = "Administrator,Administrador")]
    [Authorize]
    public class SeguimientoController : Controller
    {
        
          public SeguimientoController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

          public SeguimientoController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        
        
        // GET: Seguimiento
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult RegistroAdmin()
        {

            if (AspNetUsersRoles.IsUserInRole("Administrador", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Coordinador", User.Identity.Name))
            {

                return View();
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistroAdmin(RegisterViewModel model, FormCollection form)
        {
            investicEntities db = new investicEntities();
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

                   

                    AspNetUserRoles asignRoleToUser = new AspNetUserRoles();
                    string roleSelected = form["SelectedUserRoleId"].ToString();
                    asignRoleToUser.RoleId = AspNetRoles.GetRoleId(roleSelected);
                    asignRoleToUser.UserId = AspNetUsers.GetUserId(model.UserName);
                    db.AspNetUserRoles.Add(asignRoleToUser);
                    db.SaveChanges();


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