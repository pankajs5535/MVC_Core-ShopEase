using Microsoft.EntityFrameworkCore;
using Shop.DataAccess;
using Shop.DataAccess.Repository;
using Shop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
//using Microsoft.Build.Framework;
using Shop.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<MyDbContext>();
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<MyDbContext>().AddDefaultTokenProviders();


// for AccessDenied
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});



// We Add Manully For Razor Pages
builder.Services.AddRazorPages();

/* This Eroor Solve Below InvalidOperationException: Unable to resolve service for type 
'Shop.DataAccess.Repository.IRepository.ICategoryRepository' while attempting to activate 'ShopEase.Controllers.CategoryController' */

//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); we modified this ..below
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// For Email
builder.Services.AddScoped<IEmailSender,EmailSender>();


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


// We Must Added
// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Map Razor Pages
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();



 