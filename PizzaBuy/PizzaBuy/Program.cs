using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzaBuy.Data;
using PizzaBuy.Repositories;
using Stripe;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<PizzaBuy.Data.PizzaBuyDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("PizzaConnectionString")));

        //injecting auth db context
        builder.Services.AddDbContext<PizzaBuy.Data.AuthDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("PizzaAuthDbConnectionString")));


        builder.Services.AddIdentity<IdentityUser ,  IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();


        //changeing the default passoword conditions
        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
        }
        );



        //Repo injected
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
        builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();

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

        StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}