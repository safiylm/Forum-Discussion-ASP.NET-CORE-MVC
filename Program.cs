using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Forum_descussion_ASP.NET_core_mvc.Data;
using Forum_descussion_ASP.NET_core_mvc.Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ForumContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("Forummcontext")));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    Class.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
