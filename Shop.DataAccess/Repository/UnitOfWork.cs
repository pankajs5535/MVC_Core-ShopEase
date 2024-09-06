using Shop.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private MyDbContext _context;
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }

        public ICompanyRepository Company { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }



        public UnitOfWork(MyDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            Product = new ProductRepository(_context);
            Company = new CompanyRepository(_context);
            ShoppingCart = new ShoppingCartRepository(_context);
            ApplicationUser = new ApplicationUserRepository(_context);
            OrderHeader = new OrderHeaderRepository(_context);
            OrderDetail = new OrderDetailRepository(_context);
        }

        // this method globally use this method so we can implment this here in unit of work
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}


/*
 
 

1. Centralized Data Access:

Concept: UnitOfWork centralizes all repository interactions.
Detail: Instead of managing each repository separately, you interact with them through a single UnitOfWork instance, providing a unified approach
to data operations.


2. Repository Management:

Concept: Initialization and management of repositories.
Detail: UnitOfWork initializes each repository and assigns it to its respective property, allowing CRUD operations through the UnitOfWork.


3. Transaction Management:

Concept: Handling transactions with the Save method.
Detail: The Save method commits all changes to the database in a single transaction. It also supports transaction rollback if an exception occurs.


4. Dependency Injection:

Concept: Registration in the DI container.
Detail: UnitOfWork is registered in the dependency injection container, enabling IUnitOfWork to be injected into controllers or services,
promoting loose coupling and easier management.


 */


