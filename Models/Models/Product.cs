namespace Models.Models
{
    public class Product
	{
		public int ProductID { get; set;}
		public string ProductName { get; set; }
		public float Price { get; set; }
		public string ProductStatus { get; set; }
		public string? Description { get; set; }
		public int CategoryID { get; set; }

		//Relations
		public Category Category { get; set; }
		public List<ProductOrder> Products { get; set; }
		public string CustomerID { get; set; }
	}
}
