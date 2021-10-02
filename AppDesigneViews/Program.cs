using AppDesigneViews.ServicesController;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
      

      builder.Services.AddScoped<IRequestService, RequestService>();

      await builder.Build().RunAsync();
    }
  }
}
