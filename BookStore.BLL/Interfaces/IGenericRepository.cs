namespace BookStore.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetEntityById(long? id);
        Task<IEnumerable<T>> GetAllEntities();
        Task<int> CreateEntityAsync(T obj);
        Task<int> UpdateEntityAsync(T obj);
        void DeleteEntityAsync(T obj);
        void DeleteAllEntitiesAsync(IEnumerable<T> obj);
        Task<long> GetCountAsync(ISpecification<T> specifications);
        Task<T> GetEntityWithSpec(ISpecification<T> specifications);
        Task<IEnumerable<T>> GetAllEntitiesWithSpec(ISpecification<T> specifications);
        public void DetachedEntity(T obj);
        Task<int> AddRangeAsync(IEnumerable<T> entities);
    }
}
