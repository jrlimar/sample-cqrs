using Dev.Core.Entities;

namespace Dev.Core.Data
{
    public interface IRepository<T> where T : IAggregationRoot
    {
    }
}
