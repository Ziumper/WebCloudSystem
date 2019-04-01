using System;
using Microsoft.EntityFrameworkCore;

namespace WebCloudSystem.Dal
{
    public class WebCloudSystemContext : DbContext
    {
        public WebCloudSystemContext(DbContextOptions<WebCloudSystemContext> options)
        : base(options)
        { 

        }
    }
}
