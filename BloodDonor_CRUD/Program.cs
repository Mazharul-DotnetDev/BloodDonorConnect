using BloodDonor_CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonor_CRUD
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<DonorContext>(abc =>

            abc.UseSqlServer("server= DESKTOP-PQL41F3\\SQLEXPRESS; database =  DonorManage ; trusted_connection =true; trust server certificate =true;")

            );

            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

            app.MapControllerRoute(
            name: "donors",
            pattern: "Donors/{action=Index}/{id?}",
            defaults: new { controller = "Donors" });

            app.MapControllerRoute(
                name: "home",
                pattern: "Home/{action=Index}/{id?}",
                defaults: new { controller = "Home" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
          

            app.Run();
		}
	}
}
