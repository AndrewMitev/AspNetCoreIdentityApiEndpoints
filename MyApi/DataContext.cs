using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyApi
{
    public class DataContext : IdentityDbContext<MyUser, MyRole, int>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MyRole>().HasData([new MyRole 
            {
                Id = 1,
                Name = MyRoleEnum.Athlete.ToString(),
                NormalizedName = MyRoleEnum.Athlete.ToString().ToUpper()
            },
            new MyRole 
            {
                Id = 2,
                Name = MyRoleEnum.Sponsor.ToString(),
                NormalizedName = MyRoleEnum.Sponsor.ToString().ToUpper()
            }]);

            base.OnModelCreating(builder);
        }
    }
}
