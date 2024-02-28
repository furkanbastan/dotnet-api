using Microsoft.EntityFrameworkCore;
using App.Api.Extensions;
using App.Api.Filters;

var builder = WebApplication.CreateBuilder(args);
var appConnectionString = builder.Configuration.GetConnectionString("local")!.ToString();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ValidationFilter>();
}).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddCorsServices(builder.Configuration);
builder.Services.AddLayerServices(appConnectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.ConfigureCors(app.Configuration);

app.ConfigureGlobalExceptionHandler();
app.MapControllers();

app.Run();