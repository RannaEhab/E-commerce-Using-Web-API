namespace DTOs.Products
{
	public class ReadProductsByCategoryDTO
	{
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public float Price { get; set; }
		public string? Description { get; set; }
	}
}
