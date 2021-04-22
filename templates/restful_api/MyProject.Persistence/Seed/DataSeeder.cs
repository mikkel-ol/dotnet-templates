using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MyProject.Persistence.Seed
{
    public static class DataSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider, bool createAdminUser = false)
        {
            var provider = serviceProvider.CreateScope().ServiceProvider;

            using (var context = provider.GetRequiredService<AppDbContext>())
            {
                context.Database.Migrate();

                if (createAdminUser)
                {
                }
            }
        }
    }
}
