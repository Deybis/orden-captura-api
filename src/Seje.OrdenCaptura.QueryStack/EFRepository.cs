using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.QueryStack
{
    public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class
    {
        private readonly OrdenCapturaDbContext _dbContext;

        public EfRepository(OrdenCapturaDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            if (predicate != null)
                return await _dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync(cancellationToken);
            return null;
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query;
            if (predicate == null) query = _dbContext.Set<T>();
            else query = _dbContext.Set<T>().Where(predicate);
            foreach (Expression<Func<T, object>> include in includes)
            {
                query.Include(include);
            }
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
