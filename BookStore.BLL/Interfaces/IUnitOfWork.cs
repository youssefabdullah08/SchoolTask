namespace BookStore.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

        Task<int> Complete();
        void ClearTracking();
    }
}
