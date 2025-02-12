using BoardGameStore.Data;
using BoardGameStore.Models;
using BoardGameStore.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var cultureInfo = new CultureInfo("en-US"); // Use "en-US" for "." or "de-DE" for ","

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddTransient<SignInManager<User>>();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedEmail = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();
using var serviceScope = app.Services.CreateScope();
var serviceProvider = serviceScope.ServiceProvider;
var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
dbContext.Database.Migrate();

/*dbContext.Database.EnsureDeleted();
dbContext.Database.EnsureCreated();*/

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    string[] roles = { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    string adminEmail = "admin@example.com";
    string adminUsername = adminEmail;
    string adminPassword = "Admin@123";
    string firstName = "Admin";
    string lastName = "User";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var newUser = new User { UserName = adminUsername, Email = adminEmail, FirstName = firstName, LastName = lastName };
        var result = await userManager.CreateAsync(newUser, adminPassword);

        if (result.Succeeded)
        {
            var roleResult = await userManager.AddToRoleAsync(newUser, roles[0]);
            if (!roleResult.Succeeded)
            {
                throw new Exception($"Failed to add 'Admin' role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
            }
        }
        else
        {
            throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }

    if (!dbContext.BoardGames.Any())
    {
        dbContext.BoardGames.AddRange(
            new BoardGame
            {
                Title = "Catan",
                ImageUrl = "/images/Catan.png",
                Category = Category.StrategyGame,
                MinPlayers = 3,
                MaxPlayers = 4,
                Description = "A strategy game where players collect resources and build roads and settlements.",
                RentalPricePerDay = 5.99m,
                PurchasePrice = 45.99m,
                Quantity = 10,
                Condition = Condition.LikeNew,
                Status = Status.Available
            },
            new BoardGame
            {
                Title = "Ticket to Ride",
                ImageUrl = "/images/TicketToRide.jpg",
                Category = Category.FamilyGame,
                MinPlayers = 2,
                MaxPlayers = 5,
                Description = "A family game about building train routes across the country.",
                RentalPricePerDay = 4.99m,
                PurchasePrice = 39.99m,
                Quantity = 15,
                Condition = Condition.New,
                Status = Status.Available
            },
            new BoardGame
            {
                Title = "Pandemic",
                ImageUrl = "/images/Pandemic.jpg",
                Category = Category.FamilyGame,
                MinPlayers = 2,
                MaxPlayers = 4,
                Description = "A cooperative game where players work together to stop global outbreaks.",
                RentalPricePerDay = 6.99m,
                PurchasePrice = 49.99m,
                Quantity = 8,
                Condition = Condition.LikeNew,
                Status = Status.Available
            },
            new BoardGame
            {
                Title = "Wingspan",
                ImageUrl = "/images/Wingspan.png",
                Category = Category.StrategyGame,
                MinPlayers = 2,
                MaxPlayers = 5,
                Description = "Wingspan is a competitive, medium-weight, card-driven, engine-building board game.",
                RentalPricePerDay = 9.99m,
                PurchasePrice = 59.99m,
                Quantity = 3,
                Condition = Condition.LikeNew,
                Status = Status.Available
            },
            new BoardGame
            {
                Title = "7 Wonders",
                ImageUrl = "/images/7Wonders.jpg",
                Category = Category.StrategyGame,
                MinPlayers = 2,
                MaxPlayers = 7,
                Description = "Gather resources, develop commercial routes, and affirm your military supremacy.",
                RentalPricePerDay = 4.99m,
                PurchasePrice = 49.99m,
                Quantity = 1,
                Condition = Condition.Used,
                Status = Status.Available
            },
            new BoardGame
            {
                Title = "Root",
                ImageUrl = "/images/Root.jpg",
                Category = Category.StrategyGame,
                MinPlayers = 2,
                MaxPlayers = 4,
                Description = "A game of adventure and war in which 2 to 4 players battle for control of a vast wilderness.",
                RentalPricePerDay = 4.99m,
                PurchasePrice = 49.99m,
                Quantity = 1,
                Condition = Condition.Used,
                Status = Status.Available
            }
            /*new BoardGame
            {
                Title = "Root2",
                ImageUrl = "/images/Root.jpg",
                Category = Category.StrategyGame,
                MinPlayers = 2,
                MaxPlayers = 4,
                Description = "A game of adventure and war in which 2 to 4 players battle for control of a vast wilderness.",
                RentalPricePerDay = 4.99m,
                PurchasePrice = 49.99m,
                Quantity = 1,
                Condition = Condition.Used,
                Status = Status.Unavailable
            }*/
        );

        await dbContext.SaveChangesAsync();
    }
    
}
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
