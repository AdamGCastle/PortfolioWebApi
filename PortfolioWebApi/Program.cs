using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Builder;

var policyName = "AllowReactApp";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(policyName, policyBuilder => policyBuilder
            .WithOrigins("https://takeasurvey.acprojects.ip-ddns.com", "https://makeasurvey.acprojects.ip-ddns.com", "witty-island-04b8c5b03.5.azurestaticapps.net", "white-sea-00426ad03.5.azurestaticapps.net")
            .AllowAnyHeader()
            .AllowAnyMethod()
        );
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseCors(policyName);

app.MapControllers();

app.Run();
