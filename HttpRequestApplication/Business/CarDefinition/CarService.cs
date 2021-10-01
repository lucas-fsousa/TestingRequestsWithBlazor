using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Entities;
using Server.Infrastructure.Contract;

namespace App.WebApi.Business.CarDefinition {
  public class CarService<TEntity>: ICarService<Car> {
    private readonly IGenericRepository<Car> _generic;
    public CarService(IGenericRepository<Car> car) {
      _generic = car;
    }

    public async ValueTask<Car> AddAsync(Car entity) {
      entity.Id = 0;
      entity.Manufacturer = entity.Manufacturer.ToUpper();
      entity.Color = entity.Color.ToUpper();
      entity.Model = entity.Model.ToUpper();
      entity.Name = entity.Name.ToUpper();
      var requestResult = await _generic.AddAsync(entity);
      await _generic.CommitAsync();
      return requestResult;
    }

    public void Delete(int? key = 0, string? unique = null) {
      if(key > 0) {

        var entity = _generic.GetByKeysAsync(keys: key);
        if(entity.Result != null) {
          _generic.Delete(entity.Result);
          _generic.CommitAsync();
        }
      }
    }

    public async Task<IEnumerable<Car>> GetAllAsync(int currentPage) {
      var MaxPerPage = 10;
      var skipCurrent = (currentPage - 1) * MaxPerPage;
      if(currentPage < 1) {
        currentPage = 1;
      }

      var list = await _generic.GetAllAsync(
        noTracking: false,
        take: MaxPerPage,
        skip: skipCurrent
        ).ConfigureAwait(false);
      return list;
    }

    public async ValueTask<Car> GetAsyncByKey(int? key = 0, string? unique = "") {
      if(key > 0) {
        return await _generic.GetByKeysAsync(keys: key);
      }
      return null;
    }

    public async Task Update(Car entity) {
      if(entity != null) {
        _generic.Update(entity);
        await _generic.CommitAsync();
      }
    }
  }
}
