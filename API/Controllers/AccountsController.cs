using API.Dto;
using API.Dto.Identity;
using API.Errors;
using AutoMapper;
using Entity.Identity;
using Entity.Models;
using Entity.Services;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers
{
    
    public class AccountsController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenServices _token;
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public AccountsController( UserManager<AppUser> userManager , SignInManager<AppUser> signInManager , 
            ITokenServices token , StoreDbContext context , IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(loginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                    return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized));

           var email =  await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!email.Succeeded)
                return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized));


            var userBasket = await ExtractBasket(user.UserName);
          
            var basket = await ExtractBasket(Request.Cookies["clientId"]);
            
            if(basket is not null)
            {
                if(userBasket is not null)
                {
                    _context.Baskets.Remove(userBasket);
                    basket.ClientId = user.UserName;
                    Response.Cookies.Delete("clienyt");
                    await _context.SaveChangesAsync();
                }
            }
            var userDto = new UserDto
            {
                Email = user.Email,
                Token = await _token.CreatTokenAsync(user),
                BasketDto = basket != null ? _mapper.Map<BasketDto>(basket)
                : _mapper.Map<BasketDto>(userBasket) 
               
            };

            return Ok(userDto);
    
    }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var user = new AppUser
            {
                UserName = model.Name,
                Email = model.Email
            };

           var Result  =  await _userManager.CreateAsync(user, model.Password);
            if (!Result.Succeeded)
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));

            await _userManager.AddToRoleAsync(user, "Student");
            var userDto = new UserDto
            {
                Email = user.Email,
                Token = await _token.CreatTokenAsync(user)
            };

            return Ok(userDto);

        }



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("purches-course")]
        public async Task<ActionResult> PurchesCourse()
        {
            var basket = await ExtractBasket(User.Identity.Name);

            
            //var user = await _userManager.FindByEmailAsync(ClaimTypes.Email);
            
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            foreach (var item in basket.Items)
            {
                var userCourse = new UserCourse
                {
                    CourseId = item.CourseId,
                    AppUserId = user.Id
                };

                await _context.UserCourses.AddAsync(userCourse);
            
            }
            var result = await _context.SaveChangesAsync() > 0;

            if (result)
                return Ok();
            return BadRequest();
        }



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("current-user")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user is null)
                return NotFound();

            var basket = await ExtractBasket(User.Identity.Name);

            var course = _context.UserCourses.AsQueryable();

            var userDto = new UserDto
            {
                Email = user.Email,
                Token = await _token.CreatTokenAsync(user),
                BasketDto = _mapper.Map<BasketDto>(basket),
               Courses =await  course.Where( c => c.AppUserId == user.Id)
                        .Select( u => u.Course).ToListAsync()

            };

            return Ok(userDto);
        }



        [Authorize]
        [HttpPost("add-role")]
        public async Task<ActionResult> AddRole()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            _userManager.AddToRoleAsync(user, "Instructor");

            return Ok();
        }



        private async Task<Basket?> ExtractBasket(string clientId)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                Response.Cookies.Delete(clientId);
                return null;
            }

            return await _context.Baskets.Include(B => B.Items)
                .ThenInclude(I => I.Course)
                .FirstOrDefaultAsync(B => B.ClientId == clientId);                                
        
        }
    }
}
