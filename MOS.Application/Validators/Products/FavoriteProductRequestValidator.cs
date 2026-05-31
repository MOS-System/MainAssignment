using MOS.Application.DTOs.Requests.Products;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;


namespace MOS.Application.Validators.Products
{
    public class AddFavoriteRequestValidator : AbstractValidator<FavoriteProductRequest>
    {
        public AddFavoriteRequestValidator()
        {
            // TODO: validate ProductId - greater than 0
        }
    }
}
