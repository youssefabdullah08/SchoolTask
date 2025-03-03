using System.Linq.Expressions;

namespace BookStore.BLL.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> dbndanceIncludes { get; }
        public Expression<Func<T, object>> OrderBy { get; }
        public Expression<Func<T, object>> OrderByDesc { get; }
        public int Take { get; }
        public int Skip { get; }
        public bool IsPaging { get; }
    }
}
