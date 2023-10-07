using BookingApp;
using BookingApp.Models;
using BookingApp.Services;
using BookingApp.Services.Interfaces;
using BookingApp.Sieve;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using Sieve;
using Swashbuckle.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISieveProcessor, AppSieveProcessor>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddSingleton<AppSieveProcessor>();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddControllers();
//builder.Services.AddSwaggerGen();

builder.Services.Configure<FormOptions>(options =>
{
	options.MultipartBodyLengthLimit = 104857600;
});
builder.Services.AddDbContext<AppDbContext>(builder =>
{
	builder.UseSqlServer(@"Data Source=DESKTOP-R5C9EQ0\SQLEXPRESS;Initial Catalog=DbContextBooking;Integrated Security=True");
});

builder.Services.AddIdentity<UserModel, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 2;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDbContext>();
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

app.UseAuthorization();
app.UseAuthentication();
//app.UseSwagger();
/*
app.UseSwaggerUI(options =>
{
	options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
	options.RoutePrefix = string.Empty;
	options.DocumentTitle = "My swagger";
});
*/
app.MapPost("sieve", async ([FromBody] SieveModel query,ISieveProcessor sieveProcessor, AppDbContext context)=>
{

	var offers = context.OfferList
	.AsQueryable();

	var Goodoffers = await sieveProcessor
	.Apply(query, offers)
	.ToListAsync();

	var totalcount = await sieveProcessor
	.Apply(query, offers, applyPagination: false, applySorting: false)
	.CountAsync();

	var result = new PagedResult<Offer>(Goodoffers, totalcount, query.PageSize.Value, query.Page.Value);
	return result;
});


app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
