using DTOs.Validations;

namespace DB.DTOs.DTOs.User
{
	public class CustomerRegisterDTO
	{
		public string UserName { get; set; }
		[EmailValidation]
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }
	}
}
