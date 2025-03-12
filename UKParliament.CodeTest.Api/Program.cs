using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using UKParliament.CodeTest.Api.Services;
using UKParliament.CodeTest.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidAudience = builder.Configuration["Jwt:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//        };
//    });

builder.Services.AddAuthorization();

// Register PersonManagerContext and PersonService based on environment
if (builder.Environment.IsDevelopment())
{
    // Use in-memory database and register as singleton in development so the data persists between calls
    builder.Services.AddDbContext<PersonManagerContext>(op => op.UseInMemoryDatabase("PersonManager"), ServiceLifetime.Singleton);
}
else
{
    // Uncomment and configure the connection string for non-development environments
    // builder.Services.AddDbContext<PersonManagerContext>(options =>
    //     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));    
}
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddValidatorsFromAssemblyContaining<PersonService>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddTransient<DatabaseSeeder>();
}

// Add development environment CORS policy - do not use in production due to security vulnerabilities
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentCorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    seeder.Seed();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors("DevelopmentCorsPolicy");

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

