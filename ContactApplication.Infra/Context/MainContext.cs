using ContactApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ContactApplication.Infra.Context
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }

    }
}
