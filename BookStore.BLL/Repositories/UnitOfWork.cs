using BookStore.BLL.Interfaces;
using DAL;
using System.Collections;

namespace BookStore.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<int> Complete() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
        public void ClearTracking() => _context.ChangeTracker.Clear();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories is null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                _repositories.Add(type, repository);
            }

            return (IGenericRepository<TEntity>)_repositories[type]!;
        }

    }
}
