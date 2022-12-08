using Microsoft.EntityFrameworkCore.Storage;
using System.Collections;
using System.Text;
using UrlCutter.Models;




/*

string token = "1234";
token = token.Insert(token.Length, new string('0', 7 - token.Length));
return 0;





byte[] nBa = { 49, 0, 66, 0, 88, 0, 111, 0, 66, 0, 104, 0, 105, 0, 75, 0, 119, 0};
byte[] nBaa = { 49, 0, 66, 0, 88, 0, 111, 0, 66, 0, 104, 0, 105, 0, 75, 0, 119, 1};
byte[] nBaaa = { 49, 0, 66, 0, 88, 0, 111, 0, 66, 0, 104, 0, 105, 0, 75, 0, 119, 255};
Array.Reverse(nBaa, 0, nBaa.Length);
//15


string hashStr = Encoding.Unicode.GetString(nBa);
string hashStr1 = Encoding.Unicode.GetString(nBaa);
string hashStr2 = Encoding.Unicode.GetString(nBaaa);

Console.WriteLine(hashStr+" : "+hashStr1 +" : " + hashStr2+ "\n\n\n");

byte ns = 1;
nBa[0] += ns;
BitArray nB = new BitArray(nBa);

var s = GetInt(nBa);

BitArray iB= new BitArray(s);

PrintValues(iB, 16);
Console.WriteLine("\n\n");

PrintValues(nB, 8);

static void PrintValues(IEnumerable myList, int myWidth)
{
    int i = myWidth;
    foreach (Object obj in myList)
    {
        if (i <= 0)
        {
            i = myWidth;
            Console.WriteLine();
        }
        i--;
        Console.Write("{0,8}", obj);
    }
    Console.WriteLine();
}

static byte[] GetInt(byte[] i)
{
    byte[] sd = { byte.MinValue };
    foreach (var a in i)
    {
        sd[0] += a;
    }
    return sd;
}

return 0;*/

var builder = WebApplication.CreateBuilder(args);
//DbUrl db = new();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc(options => { options.EnableEndpointRouting = false; });

builder.Services.AddDbContext<DbUrl>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.RequestHeaderEncodingSelector = (_) => Encoding.UTF8;
    options.ResponseHeaderEncodingSelector = (_) => Encoding.UTF8;
});


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