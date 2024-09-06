using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.DataAccess.Repository.IRepository;

namespace Shop.DataAccess.Repository
{

    // To Implement Interface  select IRepository Controller and " ctrl + . "
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MyDbContext _context;

        // If we dont know which data types we use then  we use <T> as Generic types
        internal DbSet<T> dbSet;

        public Repository(MyDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
            //_context.Categories== dbSet

            _context.Products.Include(x => x.Category).Include(x => x.CategoryId);

        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {

            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;

            }
            else
            {
                query = dbSet.AsNoTracking();
            }

            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();

            /*
            if (tracked)
            {
                IQueryable<T> query = dbSet;
                query = query.Where(filter);
                if (!string.IsNullOrEmpty(includeProperties))
                {
                    foreach (var includePros in includeProperties.Split
                        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includePros);
                    }
                }
                return query.FirstOrDefault();

            }
            else
            {
                IQueryable<T> query = dbSet.AsNoTracking();
                query = query.Where(filter);
                if (!string.IsNullOrEmpty(includeProperties))
                {
                    foreach (var includePros in includeProperties.Split
                        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includePros);
                    }
                }
                return query.FirstOrDefault();
            }
            */

            /*
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includePros in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includePros);
                }
            }
            return query.FirstOrDefault();
            */
        }



        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includePros in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includePros);
                }
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

/*
 
public class Extend1<T, E> extends MyGeneric<T, E> {}

Here T and E are each present twice and in two different roles in Extend1<T,E> you define type parameters. 
This means that the type Extend1 has two (unbounded) type parameters T and E.
This tells the Java compiler that those who use Extend1 need to specify the types in extends MyGeneric<T,E> 
you use the previously defined type arguments. If T and E were not known to be type arguments here, 
then T and E would be simple type references, i.e. the compiler would look for classes (or interfaces, ...) named T and E (and most likely not find them).
Yes, type arguments follow the same syntactic rules as any other identifier in Java, so you can use multiple letters ABC or even names that can be confusing 
(using a type argument called String is legal, but highly confusing).

Single-letter type argument names are simply a very common naming strategy.


 */