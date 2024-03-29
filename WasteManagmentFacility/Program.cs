using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WasteManagmentFacility.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}
   );
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.AddRazorPages();



//var emailConfig = builder.Configuration
//        .GetSection("EmailConfiguration")
//        .Get<EmailConfiguration>();
//builder.Services.AddSingleton(emailConfig);
//builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
options.TokenLifespan = TimeSpan.FromHours(2));
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

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//app.MapAreaControllerRoute(
//    name: "admin",
//    pattern: "{controller=Admin}/{action=Index}");

//app.MapControllerRoute(
//    name: "admin",
//    pattern: "admin",
//    defaults: new { controller = "Admin", action = "Edit" });

//app.MapControllerRoute(
//    name: "register",
//    pattern: "register",
//    defaults: new { controller = "Account", action = "Register" });

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}


async Task Main()
{
    using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var roles = new[] { "Admin", "User", "Driver" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    using (var scope = app.Services.CreateScope())
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string email = "admin@admin.com";
        string password = "Admin123_";
        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new ApplicationUser();

            user.FirstName = email;
            user.LastName = email;
            user.PhoneNumber = "061222333";
            user.UserName = email;
            user.Email = email;
            user.NormalizedEmail = userManager.NormalizeEmail(email);

            await userManager.CreateAsync(user, password);

            await userManager.AddToRoleAsync(user, "Admin");
        }
    }

    app.Run();
}

await Main();
