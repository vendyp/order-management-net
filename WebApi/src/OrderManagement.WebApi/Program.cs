using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderManagement.Core.Abstractions;
using OrderManagement.Infrastructure;
using OrderManagement.Infrastructure.Databases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SqlServerDbContext>(
    x => x.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<SqlServerDbContext>());
builder.Services.AddSingleton<ContextAccessor>();
builder.Services.AddTransient(sp => sp.GetRequiredService<ContextAccessor>().Context!);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers(options =>
    {
        options.Conventions.Add(
            new CustomRouteToken(
                "namespace",
                c => c.ControllerType.Namespace?.Split('.').Last()
            ));
        options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
    })
    .AddJsonOptions(options =>
    {
        //remove based from discussion
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API", Version = "v1" });
    c.CustomSchemaIds(type => type.ToString());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use((ctx, next) =>
{
    ctx.RequestServices.GetRequiredService<ContextAccessor>().Context = new Context(ctx);

    return next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();