using API.Dto;
using AutoMapper;
using Entity.Models;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace API.Controllers
{
    public class PaymentsController : BaseController
    {
        private readonly StripetPaymentServices _paymentServices;
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public PaymentsController(StripetPaymentServices paymentServices , 
            StoreDbContext context , IMapper mapper)
        {
            _paymentServices = paymentServices;
            _context = context;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePayment()
        {
            var basket = await ExtractBasket(User.Identity.Name);
            if (basket is null)
                return NotFound();

            var intend =  await _paymentServices.PaymentIntendId(basket);
            if (intend is null)
                return BadRequest();

            basket.PaymentIntendId = basket.PaymentIntendId ?? intend.Id;
            basket.ClientSecrit = basket.ClientId ?? intend.ClientSecret;

            _context.Baskets.Update(basket);
           var result = await  _context.SaveChangesAsync() > 0;

            var basketMapping = _mapper.Map<BasketDto>(basket);
            if (result)
                return Ok(basketMapping);

            return BadRequest();
        }



        private async Task<Basket> ExtractBasket(string clientId)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                Response.Cookies.Delete("clientId");
                return null;
            }

            return await _context.Baskets.Include(b => b.Items)
                                         .ThenInclude(i => i.Course)
                                         .FirstOrDefaultAsync(b => b.ClientId == clientId);
        
        
        }
    }
}
