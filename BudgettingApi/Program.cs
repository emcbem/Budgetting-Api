using BudgettingApi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.WebHost.UseUrls("http://localhost:8080");

builder.Services.AddControllers();

var dbConn = builder.Configuration.GetValue<string>("DB_CONN") ?? throw new Exception("No DB_CONN found");

builder.Services.AddDbContextFactory<BudgettingDbContext>(props => props.UseNpgsql(dbConn));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://dev-013fwxix4dwe1jea.us.auth0.com";
        options.Audience = "https://dev-013fwxix4dwe1jea.us.auth0.com/api/v2/";
        options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
            };
        options.RequireHttpsMetadata = false;

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("Authentication failed:");
                Console.WriteLine(context.Exception.ToString());
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<BudgettingDbContext>>();
    var dbContext = dbContextFactory.CreateDbContext();


    // Check and apply pending migrations
    var pendingMigrations = dbContext.Database.GetPendingMigrations();
    if (pendingMigrations.Any())
    {
        Console.WriteLine("Applying pending migrations...");
        dbContext.Database.Migrate();
        Console.WriteLine("Migrations applied successfully.");
    }
    else
    {
        Console.WriteLine("No pending migrations found.");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
