using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Infrastructure.Configuration;
using App.WebApi.Business.CarDefinition;
using App.Entities;
using System.Reflection;
using System.IO;

namespace HttpRequestApplication {
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      services.ExtensionConfigureServices(Configuration);

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
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if(env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HttpRequestApplication v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
    }
  }
}
