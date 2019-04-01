using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WebCloudSystem.Dal;

namespace WebCloudsystem.Dal {
    public class WebCloudsystemContextFactory : IDesignTimeDbContextFactory<WebCloudSystemContext> {

        public WebCloudSystemContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WebCloudSystemContext>();
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=WebCloudSystem;Trusted_Connection=True;");

            return new WebCloudSystemContext(optionsBuilder.Options);
        }
    }
}