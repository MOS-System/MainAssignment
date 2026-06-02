using MOS.Application.DTOs.Responses.Products;
using MOS.Application.DTOs.Responses.Users;
using MOS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Responses.Auth
{
    public class AuthResponse : UserResponse
    {
        public string Token { get; set; } = string.Empty;
        public List<ProductResponse> Products = new List<ProductResponse>();
    }
}
