using Domain.Entities;
using Domain.Interfaces;
using System.Collections;


namespace Infrastructure.Data
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly EMSContext _context;
        private Hashtable _repositories;
        public UnitOfWork(EMSContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericEFRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericEFRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericEFRepository<TEntity>)_repositories[type];
        }
    }
}
