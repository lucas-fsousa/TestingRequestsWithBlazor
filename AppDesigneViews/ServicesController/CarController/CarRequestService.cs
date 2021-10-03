using AppDesigneViews.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppDesigneViews.ServicesController.CarController {
  public class CarRequestService : ICarRequestService {
    HttpClient _http;
    public CarRequestService(HttpClient httpClient) {
      _http = httpClient;
    }

    public async Task DeleteByIdAsync(int id) {
      try {
        await _http.DeleteAsync($"api/v1/home/delete/{id}");
      } catch(Exception ex) {
        Console.WriteLine(ex.Message);
      }
    }

    public async Task<List<Car>> GetAllForCurrentPage(int currentPage) {
      try {
        return await _http.GetFromJsonAsync<List<Car>>($"api/v1/home/getAll/{currentPage}");
      } catch(Exception ex) {
        Console.WriteLine(ex.Message);
        return null;
      }
    }

    public async ValueTask<Car> GetCarByIdAsync(int id) {
      try {
        return await _http.GetFromJsonAsync<Car>($"api/v1/home/getbyid/{id}");
      } catch(Exception ex) {
        Console.WriteLine(ex.Message);
        return null;
      }
    }
  }
}
