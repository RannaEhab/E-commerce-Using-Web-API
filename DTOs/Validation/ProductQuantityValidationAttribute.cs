using System.ComponentModel.DataAnnotations;

namespace DTOs.Validation
{
	internal class ProductQuantityValidationAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value == null || !(value is int productQuantity))
			{
				return new ValidationResult("The product quantity must be provided and must be an integer.");
			}

			if (productQuantity <= 0)
			{
				return new ValidationResult("The product quantity must be a positive integer.");
			}

			return ValidationResult.Success;
		}
	}
}
