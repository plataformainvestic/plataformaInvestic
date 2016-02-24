using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SEG.Models;
using SEG.Models.DataBase;
using System.Linq;



namespace SEG.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
       
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Cedula, model.Password);
                if (user != null)
                {
                    if (user.ConfirmedEmail)
                    {
                        await SignInAsync(user, model.RememberMe);
                        return RedirectToLocal(returnUrl);
                    }
                    ModelState.AddModelError("", "Confirme Correo Electronico.");
                }
                else
                {
                    ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos");
                }
            }
            return View(model);
        }

        //
        // GET: /Account/Register
        [Authorize(Roles = "admin,cordinador")]
        [AllowAnonymous]
        public ActionResult Register()
        {

            Entities db = new Entities();

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());
            //equipos eq  = db.equipos.Where(r => r.Id_Coordinador.Equals(currentUser.Id));
            var equip = db.equipos.Where(x => x.Id_Coordinador.Equals(currentUser.Id));
            var carge = db.cargos.ToList();

            ViewBag.Equipo = new SelectList(equip, "Id_Equipo", "Nombre_Equipo");
            ViewBag.Cargo = new SelectList(carge, "Id_Cargo", "Nombre_Cargo");

            if (User.IsInRole("contratista"))
             {
                return RedirectToLocal("/Home/SinAutorizar");
             }
            if (User.IsInRole("coordinador"))
            {
                Entities db2 = new Entities();
                
                var manager2 = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                ApplicationUser currentUser2 = manager.FindById(User.Identity.GetUserId());
                //equipos eq  = db.equipos.Where(r => r.Id_Coordinador.Equals(currentUser.Id));
                var algo = db2.equipos.Where(x => x.Id_Coordinador.Equals(currentUser2.Id));
                var carg = db2.cargos.ToList();

                ViewBag.Equipo = new SelectList(algo, "Id_Equipo", "Nombre_Equipo");
                ViewBag.Cargo = new SelectList(carg, "Id_Cargo", "Nombre_Cargo");
               

                

            }
                
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var db = new ApplicationDbContext();
                if(User.IsInRole("admin"))
                {

                    var user = new ApplicationUser()
                    {
                        Cedula = model.Cedula,
                        Nombres = model.Nombres,
                        Apellidos = model.Apellidos,
                        Celular = model.Celular,
                        Email = model.Email,
                        Cargo = model.Cargo,
                        Contrato = model.Contrato,
                        Cdp = model.Cdp,
                        Equipo = model.Equipo,
                        LastName = model.Nombres + " " + model.Apellidos
                       
                    };

                    user.Cargo = "coordinador";
                    user.Contrato = "123";
                    user.Cdp = "123";
                    user.Equipo = "coordinador";
                    
                    
                    user.UserName = model.Cedula;
                    user.ConfirmedEmail = true;
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);

                        ApplicationUser currentUser = UserManager.FindByName(user.UserName);

                        IdentityResult roleresult = UserManager.AddToRole(currentUser.Id, "coordinador");

                        await SignInAsync(user, false);

                        AuthenticationManager.SignOut();

                        return RedirectToAction("Confirma", "Home");
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }






                if (User.IsInRole("coordinador"))
                {
                    var user = new ApplicationUser
                    {
                        Cedula = model.Cedula,
                        Nombres = model.Nombres,
                        Apellidos = model.Apellidos,
                        Celular = model.Celular,                     
                        Contrato = model.Contrato,
                        Cargo= model.Cargo,
                        Cdp = model.Cdp,
                        Equipo = model.Equipo,
                        Email = model.Email,
                        Fecha_IniContrato = model.Fecha_IniContrato,
                        Fecha_FinContrato = model.Fecha_FinContrato,
                        LastName = model.Nombres + " " + model.Apellidos
                    };
                    
                    user.UserName = model.Cedula;
                    user.ConfirmedEmail = false;
                    model.Password = GeneratePassword.GetUniqueKey(8);
                    

                    IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var m = new MailMessage(
                            new MailAddress("investicudenar@gmail.com", "Registro Plataforma Investic"),
                            new MailAddress(user.Email));
                        m.Subject = "Plataforma Investic - Confirmación de correo electronico";
                        m.Body =
                            string.Format(
                                "Estimado {0}<BR/>Gracias por su registro en la plataforma Investic,<BR/>Sus datos de acceso:<BR/>Usuario: " +
                                model.Cedula + "<BR/>Contraseña: " + model.Password +
                                "<BR/>Por favor haga clic en el siguiente vinculo, para confirmar su registro: <a href=\"{1}\" title=\"User Email Confirm\">{1}</a>",
                                user.UserName,
                                Url.Action("ConfirmEmail", "Account", new { Token = user.Id, user.Email }, Request.Url.Scheme));
                        m.IsBodyHtml = true;
                        var smtp = new SmtpClient("smtp.gmail.com");
                        smtp.Credentials = new NetworkCredential("investicudenar@gmail.com", "Investic666");
                        smtp.EnableSsl = true;
                        smtp.Send(m);

                        ApplicationUser currentUser = UserManager.FindByName(user.UserName);

                        IdentityResult roleresult = UserManager.AddToRole(currentUser.Id, "contratista");

                        await SignInAsync(user, false);

                        

                        AuthenticationManager.SignOut();

                        return RedirectToAction("Confirm", "Account", new { user.Email });
                    }


                    AddErrors(result);
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Confirm(string Email)
        {
            AuthenticationManager.SignOut();
            ViewBag.Email = Email;
            return View();
        }

        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string Token, string Email)
        {
            ApplicationUser user = UserManager.FindById(Token);
            if (user != null)
            {
                if (user.Email == Email)
                {
                    user.ConfirmedEmail = true;
                    await UserManager.UpdateAsync(user);
                    // await SignInAsync(user, isPersistent: false);
                    AuthenticationManager.SignOut();

                    return RedirectToAction("Confirma", "Home", new {ConfirmedEmail = user.Email});
                }
                AuthenticationManager.SignOut();
                return RedirectToAction("Confirm", "Account", new {user.Email});
            }
            AuthenticationManager.SignOut();
            return RedirectToAction("Confirm", "Account", new {Email = ""});
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result =
                await
                    UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                        new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new {Message = message});
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess
                    ? "Tu contraseña ha cambiado"
                    : message == ManageMessageId.SetPasswordSuccess
                        ? "Tu contraseña se ha actulizado."
                        : message == ManageMessageId.RemoveLoginSuccess
                            ? "El acceso externo ha sido removido"
                            : message == ManageMessageId.Error
                                ? "Ocurrió un error"
                                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result =
                        await
                            UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                                model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new {Message = ManageMessageId.ChangePasswordSuccess});
                    }
                    AddErrors(result);
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
                    IdentityResult result =
                        await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new {Message = ManageMessageId.SetPasswordSuccess});
                    }
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider,
                Url.Action("ExternalLoginCallback", "Account", new {ReturnUrl = returnUrl}));
        }

        //
        // GET: /Account/ExternalLoginCallback


        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback


        //
        // POST: /Account/ExternalLoginConfirmation

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Salir()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            IList<UserLoginInfo> linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return PartialView("_RemoveAccountPartial", linkedAccounts);
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
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            ClaimsIdentity identity =
                await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = isPersistent}, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
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
                var properties = new AuthenticationProperties {RedirectUri = RedirectUri};
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