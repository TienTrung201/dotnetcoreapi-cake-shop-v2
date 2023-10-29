﻿using AutoMapper;
using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi.cake.shop.application
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // Get all categories response DTO
        public async Task<List<CategoryResponseDto>> GetAllCategories(int? limit = null)
        {
            var allCategoriesQuery = _categoryRepository.GetAllEntities();

            // Get limit categories
            if(limit.HasValue)
            {
                allCategoriesQuery = allCategoriesQuery.Take(limit.Value);
            }

            var allCategories = await allCategoriesQuery.ToListAsync();
            var allCategoryResponseDtos = _mapper.Map<List<CategoryResponseDto>>(allCategories);

            return allCategoryResponseDtos;
        }

        // Get category response DTO
        public async Task<CategoryResponseDto> GetCategoryById(int categoryId)
        {
            var category = await _categoryRepository.GetEntityByIdAsync(categoryId);

            var categoryResponseDto = _mapper.Map<CategoryResponseDto>(category);
            return categoryResponseDto;
        }

        // Create category
        public async Task<CategoryResponseDto> CreateCategory(CategoryRequestDto categoryRequestDto)
        {
            var newCategory = _mapper.Map<Category>(categoryRequestDto);
            newCategory.CreateAt = DateTime.UtcNow;

            var createdCategory = await _categoryRepository.CreateEntityAsync(newCategory);

            var createdCategoryResponseDto = _mapper.Map<CategoryResponseDto>(createdCategory);
            return createdCategoryResponseDto;
        }

        // Update category
        public async Task<CategoryResponseDto> UpdateCategory(int id, CategoryRequestDto categoryRequestDto)
        {
            var existCategory = await _categoryRepository.GetEntityByIdAsync(id);

            if (existCategory == null)
            {
                throw new Exception("category not found");
            }

            _mapper.Map(categoryRequestDto, existCategory);
            var updatedCategory = await _categoryRepository.UpdateEntityAsync(existCategory);

            var updatedCategoryResponseDto = _mapper.Map<CategoryResponseDto>(updatedCategory);
            return updatedCategoryResponseDto;
        }

        // Delete category
        public async Task<CategoryResponseDto> DeleteCategory(int categoryId)
        {
            var category = await _categoryRepository.GetEntityByIdAsync(categoryId);

            if (category == null)
            {
                throw new Exception("category not found");
            }

            var deletedCategory = await _categoryRepository.DeleteEntityAsync(category);

            var deletedCategoryResponseDto = _mapper.Map<CategoryResponseDto>(deletedCategory);
            return deletedCategoryResponseDto;
        }
    }
}
