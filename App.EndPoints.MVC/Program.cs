using Serilog;
using App.Domain.Core.Contract.AppService;
using App.Domain.Core.Contract.Repository;
using App.Domain.Core.Contract.Services;
using App.Domain.Core.Entities.User;
using App.Domain.Services.AppServices;
using App.Domain.Services.Services;
using App.Infra.DataAccess.EfCore;
using App.Infra.DataAccess.EfCore.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using System.Drawing;
using Serilog.Sinks.MSSqlServer;
using App.EndPoints.MVC.MiddleWare;

var builder = WebApplication.CreateBuilder(args);



builder.Host.ConfigureLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();

}).UseSerilog((context, config) =>
{
    config.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
    config.MinimumLevel.Override("System", LogEventLevel.Warning);
    config.MinimumLevel.Override("BrowserLink", LogEventLevel.Fatal);
    config.WriteTo.Console();
    config.WriteTo.Seq("http://localhost:5341",apiKey: "0j2hkEmc8v0O6Gih7Ueg");
});


#region alternative Log Destinations 
//builder.Host.UseSerilog((context, config) =>
//{
//	var connectionString = context.Configuration.GetConnectionString("LogsConnection");

//	config.ReadFrom.Configuration(context.Configuration).
//			WriteTo.MSSqlServer(
//				connectionString,
//				new MSSqlServerSinkOptions
//						{
//							TableName = "MyLogs",
//							AutoCreateSqlTable = true
//						}
//				).
//		   WriteTo.File("logs/app.log",rollingInterval: RollingInterval.Day).
//           WriteTo.Console();
//});


//builder.Host.UseSerilog((context, config) =>
//{
//	config.WriteTo.Console()
//		  .WriteTo.EventLog("MyHomeServiceApp", manageEventSource: true)  // Send logs to Windows Event Viewer
//		  .MinimumLevel.Information();
//});
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews(); 

builder.Services.AddMemoryCache();


builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAccountAppService, AccountAppService>();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientAppService, ClientAppService>();

builder.Services.AddScoped<IExpertRepository, ExpertRepository>();
builder.Services.AddScoped<IExpertService, ExpertService>();
builder.Services.AddScoped<IExpertAppService, ExpertAppService>();

builder.Services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();
builder.Services.AddScoped<IServiceRequestService, ServiceRequestService>();
builder.Services.AddScoped<IServiceRequestAppService, ServiceRequestAppService>();

builder.Services.AddScoped<IServiceOfferingRepository, ServiceOfferingRepository>();
builder.Services.AddScoped<IServiceOfferingService, ServiceOfferingService>();
builder.Services.AddScoped<IServiceOfferingAppService, ServiceOfferingAppService>();


builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IServiceAppService, ServiceAppService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();

builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
builder.Services.AddScoped<ISubCategoryAppService, SubCategoryAppService>();

builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IReviewAppService, ReviewAppService>();

builder.Services.AddScoped<ICityRepository, CityRepository>();


builder.Services.AddScoped<IUtilityService, UtilityService>();

//Add Identity 
builder.Services.AddIdentity<AppUser, IdentityRole<int>>()
	.AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.SignIn.RequireConfirmedEmail = false; 
});


var app = builder.Build();



app.UseMiddleware<LoggingMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.Run();
