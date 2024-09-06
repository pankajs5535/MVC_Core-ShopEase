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
    public class ProductController : Controller
    {
        //private readonly MyDbContext _context;
        //public ProductController(MyDbContext context)
        //{
        //    _context = context;
        //}

        //private readonly IProductRepository _productRepo;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }


        // 1
        public IActionResult Index()
        {
            //var objProductList=_context.Categories.ToList();

            //List<Product> objProductList = _context.Categories.ToList();

            //List<Product> objProductList = _productRepo.GetAll().ToList();

            //List<Product> objProductList = _productRepo.GetAll().ToList();

            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();


            return View(objProductList);
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
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Product = new Product()
            };


            return View(productVM);
        }

       
        [HttpPost]
        //public IActionResult Create(Product objProduct)
        public IActionResult Create(ProductVM objProduct)
        {
            // Add this check to ensure model validity
            if (ModelState.IsValid)
            {
                //_productRepo.Add(objProduct);
                //_productRepo.Save();
                _unitOfWork.Product.Add(objProduct.Product);
                _unitOfWork.Save();
                TempData["insert_success"] = "Inserted....";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                objProduct.CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

                return View(objProduct);
            }
        }

        */



        // Merge Create and Update in Single Action


        /*
        public IActionResult Upsert(int? id)
        {
            //using viewmodel we dont want above
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Product = new Product()
            };

            if (id == null || id == 0)
            {
                // Create
                return View(productVM);
            }
            else
            {
                // Update
                productVM.Product = _unitOfWork.Product.Get(x => x.Id == id);
                if (productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }


            return View(productVM);
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            // Add this check to ensure model validity
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    //productVM.Product.ImageUrl = @"\images\product\" + file.FileName;

                    productVM.Product.ImageUrl = @"/images/product/" + file.FileName;


                }

                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["insert_success"] = "Inserted....";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

                return View(productVM);
            }
        }

        */

        public IActionResult Upsert(int? id)
        {
            // This is for category llist display
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),

                Product = new Product()
            };

            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(x => x.Id == id);
                return View(productVM);

            }

        }


        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                // for file
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        // delete old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"images\product\" + fileName;
                }


                if (productVM.Product.Id == 0)
                {
                    // Adding a new product
                    _unitOfWork.Product.Add(productVM.Product);
                    TempData["insert_success"] = "Product created successfully";
                }
                else
                {
                    // Updating an existing product
                    _unitOfWork.Product.Update(productVM.Product);
                    TempData["update_success"] = "Product updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index", "Product");
            }
            else
            {
                // Reload the category list if validation fails
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

                return View(productVM);
            }
        }

        /*
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["insert_success"] = "Inserted....";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

                return View(productVM);
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

            Product? productFromDb = _unitOfWork.Product.Get(x => x.Id == id);

            //Product? productFromDb = _productRepo.Get(x => x.Id == id);
            //Product? productFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Product? productFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }


        [HttpPost]
        public IActionResult Edit(Product objProduct)
        {

            // Add this check to ensure model validity
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(objProduct);
                _unitOfWork.Save();
                TempData["update_success"] = "Updated....";
                return RedirectToAction("Index", "Product");
            }

            if (objProduct == null)
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

            //Product? productFromDb = _context.Categories.Find(id); 1st use
            //Product? productFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Product? productFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();


            //Product? productFromDb = _productRepo.Get(x => x.Id == id);

            Product? productFromDb = _unitOfWork.Product.Get(x => x.Id == id);



            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            //Product objProduct = _context.Categories.Find(id);


            //Product? objProduct = _productRepo.Get(x => x.Id == id);

            Product? objProduct = _unitOfWork.Product.Get(x => x.Id == id);


            if (objProduct == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(objProduct);
            _unitOfWork.Save();
            TempData["Delete_success"] = "Deleted....";
            return RedirectToAction("Index", "Product");
        }

        */

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return Json(new { data = objProductList });

        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return Json(new { success = true, message = "Delete Successful" });


        }

        #endregion

    }

}

