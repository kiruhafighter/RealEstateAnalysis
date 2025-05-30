using DataAccess;
using RealEstateAnalysis.Endpoints;
using RealEstateAnalysis.Utils;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddBusinessLogicServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiSwaggerDocument(builder.Configuration);
builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.AddApiAuthorizationForUser();
builder.Services.AddApiCors(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseApiCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseApiEndpoints();

app.Run();