using TricolorSonda.Api.Infra;
using TricolorSonda.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
var mongoConnectionString = Environment.GetEnvironmentVariable("MongoDbConnectionString");

if (string.IsNullOrWhiteSpace(mongoConnectionString))
{
    mongoConnectionString = builder.Configuration["MongoDbSettings:ConnectionString"];
}

builder.Services.Configure<MongoDbSettings>(options =>
{
    options.ConnectionString = mongoConnectionString!;
    options.DatabaseName = builder.Configuration.GetSection("MongoDbSettings:DatabaseName").Value!;
});

builder.Services.AddSingleton<MongoContext>();

builder.Services.AddScoped<TransferService>();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
