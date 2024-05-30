using Blank.Domain.Entities;
using Blank.Domain.Interfaces.Repositories;
using Blank.Infraestructure.Data.Context;
using Microsoft.EntityFrameworkCore;
namespace Blank.Infraestructure.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : EntidadeBase
    {
        protected readonly AppDbContext DbContext;
        protected readonly DbSet<T> DbSet;

        public Repository(AppDbContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<T>();
        }

        public async Task<T> ObterPorIdAsNoTrackingAsync(Guid id) => await DbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

        public async Task<T> ObterPorIdAsync(Guid id) => await DbSet.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<T> ObterPorIdAsync(Guid id, IList<string> includes)
        {
            var result = DbSet.Where(x => x.Id == id).AsQueryable();
            foreach (var include in includes)
                result.Include(include);

            return await result.SingleAsync();
        }

        public async Task<IList<T>> ObterPorIdsAsync(Guid[] ids) => await DbSet.Where(x => ids.Contains(x.Id)).ToListAsync();

        public async Task<IList<T>> ObterTodosAsync() => await DbSet.ToListAsync();

        public async Task AdicionarAsync(T t) => await DbSet.AddAsync(t);

        public void Atualizar(T t)
        {
            DbSet.Attach(t);
            DbContext.Entry(t).State = EntityState.Modified;
        }

        public void Remover(T t) => DbSet.Remove(t);
        public void Dispose() => DbContext?.Dispose();

        public async Task SaveChangesAsync() => await DbContext.SaveChangesAsync();

    }
}
