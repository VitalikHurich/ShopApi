using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ShopApi.BLL.DTO;
using ShopApi.BLL.Response;
using ShopApi.BLL.Services.Interfaces;
using ShopApi.DAL.Models;
using ShopApi.DAL.Repositories.RepositoriesInterfaces;

namespace ShopApi.BLL.Services
{
    public class CategoryService : IService<CategoryDTO, CategoryResponse>
    {
        private readonly IRepository<Category> categoryRepository;
        private readonly IUnitOfwork unitOfwork;
        private readonly IMapper mapper;
        public CategoryService(IRepository<Category> categoryRepository, IUnitOfwork unitOfwork, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.unitOfwork = unitOfwork;
            this.mapper = mapper;
        }
        public async Task<CategoryResponse> DeleteAsync(int id)
        {
            var existingCategory = await categoryRepository.FindByIDAsync(id);
            if(existingCategory == null)
            {
                return new CategoryResponse("Category not found");
            }
            try
            {
                categoryRepository.Remove(existingCategory);
                await unitOfwork.CompleteAsync();
                return new CategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {
                return new CategoryResponse($"Error when deleting category: {ex.Message}");
            }
        }

        public async  Task<IEnumerable<CategoryDTO>> ListAsync()
        {
            var categories = await categoryRepository.ListAsync();
            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryResponse> SaveAsync(CategoryDTO categoryDTO)
        {
            Category category = mapper.Map<Category>(categoryDTO);
            try
            {
                await categoryRepository.AddASync(category);
                await unitOfwork.CompleteAsync();

                return new CategoryResponse(category);
            }
            catch (Exception ex)
            {
                return new CategoryResponse($"Error when seving the category: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> UpdateAsync(int id, CategoryDTO categoryDTO)
        {
            Category category = mapper.Map<Category>(categoryDTO);
            var existingCategory = await categoryRepository.FindByIDAsync(id);
            if(existingCategory == null)
            {
                return new CategoryResponse("Category not found");
            }
            existingCategory.CategoryName = category.CategoryName;
            try
            {
                categoryRepository.Update(existingCategory);
                await unitOfwork.CompleteAsync();
                return new CategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {
                return new CategoryResponse($"Error when updating category: {ex.Message}");
            }
        }
    }
}