using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenithWebsite.Models
{
    public class ZenithContext : DbContext
    {
        public ZenithContext(DbContextOptions<ZenithContext> options)
            : base(options) { }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Event> Events { get; set; }

    }
}
