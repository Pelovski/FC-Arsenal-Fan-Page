namespace FCArsenalFanPage.Web
{
    using System;
    using System.Reflection;

    using FCArsenalFanPage.Data;
    using FCArsenalFanPage.Data.Common;
    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Data.Repositories;
    using FCArsenalFanPage.Data.Seeding;
    using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Services.Mapping;
    using FCArsenalFanPage.Services.Messaging;
    using FCArsenalFanPage.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services, builder.Configuration);
            var app = builder.Build();
            Configure(app);
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var smtpServer = configuration["SmtpServer"];
            var smtpPort = int.Parse(configuration["SmtpPort"]);
            var smtpUser = configuration["SmtpUser"];
            var smtpPass = configuration["SmtpPass"];

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductCategoriesService, ProductCategoriesService>();
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IOrderStatusService, OrderStatusService>();
            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<IPremierLeagueService, PremierLeagueService>();

            services.AddTransient<IEmailSender, MailKitEmailSender>(provider =>
            new MailKitEmailSender(smtpServer, smtpPort, smtpUser, smtpPass));

            services.AddHttpClient("FootballData", client =>
            {
                client.BaseAddress = new Uri("https://api.football-data.org/v4/");
                client.DefaultRequestHeaders.Add("X-Auth-Token", configuration["X-Auth-Token"]);
            });

            services.AddAuthentication().AddFacebook(opt =>
            {
                opt.ClientId = configuration["Facebook:ClientId"];
                opt.ClientSecret = configuration["Facebook:ClientSecret"];
            })
            .AddGoogle(opt =>
            {
                opt.ClientId = configuration["Google:ClientId"];
                opt.ClientSecret = configuration["Google:ClientSecret"];
            })
            .AddTwitter(opt =>
            {
                opt.ConsumerKey = configuration["Twitter:ConsumerKey"];
                opt.ConsumerSecret = configuration["Twitter:ConsumerSecret"];
            });
        }

        private static void Configure(WebApplication app)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
        }
    }
}
