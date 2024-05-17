using DTOs.Categories;

namespace Services.Interface
{
    public interface ICategoryService
    {
        List<ReadCategoryDTO> Search(string categoryName);
        List<ReadCategoryDTO> GetAll();
        ReadCategoryDTO GetByID(int ID);
        bool Create(CreateAndUpdateCategoryDTO c);
        bool Update(int ID, CreateAndUpdateCategoryDTO u);
        bool Delete(int ID);
    }
}
