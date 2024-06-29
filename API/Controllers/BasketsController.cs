using API.Dto;
using API.Errors;
using AutoMapper;
using Entity.Models;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class BasketsController : BaseController
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public BasketsController(StoreDbContext context , IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket =await  ExtractBasket(setClientId());
            if (basket is null)
                return NotFound(new ApiResponse(404));

            var basketDto = _mapper.Map<BasketDto>(basket);
            return Ok(basketDto);
        }


        [HttpPost]
        public async Task<ActionResult<BasketDto>> AddToBasket(Guid courseId)
        {
            var basket = await ExtractBasket(setClientId());

            if (basket is null)
                basket =  CreateBasket();

            var course = await _context.Courses.FindAsync(courseId);
            if (course is null)
                return NotFound(new ApiResponse(404));

            basket.AddToBasket(course);

           var result = await _context.SaveChangesAsync() > 0 ;
            var basketDto = _mapper.Map<BasketDto>(basket);
            if (result)
                return Ok(basketDto);
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "proplem happend while saving data"));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteFromBasket(Guid courseId)
        {
            var basket = await ExtractBasket(setClientId());
            if (basket is null)
                return NotFound(new ApiResponse(404));
            basket.DeleteFromBasket(courseId);

            var result = await _context.SaveChangesAsync() > 0;
            if (result)
                return Ok();
            return BadRequest(new ApiResponse(400, "problem while saving the data"));
                
         }

        [HttpDelete("Clear-basket")]
        public async Task<ActionResult> ClearBasket()
        {
            var basket = await ExtractBasket(setClientId());

            if (basket is null)
                return NotFound();

            basket.ClearBasket();
            var result = await _context.SaveChangesAsync() > 0;
            if (result)
                return Ok();

            return BadRequest();
        
        }
        
        private async Task<Basket?> ExtractBasket(string clientId)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                Response.Cookies.Delete("clientId");
                return null;
            }

           return await _context.Baskets.Include(B => B.Items)
                .ThenInclude(I => I.Course)
                .FirstOrDefaultAsync(B => B.ClientId == clientId);
        }
   
        private string? setClientId()
        {
            return User.Identity?.Name ?? Request.Cookies["clientId"];
        }


        private  Basket? CreateBasket()
        {
            var clientId = User.Identity.Name;
            if (string.IsNullOrEmpty(clientId))
            {
                clientId = Guid.NewGuid().ToString();

                var opt = new CookieOptions()
                { IsEssential = true, Expires = DateTime.Now.AddDays(10) };

                Response.Cookies.Append("clientId", clientId, opt);
            }

            var basket = new Basket { ClientId = clientId };
            _context.Add(basket);
            return basket;
        
        }


    }
}
