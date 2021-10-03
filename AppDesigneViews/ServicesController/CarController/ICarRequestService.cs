using AppDesigneViews.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDesigneViews.ServicesController.CarController {
  public interface ICarRequestService {
    public ValueTask<Car> GetCarByIdAsync(int id);
    public Task<List<Car>> GetAllForCurrentPage(int currentPage);
    public Task DeleteByIdAsync(int id);
  }
}
