using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Zust.Business.Abstract;
using Zust.Business.Concrete;
using Zust.DataAccess.Abstract;
using Zust.DataAccess.Concrete;
using Zust.Entity.Data;
using Zust.Entity.Entities;
using Zust.WebUI.Hubs;

var builder = WebApplication.CreateBuilder(args);




var conn = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ZustDbContext>(opt =>
{
    opt.UseSqlServer(conn); 
});

builder.Services.AddControllersWithViews()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserDal, UserDal>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IFriendDal, FriendDal>();
builder.Services.AddScoped<IFriendService, FriendService>();
builder.Services.AddScoped<IPostDal, PostDal>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


builder.Services.AddIdentity<CustomUser, CustomRole>()
    .AddEntityFrameworkStores<ZustDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSignalR();

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

 
app.UseCors("AllowAllOrigins");
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapHub<ChatHub>("/chathub");


app.Run();
