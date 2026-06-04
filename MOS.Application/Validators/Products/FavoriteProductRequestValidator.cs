using FluentValidation;
using MOS.Application.DTOs.Requests.Products;

namespace MOS.Application.Validators.Products
{
    public class FavoriteProductRequestValidator
        : AbstractValidator<FavoriteProductRequest>
    {
        public FavoriteProductRequestValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Product ID is required.");
        }
    }
}