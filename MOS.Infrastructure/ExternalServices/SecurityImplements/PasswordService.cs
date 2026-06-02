using MOS.Application.Services.Interfaces;
using MOS.Domain.Constants;
using BCrypt;

namespace MOS.Infrastructure.ExternalServices.Security.Implements
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