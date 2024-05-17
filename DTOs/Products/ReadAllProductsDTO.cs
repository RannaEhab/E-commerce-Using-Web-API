using DTOs.Categories;

namespace DTOs.Products
{
    public class ReadAllProductsDTO
	{
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public float Price { get; set; }
		public string? Description { get; set; }

		public ReadCategoryDTO ReadCategory { get; set; }	
	}
}
