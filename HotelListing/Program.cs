using HotelListing.Data.Database;
using HotelListing.Data.IUnit;
using HotelListing.Data.MapperProfile;
using HotelListing.Data.Unity;
using HotelListing.ServiceExtensions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(o => {
    o.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddAutoMapper(typeof(MapperInitializer));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// --------------- Configure Serilog -----------------------------------------------------------
var isDevelopment = builder.Environment.IsDevelopment();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        path: "c:\\hotellisting\\logs\\log-.txt",
        outputTemplate: "{Timestamp:yyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: isDevelopment ? LogEventLevel.Debug : LogEventLevel.Information
    ).CreateLogger();

builder.Host.UseSerilog();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"))
);
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = 
    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Application Is Starting");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "ApplicationBuilder Failed to Start");
    throw;
}
finally
{
    Log.CloseAndFlush();
}

