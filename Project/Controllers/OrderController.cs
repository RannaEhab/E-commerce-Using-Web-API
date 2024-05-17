using DTOs.DTOs.Orders;
using DTOs.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using System.Security.Claims;

namespace Project.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private IOrderService orderService;
		public OrderController(IOrderService _orderService)
		{
			orderService = _orderService;
		}

		// ___________________________  Get All ___________________________
		[HttpGet]
		[Route("GetAll")]
		[Authorize]
		public IActionResult GetAll()
		{
			var roleClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
			if (roleClaims == null)
			{
				return Unauthorized("Role claim not identified in user credentials!");
			}
			string userRole = roleClaims.Value;

			string userID = null;
			if (userRole == "User")
			{
				var userIDClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DenyOnlySid);
				if (userIDClaims == null)
				{
					return Unauthorized("User ID claim not identified in user credentials!");
				}
				userID = userIDClaims.Value;
			}

			var orders = orderService.GetAll(userID);

			if (orders != null)
			{
				return Ok(orders);
			}
			else
			{
				return BadRequest();
			}
		}

		// ___________________________  Get By ID ___________________________
		[HttpGet("{ID:int}")]
		[Authorize]
		public IActionResult GetByID(int ID)
		{
			var roleClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
			if (roleClaims == null)
			{
				return Unauthorized("Role claim not identified in user credentials!");
			}

			string userRole = roleClaims.Value;
			ReadAllOrdersDTO order;

			if (userRole == "User")
			{
				var userClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DenyOnlySid);
				if (userClaims == null)
				{
					return Unauthorized("User ID claim not identified in user credentials!");
				}
				string userID = userClaims.Value;
				order = orderService.GetByID(ID, userID);
			}
			else if (userRole == "Admin")
			{
				order = orderService.GetByID(ID, null);
			}
			else
			{
				return Unauthorized("This user isn't authorized!");
			}

			if (order != null)
			{
				return Ok(order);
			}
			else
			{
				return BadRequest("The Order ID can't be found");
			}
		}

		// ___________________________  Create ___________________________
		[HttpPost]
		[Authorize(Roles = "User")]
		public IActionResult Create(CreateOrderDTO orderDTO)
		{
			var customerID = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DenyOnlySid).Value;
			var createOrder = orderService.Create(orderDTO, customerID);

			return Ok(customerID);
		}
		// ___________________________ 3- Update ___________________________
		[HttpPut("{ID:int}")]
		[Authorize(Roles = "Admin")]
		public IActionResult Update(int orderID, UpdateOrderDTO u)
		{
			var updatedOrder = orderService.Update(orderID, u);
			if (updatedOrder)
			{
				return Ok(updatedOrder);
			}
			else
			{
				return BadRequest("The Order ID can't not found");
			}
		}

		// ____________________________ 4- Delete ____________________________
		[HttpDelete("{ID:int}")]
		[Authorize]
		public IActionResult Delete(int ID)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return Unauthorized("This user is not authorized!");
			}

			var roleClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
			if (roleClaims == null)
			{
				return Unauthorized("Role claim not identified in user credentials!");
			}
			string userRole = roleClaims.Value;
			string? userID = null;
			if (userRole == "User")
			{
				var userClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DenyOnlySid);
				if (userClaims == null)
				{
					return Unauthorized("User ID claim can't be found in user claims");
				}
				userID = userClaims.Value;
			}

			bool deletedOrder =  orderService.Delete(ID, userID);
			if (!deletedOrder)
			{
				return BadRequest("The Order ID can't be found");
			}
			else
			{
				return Ok("The Order was deleted successfully");
			}
		}


	}
}
