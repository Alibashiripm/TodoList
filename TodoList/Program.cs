using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using TodoList.Models;
using TodoList.Models.Context;
using TodoList.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
.AddRazorRuntimeCompilation();

builder.Services.AddDbContextPool<TodoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddTransient<TodoContext>();
builder.Services.AddTransient<ITodoService, TodoService>();
//builder.Services.AddIdentityCore<TodoUser>()
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<TodoContext>();

builder.Services.AddIdentity<TodoUser, IdentityRole>()
      .AddEntityFrameworkStores<TodoContext>()
      .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;


});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
});
var app = builder.Build();
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
    pattern: "{controller=TodoLists}/{action=Index}/{id?}");

app.Run();
