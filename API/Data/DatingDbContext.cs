using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DatingDbContext:DbContext
    {
        public DatingDbContext(DbContextOptions<DatingDbContext> option):base(option){

        }

        public DbSet<User> users { get; set; }
        
    }
}