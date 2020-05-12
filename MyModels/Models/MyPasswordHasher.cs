using Microsoft.AspNet.Identity;


namespace Iplus
{
    public class MyPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword
            (string hashedPassword, string providedPassword)
        {
            if (hashedPassword == HashPassword(providedPassword))
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
    }
}