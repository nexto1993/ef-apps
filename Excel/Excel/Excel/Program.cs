using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Excel.Pages.Data;
using Excel.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=CodingWiki;TrustServerCertificate=True;Trusted_Connection=True;"));
builder.Services.AddControllers();
builder.Services.AddHttpClient<EmployeeService>();
builder.Services.AddHostedService<EmployeeUpdateWorker>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllers(); // Add this line
app.MapRazorPages();

app.Run();
