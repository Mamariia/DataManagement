using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly Context _context;

    public Repository(Context context)
    {
        _context = context;
    }

    public List<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T? GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = _context.Set<T>().Find(id);
        if (entity != null) _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }
}