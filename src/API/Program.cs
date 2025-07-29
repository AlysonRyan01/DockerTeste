using API.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.AddCorsConfiguration();
builder.AddSqlServer();
builder.AddAuthentication();
builder.AddJwtService();
builder.ConfigureJsonSerializer();
builder.AddDependencies();
builder.AddSwagger();

var app = builder.Build();
app.AddExceptionMiddleware();
app.UseCors(builder.Configuration["Cors:PolicyName"]!);
app.AddSwagger();
app.AddMigrations();
app.AddAuthorization();
app.AddEndpoints();

app.MapGet("/", () => "API RODANDO");

app.Run();