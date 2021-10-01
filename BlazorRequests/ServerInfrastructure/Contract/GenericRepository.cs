using Microsoft.EntityFrameworkCore;
using Server.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Infrastructure.Contract {
  /* This class will be in charge of doing all the database CRUD procedure.
   * Since there is an interface implementation, the class is dependent on dependency injection as it is an implementation of IGenericRepository
   */

  public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class {

    // Context Definitions
    private readonly Context _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(Context context) {
      // value assignments to context variables in read-only mode
      _context = context;
      _dbSet = _context.Set<TEntity>();
    }


    // Asynchronous method for adding entities to the database
    public async ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default) {
      var entityEntry = await _context.AddAsync(entity, cancellationToken).ConfigureAwait(false);
      return entityEntry.Entity;
    }

    // Method for confirming database modifications
    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;

    // Method for deleting information
    public void Delete(TEntity entity) => _context.Entry(entity).State = EntityState.Deleted;

    public async ValueTask DisposeAsync() => await _context.DisposeAsync().ConfigureAwait(false);

    // Method to get information from the database according to the parameters entered
    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", bool noTracking = false, int? take = null, int? skip = null) {
      IQueryable<TEntity> query = this._dbSet;

      // Parameter checking conditions
      if(noTracking) {
        query = query.AsNoTracking();
      }
        
      if(filter != null) {
        query = query.Where(filter);
      }
        
      foreach(var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
        query = query.Include(includeProperty);
      }
        
      if(skip != null) {
        query = query.Skip(skip.Value);
      }
        
      if(take != null) {
        query = query.Take(take.Value);
      }

      if(orderBy != null) {
        query = orderBy(query);
      }

      return query;
    }

    // Asynchronous method to get information from the database according to the parameters entered
    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", bool noTracking = false, int? take = null, int? skip = null, CancellationToken cancellationToken = default) {
      return await GetAll(filter, orderBy, includeProperties, noTracking, take, skip).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    // Method to fetch information from the database by identifier
    public async ValueTask<TEntity> GetByKeysAsync(CancellationToken cancellationToken = default, params object[] keys) => await _dbSet.FindAsync(keys, cancellationToken).ConfigureAwait(false);

    // Method for updating information in the database
    public void Update(TEntity entity) => _context.Entry(entity).State = EntityState.Modified;

  }
}
