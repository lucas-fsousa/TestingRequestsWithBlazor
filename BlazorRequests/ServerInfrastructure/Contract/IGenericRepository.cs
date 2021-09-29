using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Infrastructure.Contract {
  public interface IGenericRepository<TEntity> where TEntity : class {
    public ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    public void Update(TEntity entity);

    public void Delete(TEntity entity);

    public ValueTask<TEntity> GetByKeysAsync(CancellationToken cancellationToken = default, params object[] keys);

    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", bool noTracking = false, int? take = null, int? skip = null);
    
    public Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", bool noTracking = false, int? take = null, int? skip = null, CancellationToken cancellationToken = default);
    
    public Task<bool> CommitAsync(CancellationToken cancellationToken = default);
  }
}
