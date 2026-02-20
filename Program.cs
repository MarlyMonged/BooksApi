
using BooksApi.Data;
using BooksApi.Exceptions;
using BooksApi.Interfaces;
using BooksApi.Repositories;
using BooksApi.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BooksApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();
            builder.Host.UseSerilog();
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IBookService,BookService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IPublisherService, PublisherService>();
            builder.Services.AddAutoMapper(cfg=> { },typeof(Program));

            var app = builder.Build();
            //DataSeeder.SeedData(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(op=>op.SwaggerEndpoint("/openapi/v1.json","V1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<CustomExceptionMiddleWare>();
            app.MapControllers();

            app.Run();
        }
    }
}
