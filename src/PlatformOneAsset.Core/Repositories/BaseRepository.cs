namespace PlatformOneAsset.Core.Repositories;

public abstract class BaseRepository<T>
{
    protected readonly List<T> _entities;

    protected BaseRepository(List<T> entities)
    {
        _entities = entities;
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByReferenceAsync(string reference)
    {
        throw new NotImplementedException();
    }
    
    public Task<T> AddAsync(string reference)
    {
        throw new NotImplementedException();
    }
    
    public Task<T> UpdateAsync(string reference)
    {
        throw new NotImplementedException();
    }
}