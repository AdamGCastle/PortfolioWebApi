using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var policyName = "AllowReactAndAngularApps";
var builder = WebApplication.CreateBuilder(args);

IConfigurationSection _section = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AuthenticationSettings");

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(policyName, policyBuilder => policyBuilder
            .WithOrigins(
                //"http://localhost:3000", 
                //"http://localhost:4200", 
                "https://takeasurvey.acprojects.ip-ddns.com",
                "https://takeasurvey.acprojects.ip-ddns.com/",
                "https://www.takeasurvey.acprojects.ip-ddns.com",
                "https://makeasurvey.acprojects.ip-ddns.com",
                "https://makeasurvey.acprojects.ip-ddns.com/",
                "https://www.makeasurvey.acprojects.ip-ddns.com",
                "https://witty-island-04b8c5b03.5.azurestaticapps.net",
                "https://witty-island-04b8c5b03.5.azurestaticapps.net/",
                "https://white-sea-00426ad03.5.azurestaticapps.net",
                "https://white-sea-00426ad03.5.azurestaticapps.net/"
            )
            //.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
        );
});

builder.Services.AddAuthentication(cfg => 
    {
        cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x => 
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = false;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8
                .GetBytes(_section["JWT_Secret"])
            ),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
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
