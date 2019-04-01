using System;
using Microsoft.EntityFrameworkCore;
using WebCloudSystem.Dal.Models;

namespace WebCloudSystem.Dal
{
    public class WebCloudSystemContext : DbContext
    {
        public DbSet<User> Users {get;set;}
        public WebCloudSystemContext(DbContextOptions<WebCloudSystemContext> options)
        : base(options)
        { 

        }
    }
}
