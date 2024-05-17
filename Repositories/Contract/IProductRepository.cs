using Models.Models;

namespace Repositories.Contract
{
    public interface IProductRepository : IGenericRepository<Product>
    {
		IQueryable<Product> Search(string s);
		Product GetProductByID(int ID);



	}
}
