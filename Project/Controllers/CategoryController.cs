using DTOs.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }
        // ___________________________  Search ___________________________
        [HttpGet("search/{categoryName}")]
        [Authorize]
        public IActionResult Search(string categoryName)
        {
            var categoriesSearch = categoryService.Search(categoryName);
            if (categoriesSearch != null)
            {
                return Ok(categoriesSearch);
            }
            else
            {
                return BadRequest("There is no categories can be found");
            }
        }

        // ___________________________  GetAll ___________________________
        [HttpGet]
        [Route("GetAll")]
        [Authorize]
        public IActionResult GetAll()
        {
            var QueryGetAll = categoryService.GetAll();
            if (QueryGetAll != null)
            {
                return Ok(QueryGetAll);
            }
            else
            {
                return BadRequest("There is an error in gitting data");
            }
        }
        // ___________________________  GetByID ___________________________

        [HttpGet]
        [Route("{ID}")]
        [Authorize]
        public IActionResult GetByID(int ID)
        {
            var category = categoryService.GetByID(ID);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        // ___________________________  Create ___________________________
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCategory(CreateAndUpdateCategoryDTO c)
        {
            var newcategory = categoryService.Create(c);
            if (newcategory)
            {
                return Ok("The category was created successfully");
            }
            else
            {
                return StatusCode(500, "Already Exists");
            }
        }
        // ___________________________  Update ___________________________
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int ID, CreateAndUpdateCategoryDTO u)
        {
            var updatedCategory = categoryService.Update(ID, u);
            if (updatedCategory)
            {
                return Ok(updatedCategory);
            }
            else
            {
                return BadRequest("The Category ID can't not found");
            }
        }
        // ___________________________  Delete ___________________________

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int ID)
        {
            var deletedCategory = categoryService.Delete(ID);
            if (deletedCategory)
            {
                return Ok("The Category was deleted successfully");
            }
            else
            {
                return NotFound("The Category ID can't not found");
            }
        }
    }
}
