using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericEFRepository<T> :IGenericEFRepository<T> where T : BaseEntity
    {
        private readonly EMSContext _context;

        public GenericEFRepository()
        {
        }
        public GenericEFRepository(EMSContext context)
        {
            _context = context;
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
        public async Task<T> GetByIdAsync(int id,CancellationToken cancellationToken=default)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken=default)
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
        }
        public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec,CancellationToken cancellationToken=default)
        { 
            var output = await ApplySpecification(spec).ToListAsync(cancellationToken);
            return output;
        }

        public async Task<int> CountAsync(ISpecification<T> spec,CancellationToken cancellationToken=default)
        {
            return await ApplySpecification(spec).CountAsync(cancellationToken);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            //Created date property should never be updated hence below code
            _context.Entry(entity).Property(p => p.CreatedDate).IsModified = false;
            _context.Entry(entity).Property(p => p.CreatedUTCDate).IsModified = false;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        
    }
}
