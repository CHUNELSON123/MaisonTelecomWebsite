using MaisonTelecom.Components;
using MaisonTelecom.Data;
using MaisonTelecom.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// --- CRITICAL FIX: Add this line to enable Authentication State ---
builder.Services.AddCascadingAuthenticationState();
// -----------------------------------------------------------------

// 1. Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Register Factory for Blazor Components
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register Standard Context for Identity/Controllers
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// 2. Add Identity (Users & Roles)
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 3. Configure Cookie Paths
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/admin/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/admin/login";
});

// 4. Register Services
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CampayService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<BasketState>();
builder.Services.AddScoped<StateContainer>();
builder.Services.AddHttpContextAccessor();

// 5. Add Controllers WITH Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// 6. Enable Auth Middleware
app.UseAuthentication();
app.UseAuthorization();

// 7. Map Endpoints
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();