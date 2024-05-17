using DTOs.DTOs.Orders;
using DTOs.Orders;

namespace Services.Interface
{
    public interface IOrderService
    {
        public List<ReadAllOrdersDTO> GetAll(string? userID);
        ReadAllOrdersDTO GetByID(int orderID, string? userID);
        public bool Create(CreateOrderDTO orderDTO, string customerID);
        public bool Update(int orderID, UpdateOrderDTO u);
        public bool Delete(int orderID, string? userID);

    }
}
