var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddCatalog(config => config.UsingMongoDB(builder.Configuration.GetConnectionString("Catalog.MongoDB")));
//builder.Services.AddCatalog(config => config.UsingPostgreSQL(builder.Configuration.GetConnectionString("Catalog.PostgreSQL")));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

//if (app.Environment.IsDevelopment())
//{
//    app.InitializeCatalogDatabase();
//}

app.Run();

public class Startup
{
}
