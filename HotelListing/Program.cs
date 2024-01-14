using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => {
    o.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
    });

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

// --------------- Configure Serilog -----------------------------------------------------------

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

