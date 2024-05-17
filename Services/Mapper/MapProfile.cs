using AutoMapper;
using Models.Models;
using DTOs.Products;
using DTOs.Categories;
using DTOs.DTOs.OrderProducts;
using DTOs.Orders;
using DTOs.DTOs.Orders;

namespace Services.Mapper
{
	public class MapProfile : Profile
    {
        public MapProfile()
        {
			// ___________________________ Mapping Project Profile___________________________
			CreateMap<CreateAndUpdateProductDTO, Product>().ReverseMap();
            CreateMap<ReadAllProductsDTO, Product>().ReverseMap();
            CreateMap<ReadProductsByCategoryDTO, Product>().ReverseMap();

			// ___________________________ Mapping Category Profile___________________________
			CreateMap<ReadCategoryDTO, Category>().ReverseMap();
			CreateMap<CreateAndUpdateCategoryDTO, Category>().ReverseMap();

			// ___________________________ Mapping ProductOrder Profile___________________________
			CreateMap<ReadAndCreateProductOrderDTO, ProductOrder>().ReverseMap();

			// ___________________________ Mapping Order Profile___________________________
			CreateMap<CreateOrderDTO, Order>().ReverseMap();
			CreateMap<ReadAllOrdersDTO, Order>().ReverseMap();
			CreateMap<UpdateOrderDTO, Order>().ReverseMap();
		}
	}
}
