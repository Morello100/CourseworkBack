using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

using mongodb_dotnet_example.Models;
using mongodb_dotnet_example.Services;

public class Startup
{

    
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;

         var mongoDBSettings = Configuration.GetSection("MongoDBSettings");
    Console.WriteLine($"ConnectionString: {mongoDBSettings["ConnectionString"]}");
    Console.WriteLine($"DatabaseName: {mongoDBSettings["DatabaseName"]}");
    }

    public IConfiguration Configuration { get; }

public void ConfigureServices(IServiceCollection services)
{
    
      services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
    });

    var mongoDBSettings = Configuration.GetSection("MongoDBSettings");
    var connectionString = mongoDBSettings["ConnectionString"];
    var databaseName = mongoDBSettings["DatabaseName"];

    services.AddSingleton<IUsersDatabaseSettings>(sp =>
        new UsersDatabaseSettings { ConnectionString = connectionString, DatabaseName = databaseName });

    services.AddSingleton<UsersService>(sp =>
        new UsersService(connectionString, databaseName));

    services.AddSingleton<IProductDatabaseSettings>(sp =>
        new ProductDatabaseSettings { ConnectionString = connectionString, DatabaseName = databaseName });

    services.AddSingleton<ProductService>(sp =>
        new ProductService(connectionString, databaseName));


      services.AddSingleton<IOrdersDatabaseSettings>(sp =>
        new OrdersDatabaseSettings { ConnectionString = connectionString, DatabaseName = databaseName });

    services.AddSingleton<OrdersService>(sp =>
        new OrdersService(connectionString, databaseName));
    

    services.AddSingleton<ISelectedProductsDatabaseSettings>(sp =>
        new SelectedProductsDatabaseSettings { ConnectionString = connectionString, DatabaseName = databaseName });

    services.AddSingleton<SelectedProductsService>(sp =>
        new SelectedProductsService(connectionString, databaseName));

    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "mongodb_dotnet_example", Version = "v1" });
    });

services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

}


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
    app.UseCors("CorsPolicy");
    
        if (env.IsDevelopment())
        {
        
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "mongodb_dotnet_example v1"));
            
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
