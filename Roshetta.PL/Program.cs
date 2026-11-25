using Microsoft.EntityFrameworkCore;
using Rosetta.DAL.Data.Contexts;

namespace Roshetta.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();

            // Configure DbContext with smart fallback for macOS / development
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<RoshettaDbContext>(options =>
            {
                var forceSqlServer = builder.Configuration.GetValue<bool>("ForceSqlServer", false) ||
                                     Environment.GetEnvironmentVariable("FORCE_SQL_SERVER") == "true";

                if (forceSqlServer && !string.IsNullOrEmpty(connectionString))
                {
                    options.UseSqlServer(connectionString);
                }
                else
                {
                    // InMemory database ensures smooth execution on macOS and for live demos without local SQL Server
                    options.UseInMemoryDatabase("RoshettaDB");
                }
            });

            // Application Business Services (Task 3: Fady Maged - Cart, Checkout, Orders)

            var app = builder.Build();

            // Initialize Database Schema & Seed Demonstration Data
            try
            {
                using var scope = app.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<RoshettaDbContext>();
                dbContext.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                app.Logger.LogWarning(ex, "Database auto-seeding error: {Message}", ex.Message);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
