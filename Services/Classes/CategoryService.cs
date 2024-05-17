using AutoMapper;
using DTOs.Categories;
using Models.Models;
using Repositories.Contract;
using Services.Interface;

namespace Services.Classes
{
    public class CategoryService : ICategoryService
    {
        protected IProductRepository productRepository { get; set; }
        protected ICategoryRepository categoryRepository { get; set; }
        protected IMapper mapper { get; set; }
        public CategoryService(IProductRepository _productRepository, ICategoryRepository _categoryRepository, IMapper _mapper)
        {
            productRepository = _productRepository;
            categoryRepository = _categoryRepository;
            mapper = _mapper;
        }
        // ___________________________ Search ___________________________
        public List<ReadCategoryDTO> Search(string categoryName)
        {
            var category = categoryRepository.Search(categoryName).Select(c => mapper.Map<ReadCategoryDTO>(c)).ToList();
            return category;
        }

        // ___________________________ Get All Categories ___________________________
        public List<ReadCategoryDTO> GetAll()
        {
            var products = categoryRepository.GetAll();
            var mappedCategories = mapper.Map<List<ReadCategoryDTO>>(products);
            return mappedCategories;
        }
        // ___________________________ Get One Category ___________________________
        public ReadCategoryDTO GetByID(int ID)
        {
            var category = categoryRepository.GetCategoryByID(ID);
            if (category == null)
            {
                return null;
            }
            return mapper.Map<ReadCategoryDTO>(category);
        }

        // ___________________________ Create new Category ___________________________
        public bool Create(CreateAndUpdateCategoryDTO c)
        {
            var existingCategoryQuery = categoryRepository.GetAll();
            var existingCategory = existingCategoryQuery.FirstOrDefault(p => p.CategoryName == c.CategoryName);
            if (existingCategory != null)
            {
                return false;
            }
            else
            {
                var category = mapper.Map<Category>(c);
                categoryRepository.Create(category);
                return true;
            }
        }

        // _________________________ Update Category _________________________
        public bool Update(int ID, CreateAndUpdateCategoryDTO u)
        {
            var existingCategory = categoryRepository.GetCategoryByID(ID);
            if (existingCategory == null)
            {
                return false;
            }
            mapper.Map(u, existingCategory);
            categoryRepository.Update(existingCategory);
            return true;
        }

        // _________________________ Delete Product _________________________
        public bool Delete(int ID)
        {
            var existingCategory = categoryRepository.GetCategoryByID(ID);
            if (existingCategory == null)
            {
                return false;
            }
            categoryRepository.Delete(existingCategory);
            return true;
        }
    }
}
