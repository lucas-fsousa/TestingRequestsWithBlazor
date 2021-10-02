using AppDesigneViews.Entities;
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

    public async Task<List<Car>> GetAllForCurrentPage(int currentPage) {
      var result = await _http.GetFromJsonAsync<List<Car>>($"api/v1/home/getAll/{currentPage}");
      return result;
    }

    public async ValueTask<Car> GetCarByIdAsync(int id) {
      var result = await _http.GetFromJsonAsync<Car>($"api/v1/home/getbyid/{id}");
      return result;
    }
  }
}
