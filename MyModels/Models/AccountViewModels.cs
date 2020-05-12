using System;
using System.Collections.Generic;

namespace MyModels.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Email { get; set; }

        //public bool HasRegistered { get; set; }

        //public string LoginProvider { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsRegistered { get; set; }
        public long JoinDate { get; set; }
        public string Image { get; set; }
        public decimal Credit { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
