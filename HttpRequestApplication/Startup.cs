using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Server.Infrastructure.Configuration;
using App.WebApi.Business.CarDefinition;
using App.WebApi.Business.UserDefinition;
using App.Entities;
using App.WebApi.Business.CarImagesDefinition;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace HttpRequestApplication {
  public class Startup {

    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

     public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      services.ExtensionConfigureServices(Configuration);


      // ******** Insert here *********
      services.AddCors(options => options.AddDefaultPolicy(
        builder => { builder.WithOrigins("https://localhost:44376").AllowAnyHeader().AllowAnyMethod(); }
        ));


      services.AddControllers();
      services.AddSwaggerGen(c => {
        // definition of the name of the XML file that contains the detailed information
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        // Definition of the path where the file is located
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);


        c.SwaggerDoc("v1", new OpenApiInfo { Title = "HttpRequestApplication", Version = "v1" });
      });


      services.AddScoped(typeof(ICarService<Car>), typeof(CarService<Car>));
      services.AddScoped(typeof(IUserService<User>), typeof(UserService<User>));
      services.AddScoped(typeof(ICarImageService<Photo>), typeof(CarImageService<Photo>));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if(env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HttpRequestApplication v1"));
      }
      app.UseStaticFiles();

      app.UseHttpsRedirection();

      app.UseCors(); // ******** Insert here *********
      app.UseRouting();


      app.UseAuthorization();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
    }
  }
}
