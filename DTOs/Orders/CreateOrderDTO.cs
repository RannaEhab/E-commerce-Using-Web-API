using DTOs.DTOs.OrderProducts;

namespace DTOs.Orders
{
    public class CreateOrderDTO
	{
		public List<ReadAndCreateProductOrderDTO> Products { get; set; }
	}
}
