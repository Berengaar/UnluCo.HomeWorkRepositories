using EmirhanAvci.WebApi.Authentication;
using EmirhanAvci.WebApi.BusinessParticles.Abstract;
using EmirhanAvci.WebApi.BusinessParticles.Concrete;
using EmirhanAvci.WebApi.DataParticles.Abstract;
using EmirhanAvci.WebApi.DataParticles.Concrete;
using EmirhanAvci.WebApi.Helpers.Extensions;
using EmirhanAvci.WebApi.Middleware;
using EmirhanAvci.WebApi.Models;
using EmirhanAvci.WebApi.Validation;
using EmirhanAvci.WebApi.Validation.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmirhanAvci.WebApi.Filters;

namespace EmirhanAvci.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(config => config.Filters.Add(new GlobalHeaderResultFilter()));

            //DbContext Service
            services.AddDbContext<Context>(
            options => options.UseSqlServer("DefaultConnection"));

            // For Identity  
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();



            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                //Adding Jwt Bearer(Its adding to authentication)

                .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            }); ;




            //Swagger Service
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmirhanAvci.WebApi", Version = "v1" });
            });


            //BusinessParticle Service
            services.AddScoped<ICoinService, CoinManager>();
            services.AddScoped<ITokenService, TokenManager>();
            services.AddScoped<ICoinValidationService, CoinValidation>();
            services.AddScoped<ITokenValidationService, TokenValidation>();

            //DataParticle Service
            services.AddScoped<ICoinParticleGeneratorService, CoinParticleGenerator>();
            services.AddScoped<ITokenParticleGeneratorService, TokenParticleGenerator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmirhanAvci.WebApi v1"));
            }

            // Log Middleware
            app.UseCustomExceptionMiddle();

            //Error Handler Middleware
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
