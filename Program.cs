using Autofac.Extensions.DependencyInjection;
using Autofac;
using FoodDonationAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using FoodDonationAPI.Common;
using FoodDonationAPI.Models;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Core;
using Microsoft.AspNetCore.Identity;
using FoodDonationAPI.Services;
using FoodDonationAPI.Services.Services;
using FoodDonationAPI.Repositories.IRepositories;
using FoodDonationAPI.Repositories;
using FoodDonationAPI.Helpers;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;




// Add services to the container.

builder.Services.AddControllers();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutoFacConfig());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Food Donation", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
});

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        //ValidateAudience = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["JwtSecurityToken:Issuer"],
        // ValidAudience = Configuration["JwtSecurityToken:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityToken:Key"]))
    };

});

builder.Services.AddDbContext<FoodDonationDbContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("MainDB")
));

// For Identity
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FoodDonationDbContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddTransient<IUserRepository, UserRepository>();
//builder.Services.AddTransient<ITestRepository, TestRepository>();
//builder.Services.AddTransient<ITest1Repository, Test1Repository>();

//builder.Services.AddIdentity<ApplicationUser, ApplicationRole>();

//builder.Services.AddIdentityCore<ApplicationUser>();
//    //.AddUserManager<ApplicationUser>();

var app = builder.Build();

//var env = app.Environment;

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "foodDonation"));
//app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "foodDonation"));
//}

EmailTemplates.Initialize((IWebHostEnvironment)app.Environment, (IConfigurationRoot)configuration);
SQLHelpers.Initialize((IConfigurationRoot)configuration);

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

//app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
