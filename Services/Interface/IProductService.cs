using DTOs.Products;

namespace Services.Interface
{
    public interface IProductService
    {
        List<ReadAllProductsDTO> Search(string p);
        List<ReadAllProductsDTO> GetAll();
        ReadAllProductsDTO GetByID(int ID);
        List<ReadProductsByCategoryDTO> GetByCategory(string categoryName);
        bool Create(CreateAndUpdateProductDTO c);
        bool Update(int ID, CreateAndUpdateProductDTO u);
        bool Delete(int ID);
    }
}
