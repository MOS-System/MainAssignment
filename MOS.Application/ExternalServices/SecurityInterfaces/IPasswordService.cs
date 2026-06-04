using System;
using System.Collections.Generic;
using System.Text;


namespace MOS.Application.ExternalServices.SecurityInterfaces

{
    public interface IPasswordService
    {
        // TODO: HashPassword - takes plain password, returns hash
        // TODO: VerifyPassword - takes plain password and hash, returns bool
        // TODO: GenerateRandomPassword - returns random password string
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
        string GenerateRandomPassword();
    }
}
