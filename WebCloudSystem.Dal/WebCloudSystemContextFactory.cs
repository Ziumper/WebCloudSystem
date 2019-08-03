using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WebCloudSystem.Dal;

namespace WebCloudsystem.Dal {
    public class WebCloudsystemContextFactory : IDesignTimeDbContextFactory<WebCloudSystemContext> {

        public WebCloudSystemContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WebCloudSystemContext>();
            optionsBuilder.UseSqlServer("Data Source=webcloudsystemweb20190407011119dbserver.database.windows.net;Initial Catalog=WebCloudsystemWeb20190407011119_db;User ID=webcloudsystem;Password=MistrzWebu102;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            return new WebCloudSystemContext(optionsBuilder.Options);
        }
    }
}