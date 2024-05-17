namespace Models.Models
{
    public class Category
	{
		public int CategoryID { get; set; }
		public String CategoryName { get; set; }

		//Relations

		public List<Product> Products { get; set; }

	}
}
