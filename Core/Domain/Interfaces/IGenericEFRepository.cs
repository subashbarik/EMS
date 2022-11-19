using Domain.Entities;
using Domain.Specifications;

namespace Domain.Interfaces
{
    public interface IGenericEFRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id,CancellationToken cancellationToken);
        Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken);
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec,CancellationToken cancellationToken);
        Task<int> CountAsync(ISpecification<T> spec,CancellationToken cancellationToken);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
