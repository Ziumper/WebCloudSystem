using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebCloudSystem.Dal;

namespace WebCloudSystem.Bll
{
  
    public class AppConfigurator 
    {
        private IServiceCollection services;

        public AppConfigurator(IServiceCollection services) {
            this.services = services;
        }

        public void ConfigureDependencyInjection() {
         
        }

        public void AddAutoMapper() {
            this.services.AddAutoMapper(typeof(AppConfigurator).Assembly);
        }

        public void EstablishConnection(string connectionString) {
            services.AddDbContext<WebCloudSystemContext>(options => {
                options.UseSqlServer(connectionString);
            });
        }
     
    }
}
