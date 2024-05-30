using Blank.Domain.Entities;

namespace Blank.Domain.Interfaces.Repositories
{
    public interface IRepository<T> : IDisposable where T : EntidadeBase
    {
        Task<T> ObterPorIdAsNoTrackingAsync(Guid id);
        Task<T> ObterPorIdAsync(Guid id);
        Task<IList<T>> ObterPorIdsAsync(Guid[] ids);
        Task<IList<T>> ObterTodosAsync();
        Task AdicionarAsync(T t);
        void Atualizar(T t);
        void Remover(T t);
        Task SaveChangesAsync();
        Task<T> ObterPorIdAsync(Guid id, IList<string> includes);

    }
}
