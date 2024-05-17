using Models.Models;

namespace Repositories.Contract
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
		Order GetOrderByID(int ID);
    }
}
