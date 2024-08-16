using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerConfig();

builder.Services.AddSerilogConfig(builder.Configuration);

builder.Services.AddJwtConfig(builder.Configuration, builder.Environment.IsDevelopment());

builder.Services.AddConfOptions(builder.Configuration);

builder.Services.AddCorsWithConfiguration(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddServices();

builder.Services.AddFluentValidationConfig();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.MapEndpoints(Assembly.GetExecutingAssembly());
app.UseAuthentication();

app.UseHttpsRedirection();

app.Run();