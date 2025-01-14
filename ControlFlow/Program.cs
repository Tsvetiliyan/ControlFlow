using ControlFlow.Core;
using ControlFlow.Core.Entities;
using ControlFlow.Core.Entities.Emails;
using ControlFlow.Core.Interfaces.IServices;
using ControlFlow.Core.Interfaces.IUnitOfWork;
using ControlFlow.Data;
using ControlFlow.Data.UnitOfWork;
using ControlFlow.Infrastructure;
using ControlFlow.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ControlFlow
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddEntityFrameworkStores<IdentityAppContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAuthorization();
            builder.Services.AddDbContext<IdentityAppContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IEmailConfirmationService, EmailConfirmationService>();
            builder.Services.AddScoped<IUserTaskService, UserTaskService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddHostedService<EmailReminderService>();
            builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddUserSecrets<Program>(); // Used for storingh for the email service
                builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            }

            WebApplication app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
