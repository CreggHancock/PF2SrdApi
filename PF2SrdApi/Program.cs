using MongoDB.Driver;
using PF2SrdApi;
using PF2SrdApi.Models;
using PF2SrdApi.Models.Spells;
using PF2SrdApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("Database"));

builder.Services.AddTransient<ApiService>();
builder.Services.AddTransient<ApiRepository>();

var dbOptions = builder.Configuration.GetSection("Database").Get<DatabaseSettings>()
    ?? throw new InvalidProgramException("Database options are required");

builder.Services.AddSingleton(sp =>
{
    var mongoClient = new MongoClient(dbOptions.ConnectionString);
    return mongoClient.GetDatabase(dbOptions.DatabaseName);
});

builder.Services.AddSingleton(sp =>
{
    var mongoClient = new MongoClient(dbOptions.ConnectionString);
    var database = mongoClient.GetDatabase(dbOptions.DatabaseName);
    return database.GetCollection<Spell>(Spell.TableName);
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMongoDbProjections();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.Run();
