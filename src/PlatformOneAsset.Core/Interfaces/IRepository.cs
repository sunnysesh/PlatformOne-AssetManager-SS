namespace PlatformOneAsset.Core.Interfaces;

public interface IRepository<T>
{
    IEnumerable<T> GetAll();

    T GetByReference(string reference);

    T Add(T entity);

    T Update(T entity);
}