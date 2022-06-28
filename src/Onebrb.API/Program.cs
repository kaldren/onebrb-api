using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Onebrb.Core.Interfaces;
using Onebrb.Core.Services;
using Onebrb.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureADB2C"));

//builder.Services.AddControllers().AddOData(options => options.Select().Filter().OrderBy());

// Adds Microsoft Identity platform (Azure AD B2C) support to protect this Api

// End of the Microsoft Identity platform block    

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Data
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDbContext<OnebrbDbContext>(options =>
  //options.UseSqlServer(builder.Configuration.GetConnectionString("OnebrbContext"))
  options.UseInMemoryDatabase("Onebrb")
);

// DI
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(OnebrbGenericRepository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IProfileService, ProfileService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOnebrbSPA", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

// Lowercase routes
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<OnebrbDbContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
