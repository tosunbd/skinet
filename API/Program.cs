using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDbConnection"));
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped( typeof(IGenericRepository<>), typeof(GenericRepository<>) );
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Data Input 

// using var scope = app.Services.CreateScope();
// var services = scope.ServiceProvider;
// var context = services.GetRequiredService<StoreContext>();
// var logger = services.GetRequiredService<ILogger<Program>>();
// // var loggerFactory = services.GetRequiredService<ILoggerFactory>();

// try
// {
//     await context.Database.MigrateAsync();
//     // await StoreContextSeed.SeedAsync(context, loggerFactory);
//     await StoreContextSeed.SeedAsync(context);
// }
// catch (Exception ex)
// {
//    logger.LogError(ex, "An error occured during migration");
// }

// End of Data Input 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseRouting();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
