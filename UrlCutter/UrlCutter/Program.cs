using System.Text;
using UrlCutter;
using UrlCutter.Factory;
using UrlCutter.Managers;
using UrlCutter.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc(options => { options.EnableEndpointRouting = false; });

builder.WebHost.ConfigureKestrel(options =>
{
    options.RequestHeaderEncodingSelector = (_) => Encoding.UTF8;
    options.ResponseHeaderEncodingSelector = (_) => Encoding.UTF8;
});

builder.Services.AddDbContext<DbUrl>(options => 
        new DatabaseFactory(builder.Configuration)
        .CreateConnection()
        .UseDatabase(options)
);

builder.Services.AddScoped<DbRepositoryManager>();
builder.Services.AddScoped<HashManager>();
builder.Services.AddScoped<UrlManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthorization();

app.UseStaticFiles();

app.UseRouting();
app.UseMvc(options =>
{
    options.MapRoute(
                    "Default", "{controller}/{action}/{id}",
                    new { controller = "Home", action = "Index", id = "" }
                    );
});

app.Run();
