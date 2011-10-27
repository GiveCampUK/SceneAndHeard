using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using SceneAndHeard.Models;

namespace SceneAndHeard.Controllers
{

    
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register
        [Authorize]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [Authorize]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    //We're going to be logged in already, so don't log in as the user
                    //FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("ManageRoles", "Account");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }



        [Authorize]
        [Authorize(Roles = "Admin")]
        public ActionResult ManageRoles()
        {
            ViewBag.Title = "Manage Roles";
            ViewData["RoleData"] = UserRole.UserRolesData();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ManageRoles(ManageRolesModel model)
        {
            if (!Roles.RoleExists(model.RoleName))
            {
                Roles.CreateRole(model.RoleName);
                ViewData["RoleData"] = UserRole.UserRolesData();
                model.RoleName = string.Empty;
            }

            return View();
        }


        
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteRole(string roleName)
        {
            ViewBag.RoleName = roleName;

            string delStatus = string.Empty;
            try
            {
                Roles.DeleteRole(roleName, true);

                delStatus = "Role " + roleName + " is successfully deleted.";
            }
            catch (Exception Ex)
            {
                delStatus = Ex.Message;
            }

            ViewBag.RoleDeleteResult = delStatus;

            return View();
        }


        
        [Authorize(Roles = "Admin")]
        public ActionResult ShowUsersInRole(string roleName)
        {
            ViewBag.RoleName = roleName;

            ViewData["RoleMappings"] = UsersRoleMapping.UsersInRole(roleName);

            var users = Membership.GetAllUsers();

            var list = new SelectList(users, "UserName", "UserName");
            ViewData["ExistingUsers"] = list;




            return View(new UsersRoleMapping() { RoleName = roleName });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ShowUsersInRole(UsersRoleMapping model)
        {
            if (!Roles.IsUserInRole(model.UserName, model.RoleName))
            {
                Roles.AddUserToRole(model.UserName, model.RoleName);
            }

            ViewData["RoleMappings"] = UsersRoleMapping.UsersInRole(model.RoleName);
            var users = Membership.GetAllUsers();
            var list = new SelectList(users, "UserName", "UserName");
            ViewData["ExistingUsers"] = list;

            return View(new UsersRoleMapping() { RoleName = model.RoleName });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUserFromRole(string roleName, string userName)
        {
            if (Roles.IsUserInRole(userName, roleName))
            {
                Roles.RemoveUserFromRole(userName, roleName);
            }
            return RedirectToAction("ShowUsersInRole", "Account", new { RoleName = roleName });

        }


        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
