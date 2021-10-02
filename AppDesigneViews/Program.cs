using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using AppDesigneViews.ServicesController.UserController;
using AppDesigneViews.ServicesController.CarController;

namespace AppDesigneViews {
  public class Program {
    public static async Task Main(string[] args) {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      builder.RootComponents.Add<App>("#app");

      //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

      //base addres from App.WebApi
      builder.Services.AddScoped(sp => new HttpClient {
        BaseAddress = new Uri("https://localhost:44333/")
      });
      

      builder.Services.AddScoped<IUserRequestService, UserRequestService>();
      builder.Services.AddScoped<ICarRequestService, CarRequestService>();

      await builder.Build().RunAsync();
    }
  }
}
