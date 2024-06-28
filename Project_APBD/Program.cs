using System.Text;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project_APBD.Contexts;
using Project_APBD.Endpoints;
using Project_APBD.Enums;
using Project_APBD.RequestModels;
using Project_APBD.Services;
using Project_APBD.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddHttpClient();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddValidatorsFromAssemblyContaining<NewIndividualCustomerValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<NewCompanyCustomerValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateIndividualCustomerValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCompanyCustomerValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<NewContractValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<NewPaymentValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<NewUserValidator>();
builder.Services.AddDbContext<DatabaseContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("LoggedUser", policy => policy.RequireRole(UserRoles.LoggedUser.ToString(), UserRoles.Admin.ToString()));
    options.AddPolicy("Admin", policy => policy.RequireRole(UserRoles.Admin.ToString()));
});

builder.Services.AddAuthentication().AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.RegisterCustomerEndpoint();
app.RegisterContractEndpoint();
app.RegisterUserEndpoint();
app.Run();
