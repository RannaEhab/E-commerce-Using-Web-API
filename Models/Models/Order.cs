using Context.Context;

namespace Models.Models
{
	public class Order
	{
		public int OrderID { get; set; }
		public string OrderStatus { get; set; }
		public string CustomerID { get; set; }

		//Relations
		public List<ProductOrder> Products { get; set; }
		public Customer Customer { get; set; }
	}
}
