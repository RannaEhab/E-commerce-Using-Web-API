using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repositories.Contract;

namespace Repositories.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        protected ECommerceWebsiteContext context { get; set; }
        public OrderRepository(ECommerceWebsiteContext context) : base(context)
        {
            this.context = context;
        }


		public Order GetOrderByID(int ID)
		{
			var order = context.Orders
				.Include(o => o.Products)
				.FirstOrDefault(o => o.OrderID == ID);
			return order;
		}
	}
}
