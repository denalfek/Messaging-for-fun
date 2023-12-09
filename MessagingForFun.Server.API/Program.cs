using MessagingForFun.Server.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IRedisService, RedisService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    if (configuration.GetSection("Redis").GetSection("ConnectionString").Value is not { } connectionString)
        throw new Exception("Redis connection string is not configured.");
    return new RedisService(connectionString);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
