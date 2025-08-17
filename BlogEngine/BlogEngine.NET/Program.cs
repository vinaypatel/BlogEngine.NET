using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/login";
        options.LogoutPath = "/Account/logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(129600);
        options.SlidingExpiration = true;
        options.Cookie.Name = ".AUXBLOGENGINE";
        options.Cookie.SameSite = SameSiteMode.Strict;
    });

// Add session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add configuration manager compatibility
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

// Custom middleware to handle BlogEngine initialization
app.Use(async (context, next) =>
{
    // Equivalent to Application_BeginRequest
    // BlogEngineConfig.Initialize(context);
    await next();
});

// Custom middleware for culture setting
app.Use(async (context, next) =>
{
    // Equivalent to Application_PreRequestHandlerExecute
    // BlogEngineConfig.SetCulture(sender, e);
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Handle legacy ASPX routes
app.MapFallback("/default.aspx", async context =>
{
    context.Response.Redirect("/");
});

app.MapFallback("/post.aspx", async context =>
{
    var id = context.Request.Query["id"];
    if (!string.IsNullOrEmpty(id))
    {
        context.Response.Redirect($"/post/{id}");
    }
    else
    {
        context.Response.Redirect("/");
    }
});

app.MapFallback("/page.aspx", async context =>
{
    var id = context.Request.Query["id"];
    if (!string.IsNullOrEmpty(id))
    {
        context.Response.Redirect($"/page/{id}");
    }
    else
    {
        context.Response.Redirect("/");
    }
});

app.MapFallback("/archive.aspx", async context =>
{
    context.Response.Redirect("/archive");
});

app.MapFallback("/search.aspx", async context =>
{
    context.Response.Redirect("/search");
});

app.MapFallback("/contact.aspx", async context =>
{
    context.Response.Redirect("/contact");
});

app.Run();