using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ExpressionTreeUpdate_EF6
{
    public class GenericRepository<T> where T : class
    {
        private EfSandboxDbContext _dbContext;
        private DbSet<T> _dbSet;

        public GenericRepository(EfSandboxDbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> GetData(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public async Task Update(T entity, Expression<Func<T, object>> propsToUpdate, CancellationToken cancellationToken = default)
        {
            var members = ((NewExpression)propsToUpdate.Body).Members;
            if (members == null) throw new InvalidOperationException();

            foreach (var info in members)
            {
                _dbContext.Entry(entity).Property(info.Name).IsModified = true;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
