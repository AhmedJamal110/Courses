using Entity.Models;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StripetPaymentServices
    {
        private readonly IConfiguration _configuration;

        public StripetPaymentServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<PaymentIntent> PaymentIntendId(Basket basket)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSetting:SecretKey"];

            var paymentIntend = new PaymentIntent();
            var services = new PaymentIntentService();


            if (string.IsNullOrEmpty(basket.PaymentIntendId))
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(I => I.Course.Price) * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };

                 paymentIntend = await services.CreateAsync(option);
            
            }
            else
            {
                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(I => I.Course.Price) * 100
                };

               paymentIntend = await services.UpdateAsync(basket.PaymentIntendId, option);
            
            }

            return paymentIntend;
        
        }

    }
}
