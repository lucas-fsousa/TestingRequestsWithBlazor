using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Entities;
using Server.Infrastructure.Contract;

namespace HttpRequestApplication.Business.CarDefinition {
  public class CarService<TEntity>: ICarService<Car> {
    private readonly IGenericRepository<Car> _generic;
    public CarService(IGenericRepository<Car> car) {
      _generic = car;
    }

    public async ValueTask<Car> AddAsync(Car entity) {
      var item = await _generic.GetByKeysAsync(keys: entity.Model);

      if(item != null) {
        return null;
      }

      return await _generic.AddAsync(entity);
    }

    public void Delete(int? key = 0, string? unique = null) {
      if(key > 0) {

        var entity = _generic.GetByKeysAsync(keys: key);
        if(entity.Result != null) {
          _generic.Delete(entity.Result);
          _generic.CommitAsync();
        }

      } else {

        var entity = _generic.GetByKeysAsync(keys: unique);
        if(entity.Result != null) {
          _generic.Delete(entity.Result);
          _generic.CommitAsync();
        }

      }
    }

    public async Task<IEnumerable<Car>> GetAllAsync(int currentPage) {
      int MaxPerPage = 10;
      var list = await _generic.GetAllAsync(
        noTracking: false,
        take: currentPage,
        skip: MaxPerPage
        ).ConfigureAwait(false);
      return list;
    }

    public async ValueTask<Car> GetAsyncByKey(int? key = 0, string? unique = "") {
      if(key > 0) {
        return await _generic.GetByKeysAsync(keys: key);
      }else {
        return await _generic.GetByKeysAsync(keys: unique);
      }
    }

    public async Task Update(Car entity) {
      if(entity != null) {
        _generic.Update(entity);
        await _generic.CommitAsync();
      }
    }
  }
}
