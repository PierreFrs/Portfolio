using Portfolio.Configuration;
using Portfolio.Database;
using Portfolio.Services;

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
