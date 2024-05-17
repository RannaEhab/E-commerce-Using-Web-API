using System.ComponentModel.DataAnnotations;

namespace DTOs.Validations
{
	public class EmailValidationAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value == null)
			{
				return new ValidationResult("Email is required");
			}

			string email = value.ToString();
			if (email.Contains("@") && email.Contains(".com"))
			{
				return ValidationResult.Success;
			}
			else
			{
				return new ValidationResult("Email must contain both '@' and '.com'");
			}
		}
	}
}
