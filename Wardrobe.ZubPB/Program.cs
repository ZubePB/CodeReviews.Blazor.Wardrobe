using Microsoft.EntityFrameworkCore;
using WardrobeInventory.Database;
using WardrobeInventory.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<WardrobeContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("WardrobeContext")));
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetConnectionString("HttpClient")!) });

builder.Services.AddTransient<ClothService>();
builder.Services.AddTransient<CategoryService>();
builder.Services.AddTransient<SetService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Cloth}/{action=GetAll}/{id?}");

app.UseHttpMethodOverride();

app.Run();
