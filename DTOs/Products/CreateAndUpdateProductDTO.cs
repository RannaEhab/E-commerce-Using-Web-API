namespace DTOs.Products
{
    public class CreateAndUpdateProductDTO
	{
		public string ProductName { get; set; }
		public float Price { get; set; }
		public string? Description { get; set; }
		public int CategoryID { get; set; }

	}
}
