using BookStoreApi.Data;
using BookStoreApi.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BookContext>(opt =>
    opt.UseInMemoryDatabase("BookList"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStoreApi");
        c.RoutePrefix = "swagger";
    });
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

app.MapControllers();

// Seed initial books
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookContext>();

    if (!context.Books.Any())
    {
        context.Books.AddRange(
            new Book
            {
                Title = "Місто",
                Author = "Валер’ян Підмогильний",
                PublishedYear = 1927,
                Genre = "Роман",
                Price = 200
            },
            new Book
            {
                Title = "Тигролови",
                Author = "Іван Багряний",
                PublishedYear = 1944,
                Genre = "Пригодницький",
                Price = 180
            }
        );

        context.SaveChanges();
    }
}


app.Run();
