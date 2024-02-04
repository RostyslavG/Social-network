using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tryzoob.Data;
namespace Tryzoob
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<TryzoobContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TryzoobContext") ?? throw new InvalidOperationException("Connection string 'TryzoobContext' not found.")));

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(100);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddHttpContextAccessor();
            
            var app = builder.Build();
            app.Services.GetRequiredService<IHttpContextAccessor>();
            IWebHostEnvironment env = app.Services.GetRequiredService<IWebHostEnvironment>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapRazorPages();

            app.Run();
        }
    }
}