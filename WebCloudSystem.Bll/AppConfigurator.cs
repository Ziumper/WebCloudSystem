using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebCloudSystem.Bll.Services.Users;
using WebCloudSystem.Dal;
using WebCloudSystem.Dal.Repositories.Users;

namespace WebCloudSystem.Bll
{
  
    public class AppConfigurator 
    {
        private IServiceCollection _services;

        public AppConfigurator(IServiceCollection services) {
            _services = services;
        }

        public void ConfigureDependencyInjection() {
            _services.AddTransient<IUserService,UserService>();
            _services.AddTransient<IUserRepository,UserRepository>();
        }

        public void AddAutoMapper() {
            _services.AddAutoMapper(typeof(AppConfigurator).Assembly);
        }

        public void EstablishConnection(string connectionString) {
            _services.AddDbContext<WebCloudSystemContext>(options => {
                options.UseSqlServer(connectionString);
            });
        }
     
    }
}
