using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using WebApplication_Artisan_.DataAccessLayer;
using WebApplication_Artisan_.Models;

namespace WebApplication_Artisan_.Controllers
{
    public class ProductController : Controller
    {
        private readonly ArtisanDbContext _context;
        private readonly IWebHostEnvironment _environment;


        public ProductController(ArtisanDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }

        public IActionResult Index()
        {
            var products = _context.Products.Include(p => p.Categories);
            return View(products);
        }
        // GET: Product/Create
        [HttpGet]
        public IActionResult Create()
        {
            LoadCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Create(productviewmodel model, IFormFile imageFile)
        {
            string filename = "";
            if (model.photo != null)
            {
                string imagePath = Path.Combine(_environment.WebRootPath, "Images");
                filename = Guid.NewGuid().ToString() + "_" + model.photo.FileName;
                string filepath = Path.Combine(imagePath, filename);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    model.photo.CopyTo(stream);
                }
            }

            Product product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
                Description = model.Description,
                ImageUrl = filename,
                CategoryId = model.CategoryId // Assuming CategoryId is a property in your productviewmodel
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index)); // Redirect to the product listing page after creation
        }




        



        [NonAction]
        private void LoadCategories()
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }



        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            LoadCategories();
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
       

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            ModelState.Remove("Categories");
            if (ModelState.IsValid)
            {
                _context.Products.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            LoadCategories();
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            LoadCategories();
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult AddToCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            cart.Items.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = (decimal)product.Price,
                Quantity = 1
            });
            HttpContext.Session.Set("Cart", cart);

            return RedirectToAction(nameof(ViewCart));
        }

        [HttpGet]
        public IActionResult ViewCart()
        {
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            var itemToRemove = cart.Items.FirstOrDefault(item => item.ProductId == id);
            if (itemToRemove != null)
            {
                cart.Items.Remove(itemToRemove);
                HttpContext.Session.Set("Cart", cart);
            }
            return RedirectToAction(nameof(ViewCart));
        }

        
    }
}
