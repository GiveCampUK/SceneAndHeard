using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace SceneAndHeard.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class UserRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public static List<UserRole> UserRolesData()
        {
            List<UserRole> RoleList = new List<UserRole>();
            int idCount = 1;
            foreach (string s in Roles.GetAllRoles())
            {
                UserRole UR = new UserRole();
                UR.Id = idCount++;
                UR.RoleName = s;

                RoleList.Add(UR);
            }

            return RoleList;
        }
    }


    public class UsersRoleMapping
    {
        [Required]
        [Display(Name = "Role name:")]
        public String RoleName { get; set; }
        [Required]
        [Display(Name = "User Name:")]
        public String UserName { get; set; }


        public static List<UsersRoleMapping> UsersInRole(String roleName)
        {

            List<UsersRoleMapping> map = new List<UsersRoleMapping>();
            foreach (var userinRole in Roles.GetUsersInRole(roleName))
            {
                map.Add(new UsersRoleMapping() { RoleName = roleName, UserName = userinRole });
            }

            return map;

        }

    }


    public class ManageRolesModel
    {
        [Required]
        [Display(Name = "New Role name:")]
        public string RoleName { get; set; }
    }


}
