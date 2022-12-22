using API.Infra;
using API.Mappers;
using API.Services;
using API.Services.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
});

#region [Database]
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
#endregion

#region [HealthCheck]
builder.Services.AddHealthChecks()
    .AddMongoDb(builder.Configuration.GetSection("DatabaseSettings:ConnectionString").Value + "/" +
                builder.Configuration.GetSection("DatabaseSettings:DatabaseName").Value,
                name: "mongodb",
                tags: new string[] { "db", "data" });

builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(15); //tempo em segundos que fica validando
    options.MaximumHistoryEntriesPerEndpoint(60); //maximo de tempo de historico
    options.SetApiMaxActiveRequests(1); //maximo de requests
    options.AddHealthCheckEndpoint("default api", "/health"); //rota que sera mapeada
}).AddInMemoryStorage();
#endregion

#region [DI]
builder.Services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IGalleryService, GalleryService>();

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton<ICacheService, CacheService>();
#endregion

#region [AutoMapper]
builder.Services.AddAutoMapper(typeof(ViewModelToEntityProfile), typeof(EntityToViewModelProfile));
#endregion

#region [Cors]
builder.Services.AddCors();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso .NET 6 com MongoDB e Redis"));
}

app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}).UseHealthChecksUI(options => options.UIPath = "/healthui");

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
