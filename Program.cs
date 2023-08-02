using Portfolio.Configuration;
using Portfolio.Database;
using Portfolio.Models;
using Portfolio.Services;
using Amazon.S3;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<MongoDbContext>();
builder.Services.Configure<Settings>(options =>
{
    options.ConnectionString = builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value;
    options.Database = builder.Configuration.GetSection("MongoDbSettings:Database").Value;
    options.JwtKey = builder.Configuration["JwtKey"];
    options.AdminUserName = builder.Configuration["AdminUsername"];
    options.AdminPassword = builder.Configuration["AdminPassword"];
    options.ShouldSetupAdminUser = builder.Configuration["ShouldSetupAdminUser"];
});
builder.Services.AddScoped<AdminSetupService>();
builder.Services.AddScoped<AdminSetupHostedService>(); 
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<CVService>();
builder.Services.AddScoped<UserPicture>();

// Retrieve the AWS S3 configuration from appsettings.json
var awsOptions = builder.Configuration.GetAWSOptions();

// Add the AWS S3 client to the service collection
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonS3>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
