
using Microsoft.EntityFrameworkCore;
using PolyFlora.API.Extensions;
using PolyFlora.API.Middlewares;
using PolyFlora.Application.MappingProfiles;
using PolyFlora.Persistence;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(FlowerProfile));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration["Postgres:DbConnectionString"];   
    options.UseNpgsql(connectionString);
});

var redisConnectionString = builder.Configuration["Redis:DbConnectionString"];
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString!));

//Extensions
builder.Services.AddApplicationServices();
builder.Services.AddJwtAuthentication(builder.Configuration);


var app = builder.Build();

app.UseMiddleware<GExceptionHandler>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization(); 

app.MapControllers();

app.Run();
