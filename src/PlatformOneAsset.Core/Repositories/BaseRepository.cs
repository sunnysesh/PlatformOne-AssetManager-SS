using PlatformOneAsset.Core.Interfaces;

namespace PlatformOneAsset.Core.Repositories;

public abstract class BaseRepository<T> : IRepository<T>
{
    protected readonly Dictionary<string, T> _entities = new Dictionary<string, T>();
    
    public IEnumerable<T> GetAll()
        => _entities.Values;

    public T GetByReference(string reference)
    {
        if (_entities.TryGetValue(reference, out var entity))
        {
            return entity;
        }

        return default(T);
    }
    
    public T Add(T entity)
    {
        var id = GetEntityId(entity);
        if (!_entities.TryAdd(id, entity))
            throw new InvalidOperationException();
                
        return entity;
    }
    
    public T Update(T entity)
    {
        var id = GetEntityId(entity);
        if (!_entities.ContainsKey(id))
            throw new InvalidOperationException();
        
        _entities[id] = entity;
        return entity;
    }
    
    protected abstract string GetEntityId(T entity);
}