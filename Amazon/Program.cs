using Amazon.Authorization;
using Amazon.Data;
using Amazon.Mapping;
using Amazon.Services;
using Amazon.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure services
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Identity with ApplicationUser
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>() // Add role management
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddControllersWithViews();

// Register your custom services
builder.Services.AddScoped<LaptopService>();
builder.Services.AddScoped<UserManager<IdentityUser>>();
//builder.Services.AddScoped<SignInManager<IdentityUser>, CustomSignInManager>();

builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(MappingProfile)); // Register AutoMapper with your mapping profile

var app = builder.Build();

// Role seeding during application startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Call the method to seed roles
        await RoleSeeder.SeedRolesAsync(services);
    }
    catch (Exception ex)
    {
        // Log any errors that occur during role seeding
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding roles.");
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); // Ensure migrations are enabled in development
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Use Authentication middleware
app.UseAuthorization(); // Use Authorization middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
