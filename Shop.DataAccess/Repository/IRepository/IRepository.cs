using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class // We are working on Generic we dont know which class is !! so T is Generic classs
    {
        // Example assume that : T Category

        // Below Expression like we use in linq query(x=>x.id==ID) so what we will get its function and out is boolean and also have
        // Filter for individual Record
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties = null);
        //T Get(Expression<Func<T, bool>> filter, string? includeProperties = null); // change from string


        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);   // change for  in detail page record updateded without  _unitOfWork.ShoppingCart.Update(cartFromDb);

        void Add(T entity); // entity is parameter of T or we can say object
        void Update(T entity);
        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);

    }
}
