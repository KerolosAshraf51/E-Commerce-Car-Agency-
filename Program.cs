using CarAgency.Controllers;
using CarAgency.Models;
using CarAgency.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarAgency
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<context>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
            });

            builder.Services.AddIdentity<applicationUser, IdentityRole<int>>(option =>
            {
                option.Password.RequiredLength = 4;
                option.Password.RequireDigit = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<context>();


            builder.Services.AddScoped<ICarRepo, CarRpo>();
            builder.Services.AddScoped<IClassOfCarRepo, ClassOfCarRepo>();
            builder.Services.AddScoped<IPurchaseRepo, PurchaseRepo>();
            builder.Services.AddScoped<IReviewRepo, ReviewRepo>();
            builder.Services.AddScoped<IApplicationUserRepo, ApplicationUserRepo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=car}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
