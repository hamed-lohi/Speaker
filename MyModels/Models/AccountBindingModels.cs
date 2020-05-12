using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyModels.Models
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
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


        public int? Id { get; set; }
    }

    public class ResetPasswordBindingModelAdmin
    {
        [Required]
        public int Id { get; set; }

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

    public class PhoneNumberViewModel
    {
        [Required]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        
        //[Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

    }

    public class RegisterBindingModel
    {

        //[Required(AllowEmptyStrings = false, ErrorMessage = @"نام و نام خانوادگی را وارد کنید")]
        //public string FullName { get; set; }

        [StringLength(100, ErrorMessage = @"حداکثر طول نام 100 حرف می باشد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"نام را وارد کنید")]
        public string FName { get; set; }

        [StringLength(100, ErrorMessage = @"حداکثر طول نام خانوادگی 100 حرف می باشد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"نام خانوادگی را وارد کنید")]
        public string LName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "شماره موبایل")]
        [StringLength(10, ErrorMessage = @"تعداد ارقام شماره موبایل باید 10 عدد باشد.", MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        //[Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = @"طول کلمه عبور نباید از 6 کاراکتر کمتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "پسورد")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تایید پسورد")]
        [Compare("Password", ErrorMessage = @" کلمه عبور با تکرار آن متفاوت می باشد")]
        public string ConfirmPassword { get; set; }

    }

    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
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

    public class LoginViewModel
    {
        //[Required]
        [Display(Name = "PhoneNumber")]
        //[Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }


    public class AddUserInformationViewModel
    {

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "شماره موبایل")]
        [StringLength(10, ErrorMessage = @"تعداد ارقام شماره موبایل باید 10 عدد باشد.", MinimumLength = 10)]
        public string UserPhoneNumber { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = @"نام را وارد کنید")]
        public string FName { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = @"نام خانوادگی را وارد کنید")]
        public string LName { get; set; }

        /// <summary>
        /// فایل تصویر
        /// </summary>
        public int? ImageFileId { get; set; }

        //[Required(AllowEmptyStrings = false,ErrorMessage = @"نام را وارد کنید")]
        //public string FullName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = @"ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = @"ایمیل وارد شده صحیح نیست")]
        public string Email { get; set; }

        /// <summary>
        /// نام مجموعه یا موسسه
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// نوع فعالیت مجموعه یا موسسه
        /// </summary>
        public string ActivityType { get; set; }

        /// <summary>
        /// شهر
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// آدرس
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// نام مسئول
        /// </summary>
        public string ResponsibleName { get; set; }

        /// <summary>
        /// شماره همراه مسئول
        /// </summary>
        public string ResponsibleMobile { get; set; }
        
        /// <summary>
        /// شماره تلفن
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// آدرس سایت
        /// </summary>
        public string SiteUrl { get; set; }

    }

    public class UserAccountBindingModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
