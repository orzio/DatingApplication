using DatingApp.api.Model;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options){}

            public DbSet<Value> Values {get;set;} 
    }
}