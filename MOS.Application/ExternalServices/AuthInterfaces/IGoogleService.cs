using Microsoft.AspNetCore.Authentication;
using MOS.Application.DTOs.Responses.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.ExternalServices.AuthInterfaces
{
    public interface IGoogleService
    {
        Task<AuthResponse> HandleGoogleCompleteAsync(AuthenticateResult result);
    }
}
