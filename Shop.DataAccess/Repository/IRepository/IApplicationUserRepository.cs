using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Repository.IRepository;
using Shop.Models;

namespace Shop.DataAccess.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser> // interface implements interface
    {
        //void Update(ApplicationUser obj);
        //void Save();
    }
}
