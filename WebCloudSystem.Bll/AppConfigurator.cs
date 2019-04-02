using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebCloudSystem.Bll.Services.Users;
using WebCloudSystem.Dal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebCloudSystem.Dal.Repositories.Users;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebCloudSystem.Bll
{

    public class AppConfigurator
    {
        private IServiceCollection _services;

        public AppConfigurator(IServiceCollection services)
        {
            _services = services;
        }

        public void ConfigureDependencyInjection()
        {
            _services.AddTransient<IUserService, UserService>();
            _services.AddTransient<IUserRepository, UserRepository>();
        }

        public void AddAutoMapper()
        {
            _services.AddAutoMapper(typeof(AppConfigurator).Assembly);
        }

        public void EstablishDatabaseConnection(string connectionString)
        {
            _services.AddDbContext<WebCloudSystemContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        public void ConfigureJwtAuthentication(string secret)
        {
            var key = Encoding.ASCII.GetBytes(secret);
            _services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }


    }
}
