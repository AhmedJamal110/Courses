using API.Dto;
using API.Helper;
using AutoMapper;
using Entity.Interfaces;
using Entity.Models;
using Entity.Specifications;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class CoursesController : BaseController
    {
        private readonly IGenaricRepository<Course> _courseRepo;
        private readonly IMapper _mapper;
        private readonly StoreDbContext _context;

        public CoursesController( IGenaricRepository<Course> courseRepo, IMapper mapper  , StoreDbContext context)
        {
            _courseRepo = courseRepo;
            _mapper = mapper;
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<CourseDto>> GetAllCourses([FromQuery] CourseSpec courseSpec)
        {
            var Spec = new CourseWithCategorySpec(courseSpec);

            var courses = await _courseRepo.GetAllWithSpecAsync(Spec);
            var coursesDto = _mapper.Map<List<CourseDto>>(courses);

            var SpecForCount = new CourseWithFilteration(courseSpec);

             var count = await _courseRepo.CountWithSpecAsync(SpecForCount);

            var standerResponse = new PaginationStanderResponse<CourseDto>

                (courseSpec.PageSize, courseSpec.PageIndex, count, coursesDto);
            
            return Ok(standerResponse);

        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CourseDto>> GetCourseById(Guid id)
        {
            var spec = new CourseWithCategorySpec(id);
             var course = await _courseRepo.GetByIdWithSpecAsync(spec);
            var courseDto = _mapper.Map<CourseDto>(course);
            return Ok(courseDto);
        
        }


        [Authorize(Roles ="Instructor")]
        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse(Course course)
        {
            course.Instructor  = User.Identity.Name;

            await _context.AddAsync(course);

            var courseDto = _mapper.Map<CourseDto>(course);

            var result = await _context.SaveChangesAsync() > 0;

            if (result)
                return Ok(courseDto);
            return BadRequest();
        }
    }
}
