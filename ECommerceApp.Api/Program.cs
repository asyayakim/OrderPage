using System.Security.Claims;
using System.Text;
using ECommerceApp.ApplicationLayer;
using ECommerceApp.ApplicationLayer.Interfaces;
using ECommerceApp.ApplicationLayer.Services;
using ECommerceApp.Domain;
using ECommerceApp.Domain.Discounts;
using ECommerceApp.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Order.Infrastructure.Persistence;
using Order.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<FruitDiscount>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPlaceOrderHandler, PlaceOrderHandler>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ProductImporter>();
builder.Services.AddScoped<ProductServer>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<ProductCategorizer>();


builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        o.JsonSerializerOptions.DefaultIgnoreCondition =
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Enter your JWT token"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "BearerAuth"
                }
            },
            new List<string>()
        }
    });
});
builder.Services.AddIdentity<UserData, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        o => o.UseVector())
);


builder.Services.AddIdentityCore<UserData>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
    })
    .AddRoles<AppRole>()
    .AddSignInManager<SignInManager<UserData>>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:JwtSecret"]!)),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
            ValidateAudience = true,
            ValidAudience = "authenticated",
            ValidateLifetime = true,
            RoleClaimType = ClaimTypes.Role,
            NameClaimType = ClaimTypes.NameIdentifier,
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token validated");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();


builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<PlaceOrderHandler>();
builder.Services.AddScoped<FruitDiscount>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
var app = builder.Build();

bool enableSwaggerInProd = builder.Configuration.GetValue<bool>("EnableSwaggerInProd");
if (app.Environment.IsDevelopment() || enableSwaggerInProd)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API V1");
    });
}
//local test
// app.UseSwagger();
// app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API V1"); });
//

app.UseHttpsRedirection();

app.UseCors("AllowFrontend"); 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));
//
app.MapControllers();
app.Run();