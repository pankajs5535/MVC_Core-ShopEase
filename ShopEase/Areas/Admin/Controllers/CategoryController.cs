using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Shop.DataAccess;
using Shop.DataAccess.Repository.IRepository;
using Shop.Models;
using Shop.Utility;

namespace ShopEase.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        //private readonly MyDbContext _context;
        //public CategoryController(MyDbContext context)
        //{
        //    _context = context;
        //}

        //private readonly ICategoryRepository _categoryRepo;

        private readonly IUnitOfWork _unitOfWork;


        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // 1
        public IActionResult Index()
        {
            //var objCategoryList=_context.Categories.ToList();

            //List<Category> objCategoryList = _context.Categories.ToList();

            //List<Category> objCategoryList = _categoryRepo.GetAll().ToList();

            //List<Category> objCategoryList = _categoryRepo.GetAll().ToList();

            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();



            return View(objCategoryList);
        }


        // 2
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category objCategory)
        {

            if (objCategory.Name == objCategory.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order does not correctly match the Name.");
            }

            // Add this check to ensure model validity
            if (ModelState.IsValid)
            {
                //_categoryRepo.Add(objCategory);
                //_categoryRepo.Save();
                _unitOfWork.Category.Add(objCategory);
                _unitOfWork.Save();
                TempData["insert_success"] = "Inserted....";
                return RedirectToAction("Index", "Category");
            }

            if (objCategory == null)
            {
                return NotFound();
            }


            return View();
        }


        // 3
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.Category.Get(x => x.Id == id);

            //Category? categoryFromDb = _categoryRepo.Get(x => x.Id == id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }


        [HttpPost]
        public IActionResult Edit(Category objCategory)
        {

            // Add this check to ensure model validity
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(objCategory);
                _unitOfWork.Save();
                TempData["update_success"] = "Updated....";
                return RedirectToAction("Index", "Category");
            }

            if (objCategory == null)
            {
                return NotFound();
            }

            return View();
        }

        // 4

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Category? categoryFromDb = _context.Categories.Find(id); 1st use
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();


            //Category? categoryFromDb = _categoryRepo.Get(x => x.Id == id);

            Category? categoryFromDb = _unitOfWork.Category.Get(x => x.Id == id);



            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            //Category objCategory = _context.Categories.Find(id);


            //Category? objCategory = _categoryRepo.Get(x => x.Id == id);

            Category? objCategory = _unitOfWork.Category.Get(x => x.Id == id);


            if (objCategory == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(objCategory);
            _unitOfWork.Save();
            TempData["Delete_success"] = "Deleted....";
            return RedirectToAction("Index", "Category");
        }

    }

}




