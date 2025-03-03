using BookStore.BLL.Interfaces;
using BookStore.BLL.Specifications;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SchoolDbContext _context;

        public GenericRepository(SchoolDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> CreateEntityAsync(T obj)
        {
            await _context.Set<T>().AddAsync(obj);
            return await _context.SaveChangesAsync();
        }

        public void DeleteEntityAsync(T obj)
        {
            _context.Set<T>().Remove(obj);

        }
        public void DetachedEntity(T obj)
        {
            _context.Entry(obj).State = EntityState.Detached;

        }
        public void DeleteAllEntitiesAsync(IEnumerable<T> obj)
        {
            _context.Set<T>().RemoveRange(obj);
        }



        public async Task<IEnumerable<T>> GetAllEntities()
            => await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAllEntitiesWithSpec(ISpecification<T> specifications)
            => await ApplySpecifications(specifications).ToListAsync();

        public async Task<long> GetCountAsync(ISpecification<T> specifications)
            => await ApplySpecifications(specifications).CountAsync();

        public async Task<T> GetEntityById(long? id)
            => await _context.Set<T>().FindAsync(id);


        public async Task<T> GetEntityWithSpec(ISpecification<T> specifications)
            => await ApplySpecifications(specifications).FirstOrDefaultAsync();

        public async Task<int> UpdateEntityAsync(T obj)
        {
            _context.Set<T>().Update(obj);
            return await _context.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecifications(ISpecification<T> specifications)
            => SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specifications);


    }
}
