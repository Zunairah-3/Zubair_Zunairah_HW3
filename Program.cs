//add a using statement for currency
using System.Globalization;
//add a using statement for Entity Framework Core (uncomment this line for HW3 and beyond)
using Microsoft.EntityFrameworkCore;
//add a using statement for Identity (uncomment this line for HW4 and beyond)
//using Microsoft.AspNetCore.Identity;

// (For HW3 and beyond) Uncomment these lines and replace [Your_Project_Name] with the name of your project
using Zubair_Zunairah_HW3.DAL;
using Zubair_Zunairah_HW3.Models;

//create a web application builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//TODO: Add database on Azure so you have a connection string
//TODO: (For HW3 and beyond) Add a connection string here once you have created it on Azure
String connectionString = "Server=tcp:sp26zunairahzubair.database.windows.net,1433;Initial Catalog=Zubair_Zunairah_HW3;Persist Security Info=False;User ID=MISAdmin;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

//Configure the context to use SQL Server -- this is needed for EF Core to work
//TODO: (For HW3 and beyond) Uncomment this line once you have a connection string
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

//build the app
var app = builder.Build();

//These lines allow you to see more detailed error messages
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();

//This line allows you to use static pages like style sheets and images
app.UseStaticFiles();

//This marks the position in the middleware pipeline where a routing decision is made for a URL.
app.UseRouting();

//This allows the data annotations for currency to work on Macs
app.Use(async (context, next) =>
{
    CultureInfo.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
    CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;

    await next.Invoke();
});

//This method maps the controllers and their actions to a patter for
//requests that's known as the default route. This route identifies
//the Home controller as the default controller and the Index() action
//method as the default action. The default route also identifies a 
//third segment of the URL that's a parameter named id.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();