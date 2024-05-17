using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repositories.Contract;

namespace Repositories.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        protected ECommerceWebsiteContext context { get; set; }
        public ProductRepository(ECommerceWebsiteContext context) : base(context)
        {
            this.context = context;
        }

		public IQueryable<Product> Search(string s)
		{
			return context.Products.Where(e => e.ProductName.Contains(s));
		}

		public Product GetProductByID(int ID)
        {
            var product = context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductID == ID);
			return product;
		}

    }
}
