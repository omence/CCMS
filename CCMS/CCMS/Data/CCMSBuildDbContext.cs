using CCMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.Data
{
    public class CCMSBuildDbContext : DbContext
    {
        public CCMSBuildDbContext(DbContextOptions<CCMSBuildDbContext> options) : base(options)
        {

        }
        public DbSet<Petition> Petition { get; set; }
    }
}
