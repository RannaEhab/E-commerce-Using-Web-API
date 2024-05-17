using DTOs.DTOs.OrderProducts;

namespace DTOs.Orders
{
	public class ReadAllOrdersDTO
	{
		public int OrderID { get; set; }
		public string OrderStatus { get; set; }
		public List<ReadAndCreateProductOrderDTO> Products { get; set; }
	}
}
