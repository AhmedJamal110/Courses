using API.Dto;
using AutoMapper;
using Entity.Interfaces;
using Entity.Models;
using Entity.Specifications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class CategoriesController : BaseController
    {
        private readonly IGenaricRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public CategoriesController( IGenaricRepository<Category> categoryRepo , IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoriesDto>>> GetAllCategories()
        {
            //var Spec = new CategoetWithCourseSpec();
            var categories = await _categoryRepo.GetAllAsync();
            var categoriesDto = _mapper.Map<IReadOnlyList<CategoriesDto>>(categories);
            return Ok(categoriesDto);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        
        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategoryByid(int id)
        {
            var Spec = new CategoetWithCourseSpec(id);
            var category = await _categoryRepo.GetByIdWithSpecAsync(Spec);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

    }
}
