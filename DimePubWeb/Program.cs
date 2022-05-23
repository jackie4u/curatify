using Microsoft.EntityFrameworkCore;
using FeedDataLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FeedDataContext>(opts => {
    opts.UseSqlServer(
        builder.Configuration.GetConnectionString("DimePubConnection"));
});

builder.Services.AddScoped<IFeedRepository, EFFeedRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute("page", "Page{episodePage:int}",
    new { Controller = "Home", action = "Index", episodePage = 1 });

app.MapControllerRoute("pagination",
    "Episodes/Page{episodePage}",
    new { Controller = "Home", action = "Index" });

app.MapDefaultControllerRoute();

DimePubWeb.Models.SeedData.EnsurePopulated(app);

app.Run();
