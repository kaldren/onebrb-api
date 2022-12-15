using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Onebrb.Application;
using Onebrb.Application.Comments.Queries.GetSingleCommentByCommentId;
using Onebrb.Application.Interfaces;
using Onebrb.Infrastructure.Data;
using Onebrb.Infrastructure.Interfaces;
using Onebrb.Infrastructure.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
string environment = builder.Environment.EnvironmentName;

//Retrieve the Connection String from the secrets manager 
//var connectionString = builder.Configuration.GetConnectionString("AppConfig");

//builder.Host.ConfigureAppConfiguration(builder =>
//{
//    //Connect to your App Config Store using the connection string
//    builder.AddAzureAppConfiguration(options =>
//                options
//                    .Connect(connectionString)
//                    // Load configuration values with no label
//                    .Select(KeyFilter.Any, LabelFilter.Null)
//                    // Override with any configuration values specific to current hosting env
//                    .Select(KeyFilter.Any, environment)
//            );
//});

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.AddControllers().AddOData(options => options.Select().Filter().OrderBy().SetMaxTop(5));

builder.Services.AddControllers()
    //.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetSingleCommentByCommentIdValidator>());


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Data
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDbContext<OnebrbDbContext>(options =>
{
    options.UseLazyLoadingProxies();
    //options.UseSqlServer(builder.Configuration.GetConnectionString("OnebrbContext"));
    options.UseNpgsql(builder.Configuration.GetConnectionString("OnebrbContext"));
    //options.UseInMemoryDatabase("Onebrb");
});

// DI
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(OnebrbGenericRepository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAzureImageStoreService, AzureImageStoreService>();
builder.Services.AddTransient<IAzureQueueService, AzureQueueService>();
builder.Services.AddTransient<INotificationsService, NotificationsService>();


// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOnebrbSPA", policy =>
    {
        policy.WithOrigins(builder.Configuration.GetValue<string>("Onebrb:SPAClient:Domain"))
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

// Lowercase routes
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddApplicationServices();

builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, IdBasedUserIdProvider>();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();

        var logger = loggerFactory.CreateLogger<ILogger>();


        logger.LogInformation("Creating test data...");
        var context = services.GetRequiredService<OnebrbDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        DbInitializer.Initialize(context);
        logger.LogInformation("Test data inserted successfully...");
    }
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    loggerFactory.AddFile($@"{Directory.GetCurrentDirectory()}\Logs\log.txt");
}

app.UseCors(options => options.WithOrigins(builder.Configuration.GetValue<string>("Onebrb:SPAClient:Domain")).AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationsHub>("api/notifications-hub");

app.Run();