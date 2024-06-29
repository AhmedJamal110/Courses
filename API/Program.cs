using API.Errors;
using API.Extensions;
using API.Helper.Mapping;
using API.Middelware;
using Entity.Identity;
using Entity.Interfaces;
using Entity.Services;
using Infrastructure.Data.Context;
using Infrastructure.Reposatories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;
using StackExchange.Redis;
using System.Text;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddSwaggerDocmuntation();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));
            builder.Services.AddAutoMapper(typeof(ProfileMapping));
            
            builder.Services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = (action) =>
                {
                    var errors = action.ModelState.Where(e => e.Value.Errors.Count() > 0)
                                                   .SelectMany(e => e.Value.Errors)
                                                   .Select(e => e.ErrorMessage).ToList();


                    var responseValidition = new ApiValidationResponse(StatusCodes.Status400BadRequest, errors);

                    return new BadRequestObjectResult(responseValidition);
                
                };
            });
            builder.Services.AddScoped<StripetPaymentServices>();




            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<StoreDbContext>();
            builder.Services.AddScoped<ITokenServices, TokenServices>();
            builder.Services.AddAuthentication( opt=>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                         .AddJwtBearer(option =>
                          {
                              option.TokenValidationParameters = new TokenValidationParameters
                              {

                                  ValidateIssuerSigningKey = true,
                                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
                                  ValidateIssuer = true,
                                  ValidIssuer = builder.Configuration["Token:ValidIssuer"],
                                  ValidateAudience = true,
                                  ValidAudience = builder.Configuration["Token:ValidAudiance"],
                                  ValidateLifetime = true,



                              };


                          });

            builder.Services.AddAuthorization();

                






            var app = builder.Build();
      using var Scope = app.Services.CreateScope();
            var servicesProvider = Scope.ServiceProvider;

            var loggerFactory = servicesProvider.GetRequiredService<ILoggerFactory>();

            try
            {
                
                var Context = servicesProvider.GetRequiredService<StoreDbContext>();
                 await   Context.Database.MigrateAsync();

               var userManger = servicesProvider.GetRequiredService<UserManager<AppUser>>();
              await  StoreContextSeeding.SeedDatabaseAsync(Context, loggerFactory , userManger);

            }
            catch (Exception ex)
            {
                var logger =loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error happed while update Database");
            }


            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddelware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/redirct/{0}");

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();



            app.Run();
        }
    }
}
