using AppDesigneViews.Entities;
using AppDesigneViews.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppDesigneViews.ServicesController.UserController {
  public class UserRequestService : IUserRequestService {
    private readonly HttpClient _http;
    public UserRequestService(HttpClient http) {
      _http = http;
    }

    public async Task Create(User user) {
      try {
        await _http.PostAsJsonAsync("api/user/register", user);
      } catch(Exception ex) {
        string x;
        Console.WriteLine(ex.Message);
      }

    }

    public async ValueTask<User> LogOn(UserLoginModel loginModel) {
      try {
        //return await _http.GetFromJsonAsync<User>($"api/user/login");
        var result = await _http.PostAsJsonAsync<UserLoginModel>("api/user/login", loginModel);
        return result.Content.ReadFromJsonAsync<User>().Result;

      } catch(Exception ex) {
        string x = ex.Message;
        Console.WriteLine(ex.Message);
        return null;
      }
    }
  }
}
