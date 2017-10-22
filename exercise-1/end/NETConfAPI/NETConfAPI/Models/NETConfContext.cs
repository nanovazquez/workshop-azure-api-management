using System;
using Microsoft.EntityFrameworkCore;

namespace NETConfAPI.Models
{
    public class NETConfContext : DbContext
    {
        public NETConfContext(DbContextOptions<NETConfContext> options)
            : base(options)
        {

        }

        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Talk> Talks { get; set; }
    }
}
