using AutoMapper;
using DTOs.DTOs.Orders;
using DTOs.Orders;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repositories.Contract;
using Services.Interface;

namespace Services.Classes
{
    public class OrderService : IOrderService
	{
		protected IOrderRepository orderRepository;
		protected IProductRepository productRepository;
		protected IMapper mapper;
		public OrderService(IOrderRepository _orderRepository, IProductRepository _productRepository, IMapper _mapper)
		{
			orderRepository = _orderRepository;
			productRepository = _productRepository;
			mapper = _mapper;
		}
		// ___________________________ Get All Orders ___________________________

		public List<ReadAllOrdersDTO> GetAll(string? userID)
		{
			var orders = productRepository.GetAll();
			List<ReadAllOrdersDTO> ordersList;
			if (userID != null)
			{
				orders = orders.Where(o => o.CustomerID == userID);
			}
			var printOrders = orders
				.Include(o => o.Products)
				.Select(o => mapper.Map<ReadAllOrdersDTO>(o));
			ordersList = printOrders.ToList();
			return ordersList;

		}

		// ___________________________ Get Order By ID ___________________________
		public ReadAllOrdersDTO GetByID(int orderID, string? userID)
		{
			var order =  orderRepository.GetOrderByID(orderID);
			var mappedOrder = mapper.Map<ReadAllOrdersDTO>(order);
			if (userID == null)
			{
				return mappedOrder;
			}
			else
			{
				if (order != null && order.CustomerID == userID)
				{
					return mappedOrder;
				}
				else
				{
					return null;
				}
			}
		}
		// ___________________________ Create new Order ___________________________
		public bool Create(CreateOrderDTO orderDTO, string customerID)
		{
			foreach (var product in orderDTO.Products)
			{
				var getProduct = productRepository.GetProductByID(product.ProductID);
				if (getProduct == null) return false;
			}
			var newOrder = mapper.Map<Order>(orderDTO);
			newOrder.CustomerID = customerID;
			orderRepository.Create(newOrder);
			return true;
		}
		// ___________________________Update Order ___________________________
		public bool Update(int orderID, UpdateOrderDTO u)
		{
			var existingOrder = orderRepository.GetOrderByID(orderID);

			if (existingOrder == null)
			{
				return false;
			}
			mapper.Map(u, existingOrder);
			orderRepository.Update(existingOrder);
			return true;
		}
		// ___________________________Delete Order ___________________________
		public bool Delete(int orderID, string? userID)
		{
			var existingOrder = orderRepository.GetOrderByID(orderID);

			if (existingOrder == null)
			{
				return false;
			}
			if (userID != null && existingOrder.CustomerID != userID)
			{
				return false;
			}
			orderRepository.Delete(existingOrder);
			return true;
		}


	}
}
