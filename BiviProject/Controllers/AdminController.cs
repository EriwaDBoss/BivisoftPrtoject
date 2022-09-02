using BiviProject.ConstantEnums;
using BiviProject.Data;
using BiviProject.Logic;
using BiviProject.Models;
using BiviProject.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiviProject.Controllers
{
    public class AdminController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly InterOfDropdownOrch _interOfDropdownOrch;


        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, InterOfDropdownOrch interOfDropdownOrch)
        {
            _context = context;

            _userManager = userManager;
            _signInManager = signInManager;
            _interOfDropdownOrch = interOfDropdownOrch;

        }
        public IActionResult Index()
        {
            IEnumerable<ApplicationUser> userList = _context.ApplicationUser.ToList();
            return View(userList);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(ApplicationUser user)
        {
            if (user != null)
            {
                if (user.Email == null)
                {
                    return View(user);
                }
                if (user.Password != user.ConfirmPassword)
                {
                    return View(user);
                }
                var userEmail = _context.ApplicationUser.Find(user.Email);
                if (userEmail != null)
                {
                    return View(user);
                }
                user.UserName = user.Email;
                user.IsAdmin = true;
                user.DateCreated = DateTime.Now;

                var result = await _userManager.CreateAsync(user, user.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Dashboard");
                }
            }
            return View(user);
        }


        //GET - LOGIN
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser user)
        {
            if (user.Password != null && user.Email != null)
            {

                var userDetails = _context.ApplicationUser.Where(x => x.Email == user.Email).FirstOrDefault();
                if (userDetails == null)
                {
                    return View(user);
                }
                var result = await _signInManager.PasswordSignInAsync(userDetails, user.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Dashboard", "Admin");
                }

            }
            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

        //[HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateProductList()
        {
            ViewBag.Category = _context.Category.Where(x => !x.Softdelete && x.EnumCategory == ProductCategoryEnum.Product).ToList();

            var ddd = new ProductViewModel();
            ddd.Products = new List<Product>();

            var Products = _context.Product.Where(x => !x.Softdelete && x.EnumCategory == ProductCategoryEnum.Product).ToList();

            ddd.Products = Products;
            return View(ddd);
        }


        [HttpPost]
        public IActionResult CreateProductList(ProductViewModel product)
        {
            ViewBag.Category = _context.Category.Where(x => !x.Softdelete && x.EnumCategory == ProductCategoryEnum.Product).ToList();


                if (product.Name == null)
                {

                    SetMessage("please fill the  required field", Message.Category.Error);
                    return View(product);
                }
                if (product.Price == 0)
                {

                    return View(product);
                }

                if (product.Quatity <= 0)
                {

                    return View(product);
                }


            
           

            var model = new Product
            {
                Name = product.Name,
                Price = product.Price,
                Softdelete = false,
                EnumCategory = ProductCategoryEnum.Product,
                Keywords = product.Keywords,
                Quatity = product.Quatity,
                CategoryId = product.CategoryId,
                DateCreated = product.DateCreated,
                
            };
            _context.Add(model);
            _context.SaveChanges();
            var products = _context.Product.Where(x => !x.Softdelete && x.EnumCategory == ProductCategoryEnum.Product).ToList();
            var ddd = new ProductViewModel();
            ddd.Products = new List<Product>();
            ddd.Products = products;
            return View(ddd);
        }



        public IActionResult CreateCategoryView()
        {
            ViewBag.Category = _interOfDropdownOrch.GetAllProductCategoryEnum();
            var categories = _context.Category.Where(x => !x.Softdelete && x.EnumCategory == ProductCategoryEnum.Product).ToList();
            var ccc = new CategoryViewModel();
            ccc.Categories = new List<Category>();
            ccc.Categories = categories;
            return View(ccc);
        }



        [HttpPost]
        public IActionResult CreateCategoryView(CategoryViewModel category)
        {
            ViewBag.Category = _interOfDropdownOrch.GetAllProductCategoryEnum();

            if (category.Name == null)
            {
                SetMessage("please fill the  required field", Message.Category.Error);
                return View(category);
            }
            if (category.Discription == null)
            {
                SetMessage("please fill the  required field", Message.Category.Error);
                return View(category);
            }

            var model = new Category
            {
                Name = category.Name,
                Discription = category.Discription,
                DateCreated = DateTime.Now,
                Softdelete = false,
                EnumCategory = ProductCategoryEnum.Product,
                Title = category.Title,

            };
            _context.Add(model);
            _context.SaveChanges();
            var categories = _context.Category.Where(x => !x.Softdelete && x.EnumCategory == ProductCategoryEnum.Product).ToList();
            var ccc = new CategoryViewModel();
            ccc.Categories = new List<Category>();
            ccc.Categories = categories;
            return View(ccc);


        }

        //GET - EDIT
        [HttpGet]
        public IActionResult Edit( int id)
        {
            if (id == null || id == 0)
            {
               
                SetMessage("Select the data to edit", Message.Category.Error);
                return View(id);
            }

            var j = _context.Category.Find(id);
            if (j == null)
            {
                SetMessage("theres no record found", Message.Category.Error);
                return View(j);
            }
            var rex = new CategoryViewModel()
            {
                Id = j.Id,
                DateCreated = j.DateCreated,
                Title = j.Title,
                Discription = j.Discription,
                Name = j.Name,
                Softdelete = j.Softdelete,
                EnumCategory = j.EnumCategory,
            };
            return View(rex);



        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel category)
        {

            if (category.Name == null)
            {
                SetMessage("please fill the  required field", Message.Category.Error);
                return View(category);
            }
            if (category.Discription == null)
            {
                SetMessage("please fill the  required field", Message.Category.Error);
                return View(category);
            }

            var model = _context.Category.Where(x => x.Id == category.Id && x.EnumCategory == ProductCategoryEnum.Product).FirstOrDefault();
            model.Name = category.Name;
            model.Discription = category.Discription;
            model.Title = category.Title;

            _context.Update(model);
            _context.SaveChanges();
           
            return RedirectToAction ("CreateCategoryView");


        }




        //GET - DELETE
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var Category = _context.Category.Find(id);
            if (Category == null)
            {
                return NotFound();
            }
            return RedirectToAction("CreateCategoryView");
        }





        [HttpPost]
        public IActionResult DeleteItem(int id)
        {
            if ( id == 0)
            {
                SetMessage("Select the data to Delete", Message.Category.Error);
                return RedirectToAction("CreateCategoryView");
            }

            var category = _context.Category.Find(id);
            if (category == null)
            {
                SetMessage("theres no record found", Message.Category.Error);
                return View("CreateCategoryView");
            }
            category.Softdelete = true;
            _context.Update(category);
            _context.SaveChanges();

            return RedirectToAction("CreateCategoryView");

        }


        //GET - EDIT
        [HttpGet]
        public IActionResult Edits(int id)
        {
            if (id == null || id == 0)
            {

                SetMessage("Select the data to edit", Message.Category.Error);
                return View(id);
            }


            var eriwa = _context.Product.Find(id);
            if (eriwa == null)
            {
                SetMessage("theres no record found", Message.Category.Error);
                return View(eriwa);
            }
            var pro = new ProductViewModel()
            {
                Id = eriwa.Id,
                DateCreated = eriwa.DateCreated,
 
                Discription = eriwa.Discription,
                Name = eriwa.Name,
                Softdelete = eriwa.Softdelete,
                EnumCategory = eriwa.EnumCategory,
            };
            return View(pro);



        }

        [HttpPost]
        public IActionResult Edits(ProductViewModel product)
        {

            if (product.Name == null)
            {
                SetMessage("please fill the  required field", Message.Category.Error);
                return View(product);
            }
            if (product.Discription == null)
            {
                SetMessage("please fill the  required field", Message.Category.Error);
                return View(product);
            }

            var model = _context.Product.Where(x => x.Id == product.Id && x.EnumCategory == ProductCategoryEnum.Product).FirstOrDefault();
            model.Name = product.Name;
            model.Discription = product.Discription;
          
            _context.Update(model);
            _context.SaveChanges();

            return RedirectToAction("CreateProductList");


        }





    }
}






      