using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HttpRequestApplication.Business.CarDefinition {
  public interface ICarService<TEntity> where TEntity : class {
    public ValueTask<TEntity> AddAsync(TEntity entity);
    public ValueTask<TEntity> GetAsyncByKey(int? key = 0, string? unique = "");
    public Task<IEnumerable<TEntity>> GetAllAsync(int currentPage);
    public Task Update(TEntity entity);
    public void Delete(int? key = 0, string? unique = "");
  }
}
