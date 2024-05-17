using AutoMapper;
using DTOs.Products;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repositories.Contract;
using Services.Interface;

namespace Services.Classes
{
    public class ProductService : IProductService
    {
        protected IProductRepository productRepository { get; set; }
        protected ICategoryRepository categoryRepository { get; set; }
        protected IMapper mapper { get; set; }
        public ProductService(IProductRepository _productRepository, ICategoryRepository _categoryRepository, IMapper _mapper)
        {
            productRepository = _productRepository;
            categoryRepository = _categoryRepository;
            mapper = _mapper;
        }
        // ___________________________ Search ___________________________
        public List<ReadAllProductsDTO> Search(string p)
        {
            var searchedProducts = productRepository.Search(p).Select(p => mapper.Map<ReadAllProductsDTO>(p)).ToList();
            return searchedProducts;
        }
        // ___________________________ Get All Products ___________________________
        public List<ReadAllProductsDTO> GetAll()
        {
            var products = productRepository.GetAll();
            var mappedProducts = mapper.Map<List<ReadAllProductsDTO>>(products);
            return mappedProducts;
        }
        // ___________________________ Get One Product ___________________________
        public ReadAllProductsDTO GetByID(int ID)
        {
            var product = productRepository.GetProductByID(ID);
            if (product == null)
            {
                return null;
            }
            return mapper.Map<ReadAllProductsDTO>(product);
        }
        // ___________________________ Get Product By Categoryname ___________________________
        public List<ReadProductsByCategoryDTO> GetByCategory(string categoryName)
        {
            var products = productRepository.GetAll();
            var productsOfCategory = products
                .Include(p => p.Category)
                .Where(p => p.Category.CategoryName.ToLower() == categoryName.ToLower())
                .ToList();
            return mapper.Map<List<ReadProductsByCategoryDTO>>(productsOfCategory);
        }

        // ___________________________ Create new Product ___________________________
        public bool Create(CreateAndUpdateProductDTO c)
        {
            var existingProductQuery = productRepository.GetAll();
            var existingProduct = existingProductQuery.FirstOrDefault(product => product.ProductName == c.ProductName);
            if (existingProduct != null)
            {
                return false;
            }
            else
            {
                var existingCategory = categoryRepository.GetCategoryByID(c.CategoryID);
                if (existingCategory == null)
                {
                    return false;
                }
                else
                {
                    var product = mapper.Map<Product>(c);
                    productRepository.Create(product);
                    return true;
                }
            }
        }

        // _________________________ Update Product _________________________
        public bool Update(int ID, CreateAndUpdateProductDTO u)
        {
            var existingProduct = productRepository.GetProductByID(ID);
            if (existingProduct == null)
            {
                return false;
            }
            mapper.Map(u, existingProduct);
            productRepository.Update(existingProduct);
            return true;
        }

        // _________________________ Delete Product _________________________
        public bool Delete(int ID)
        {
            var existingProduct = productRepository.GetProductByID(ID);
            if (existingProduct == null)
            {
                return false;
            }
            productRepository.Delete(existingProduct);
            return true;
        }
    }
}
