using WardrobeInventory.Database;
using WardrobeInventory.Models;
using WardrobeInventory.Repositories;
using WardrobeInventory.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<WardrobeContext>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5076") });

builder.Services.AddTransient<IRepository<Cloth>, ClothRepository>();
builder.Services.AddTransient<IService<Cloth>, ClothService>();

builder.Services.AddTransient<IRepository<Category>, CategoryRepository>();
builder.Services.AddTransient<IService<Category>, CategoryService>();

builder.Services.AddTransient<IRepository<Set>, SetRepository>();
builder.Services.AddTransient<IService<Set>, SetService>();

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
