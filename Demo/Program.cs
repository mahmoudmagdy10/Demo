using Demo.BL.Interface;
using Demo.BL.Repository;
using Demo.DAL.Database;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Demo.BL.Mapper;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Razor;
using Demo.Language;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Identity;
using Demo.BL.Helper.Hubs;

namespace Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                })
                .AddNewtonsoftJson(opt => {
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            builder.Services.AddScoped<IDepartmentRep, DepartmentRep>();
            builder.Services.AddScoped<IEmployeeRep, EmployeeRep>();
            builder.Services.AddScoped<ICountryRep, CountryRep>();
            builder.Services.AddScoped<ICityRep, CityRep>();
            builder.Services.AddScoped<IDistrictRep, DistrictRep>();
            builder.Services.AddScoped<IUserRep, UserRep>();
            builder.Services.AddScoped<IRolesRep, RolesRep>();

            builder.Services.AddDbContextPool<DemoContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DemoConnection"))
            );
            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.LoginPath = new PathString("/Home/Index");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            }).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DemoContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            builder.Services.AddSignalR();

            var app = builder.Build();

            app.MapHub<ChatHub>("/chatHub");

            #region All Needed Configurations Need To Localization
            var supportedCultures = new[] {
                      new CultureInfo("ar-EG"),
                      new CultureInfo("en-US"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
            });
            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}