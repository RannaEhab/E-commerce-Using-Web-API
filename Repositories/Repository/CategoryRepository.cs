using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repositories.Contract;

namespace Repositories.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
		protected ECommerceWebsiteContext context;
        public CategoryRepository(ECommerceWebsiteContext context) : base(context)
        {
            this.context = context;
        }


		public IQueryable<Category> Search(string s)
		{
			var searchedCataloge = context.Categories.Where(e => e.CategoryName.Contains(s));
			return searchedCataloge;

		}

		public Category GetCategoryByID(int ID)
		{
			var category = context.Categories
				.Include(p => p.Products)
				.FirstOrDefault(p => p.CategoryID == ID);
			return category;
		}
	}
}
