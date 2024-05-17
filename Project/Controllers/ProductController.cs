using DTOs.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace Project.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		IProductService productService;

		public ProductController(IProductService _productService)
		{
			productService = _productService;
		}
		// ___________________________  Search ___________________________
		[HttpGet]
		[Route("Search/{productName}")]
		[Authorize]
		public IActionResult Search(string productName)
		{
			var searchedProducts = productService.Search(productName);
			if (searchedProducts == null)
			{
				return Ok(searchedProducts);
			}
			else
			{
				return BadRequest("There is no products can be found");
			}
		}

        // ___________________________  GetAll ___________________________
        [HttpGet]
		[Route("GetAll")]
		[Authorize]
		public IActionResult GetAll(string? CategoryName)
		{
			if (CategoryName == null)
			{
				var Query = productService.GetAll();
				return Ok(Query);
			}
			else
			{
				var QueryGetAll = productService.GetByCategory(CategoryName);
				if (QueryGetAll.Count == 0)
				{
					return BadRequest();
				}
				return Ok(QueryGetAll);
			}
		}
		// ___________________________  GetByID ___________________________

		[HttpGet]
		[Route("{ID}")]
		[Authorize]
		public IActionResult GetByID(int ID)
		{
			var product = productService.GetByID(ID);
			if (product == null)
			{
				return NotFound();
			}

			return Ok(product);
		}
		// ___________________________  Create ___________________________
		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult CreateProduct(CreateAndUpdateProductDTO c)
		{
				var newProduct = productService.Create(c);
				if (newProduct)
					return Created();
				else
					return StatusCode(500, "Already Exists");	
		}
		// ___________________________  Update ___________________________
		[HttpPut]
		[Authorize(Roles = "Admin")]
		public IActionResult Update(int ID, CreateAndUpdateProductDTO u)
		{
			var updatedProduct = productService.Update(ID, u);
			if (updatedProduct)
			{
				return Ok(updatedProduct);
			}
			else
			{
				return BadRequest("The Product ID can't not found");
			}
		}
		// ___________________________  Delete ___________________________

		[HttpDelete]
		[Authorize(Roles = "Admin")]
		public IActionResult Delete(int ID)
		{
			var deletedProduct = productService.Delete(ID);
			if (deletedProduct)
			{
				return Ok("The Product was deleted successfully");
			}
			else
			{
				return NotFound("The Product ID can't not found");
			}
		}
	}
}
