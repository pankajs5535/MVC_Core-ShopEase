using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.DataAccess;
using Shop.DataAccess.Repository.IRepository;
using Shop.Models;
using Shop.Models.ViewModels;
using Shop.Utility;
//using Shop.Models.Models;

namespace ShopEase.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        //private readonly MyDbContext _context;
        //public CompanyController(MyDbContext context)
        //{
        //    _context = context;
        //}

        //private readonly ICompanyRepository _CompanyRepo;

        private readonly IUnitOfWork _unitOfWork;


        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // 1
        public IActionResult Index()
        {
            //var objCompanyList=_context.Categories.ToList();

            //List<Company> objCompanyList = _context.Categories.ToList();

            //List<Company> objCompanyList = _CompanyRepo.GetAll().ToList();

            //List<Company> objCompanyList = _CompanyRepo.GetAll().ToList();

            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();


            return View(objCompanyList);
        }


        // 2

        /*
        public IActionResult Create()
        {
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
            //{
            //    Text = x.Name,
            //    Value = x.Id.ToString()
            //});

            //ViewBag.CategoryList = CategoryList;

            //ViewData["CategoryList"] = CategoryList;

            //using viewmodel we dont want above
            CompanyVM CompanyVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Company = new Company()
            };


            return View(CompanyVM);
        }

       
        [HttpPost]
        //public IActionResult Create(Company objCompany)
        public IActionResult Create(CompanyVM objCompany)
        {
            // Add this check to ensure model validity
            if (ModelState.IsValid)
            {
                //_CompanyRepo.Add(objCompany);
                //_CompanyRepo.Save();
                _unitOfWork.Company.Add(objCompany.Company);
                _unitOfWork.Save();
                TempData["insert_success"] = "Inserted....";
                return RedirectToAction("Index", "Company");
            }
            else
            {
                objCompany.CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

                return View(objCompany);
            }
        }

        */



        // Merge Create and Update in Single Action


        /*
        public IActionResult Upsert(int? id)
        {
            //using viewmodel we dont want above
            CompanyVM CompanyVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Company = new Company()
            };

            if (id == null || id == 0)
            {
                // Create
                return View(CompanyVM);
            }
            else
            {
                // Update
                CompanyVM.Company = _unitOfWork.Company.Get(x => x.Id == id);
                if (CompanyVM.Company == null)
                {
                    return NotFound();
                }
                return View(CompanyVM);
            }


            return View(CompanyVM);
        }

        [HttpPost]
        public IActionResult Upsert(CompanyVM CompanyVM, IFormFile? file)
        {
            // Add this check to ensure model validity
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string CompanyPath = Path.Combine(wwwRootPath, @"images\Company");

                    using (var fileStream = new FileStream(Path.Combine(CompanyPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    //CompanyVM.Company.ImageUrl = @"\images\Company\" + file.FileName;

                    CompanyVM.Company.ImageUrl = @"/images/Company/" + file.FileName;


                }

                _unitOfWork.Company.Add(CompanyVM.Company);
                _unitOfWork.Save();
                TempData["insert_success"] = "Inserted....";
                return RedirectToAction("Index", "Company");
            }
            else
            {
                CompanyVM.CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

                return View(CompanyVM);
            }
        }

        */

        public IActionResult Upsert(int? id)
        {
            
            if (id == null || id == 0)
            {
                //create
                return View(new Company()); // In the code, new Company creates an empty instance of the Company class for a new entry, while fetching the existing Company object by id allows for updates; both scenarios pass the Company object to the view for further processing
            }
            else
            {
                //update
                Company CompanyObj = _unitOfWork.Company.Get(x => x.Id == id);
                return View(CompanyObj);

            }
        }


        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {
            if (ModelState.IsValid)
            {
                if (CompanyObj.Id== 0)
                {
                    // Adding a new Company
                    _unitOfWork.Company.Add(CompanyObj);
                    TempData["insert_success"] = "Company created successfully";
                }
                else
                {
                    // Updating an existing Company
                    _unitOfWork.Company.Update(CompanyObj);
                    TempData["update_success"] = "Company updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index", "Company");
            }
            else
            {
                return View(CompanyObj);
            }
        }

        /*
        [HttpPost]
        public IActionResult Upsert(CompanyVM CompanyVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Add(CompanyVM.Company);
                _unitOfWork.Save();
                TempData["insert_success"] = "Inserted....";
                return RedirectToAction("Index", "Company");
            }
            else
            {
                CompanyVM.CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

                return View(CompanyVM);
            }
        }

        */

        // 3

        /*
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Company? CompanyFromDb = _unitOfWork.Company.Get(x => x.Id == id);

            //Company? CompanyFromDb = _CompanyRepo.Get(x => x.Id == id);
            //Company? CompanyFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Company? CompanyFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (CompanyFromDb == null)
            {
                return NotFound();
            }

            return View(CompanyFromDb);
        }


        [HttpPost]
        public IActionResult Edit(Company objCompany)
        {

            // Add this check to ensure model validity
            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Update(objCompany);
                _unitOfWork.Save();
                TempData["update_success"] = "Updated....";
                return RedirectToAction("Index", "Company");
            }

            if (objCompany == null)
            {
                return NotFound();
            }

            return View();
        }

        */


        // 4

        /*
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Company? CompanyFromDb = _context.Categories.Find(id); 1st use
            //Company? CompanyFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Company? CompanyFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();


            //Company? CompanyFromDb = _CompanyRepo.Get(x => x.Id == id);

            Company? CompanyFromDb = _unitOfWork.Company.Get(x => x.Id == id);



            if (CompanyFromDb == null)
            {
                return NotFound();
            }

            return View(CompanyFromDb);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            //Company objCompany = _context.Categories.Find(id);


            //Company? objCompany = _CompanyRepo.Get(x => x.Id == id);

            Company? objCompany = _unitOfWork.Company.Get(x => x.Id == id);


            if (objCompany == null)
            {
                return NotFound();
            }
            _unitOfWork.Company.Remove(objCompany);
            _unitOfWork.Save();
            TempData["Delete_success"] = "Deleted....";
            return RedirectToAction("Index", "Company");
        }

        */

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return Json(new { data = objCompanyList });

        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

          
            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();

            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return Json(new { success = true, message = "Delete Successful" });


        }

        #endregion

    }

}

