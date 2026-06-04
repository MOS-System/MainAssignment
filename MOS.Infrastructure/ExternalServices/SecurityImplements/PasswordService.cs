using MOS.Domain.Constants;
using MOS.Application.ExternalServices.SecurityInterfaces;

namespace MOS.Infrastructure.ExternalServices.SecurityImplements
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public string GenerateRandomPassword()
        {
            return Guid.NewGuid().ToString("N")
                       .Substring(0, ValidationConstants.PasswordMinLength);
        }
    }
}