using API.Dto;
using AutoMapper;
using Entity.Identity;
using Entity.Models;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class LecturesController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public LecturesController(UserManager<AppUser> userManager , StoreDbContext context
            
            , IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<UserLectureDto>> GetLectures(Guid courseId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var course = await _context.Courses.FindAsync(courseId);

            var sections = await _context.Sections.Where(s => s.CourseId == course.Id).Include(s => s.Lectures).ToListAsync();

            var userCourse = await _context.UserCourses.Where(uc => uc.AppUser == user).Where(u => u.Course == course).FirstOrDefaultAsync();

            var userLecture = new UserLectureDto
            {
                Sections = _mapper.Map<List<Section>, List<SectionDto>>(sections),
                CurrentLecture = userCourse.CurrentLecture,
                CourseName = course.Title,
            };

            return Ok(userLecture);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public async Task<ActionResult> UpdateLectureCourse(UpdateLecureDto lecureDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // course =>

           var userCurse = await _context.UserCourses.Where(uc => uc.AppUser == user).Where(u => u.CourseId == lecureDto.CourseId).FirstOrDefaultAsync();

            userCurse.CurrentLecture = lecureDto.LectureId;

            var result = await _context.SaveChangesAsync() > 0;
            if (result)
                return Ok();
            return BadRequest();

        }

    }
}
