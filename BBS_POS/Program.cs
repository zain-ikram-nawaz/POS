using BBS_POS.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(row =>
    row.UseSqlServer(
        builder.Configuration.GetConnectionString("conn"),
        opt => opt.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        )
    )
);
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddSession(row => row.IdleTimeout = TimeSpan.FromHours(5));
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}");

app.Run();