using E_Commerce_Cake.Models.Database;
using E_Commerce_Cake.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Cake.Controllers
{
    public class ProductsController : Controller
    {
        private readonly CakeDbContext _context;
        private readonly IWebHostEnvironment env;

        public ProductsController(CakeDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }



        // GET: Products
        public async Task<IActionResult> Index(string anyone)
        {
            TempData["Hii"] = HttpContext.Session.GetString("admin");
            if (string.IsNullOrEmpty(anyone))
            {
                var cakeDbContext = _context.cakeproduct.Include(p => p.Cg).Include(p => p.Scg);
                return View(await cakeDbContext.ToListAsync());
            }
            else
            {
                var cakeDbContext = _context.cakeproduct.Include(p => p.Cg).Include(p => p.Scg).Where(x => x.Cg.Tittle == anyone || x.Scg.tittle == anyone || x.Name == anyone);
                return View(await cakeDbContext.ToListAsync());
            }
            return View(anyone);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {

                if (id == null)
                {
                    return NotFound();
                }
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var product = await _context.cakeproduct
                    .Include(p => p.Cg)
                    .Include(p => p.Scg)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                ViewData["CategoryesId"] = new SelectList(_context.cakecategory, "Id", "Tittle");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM vm)
        {
            if (ModelState.IsValid)
            {
                string filepath = "";
                if (vm.ImagePath != null)
                {
                    string uploadFolder = Path.Combine(env.WebRootPath, "Images/Product/");
                    filepath = Guid.NewGuid().ToString() + "_" + vm.ImagePath.FileName;
                    string fullPath = Path.Combine(uploadFolder, filepath);
                    vm.ImagePath.CopyTo(new FileStream(fullPath, FileMode.Create));
                }


                Product product = new Product
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    Price = vm.Price,
                    CategoryesId = vm.CategoryesId,
                    SubCategoryId = 1,
                    ImagePath = filepath

                };


                _context.cakeproduct.Add(product);
                await _context.SaveChangesAsync();

                var data = await _context.cakeproduct.FirstOrDefaultAsync(x => x.Name == vm.Name);
                TempData["Id"] = data.Id;

                return RedirectToAction(nameof(PageView));
            }
            ViewData["CategoryesId"] = new SelectList(_context.cakecategory, "Id", "Tittle", vm.CategoryesId);
            return View(vm);
        }

        //extra page
        public IActionResult PageView()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        //Add Sub Category
        public async Task<IActionResult> AddSubCategory(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var product = await _context.cakeproduct.FindAsync(id);

                AddSubCategory data = new AddSubCategory
                {
                    Id = product.Id,
                    SubCategoryId = product.SubCategoryId
                };
                if (product == null)
                {
                    return NotFound();
                }
                ViewData["SubCategoryId"] = new SelectList(_context.cakesubcategory.Where(x => x.CategoryesId == product.CategoryesId), "Id", "tittle", product.SubCategoryId);
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // POST: Products/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSubCategory(int id, AddSubCategory product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var item = await _context.cakeproduct.FirstOrDefaultAsync(x => x.Id == product.Id);

                    if (item != null)
                    {
                        item.Name = item.Name;
                        item.Description = item.Description;
                        item.Price = item.Price;
                        item.CategoryesId = item.CategoryesId;
                        item.SubCategoryId = product.SubCategoryId;
                        item.ImagePath = item.ImagePath;

                        _context.cakeproduct.Update(item);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return NotFound();
                    }


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["SubCategoryId"] = new SelectList(_context.cakesubcategory, "Id", "tittle", product.SubCategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.cakeproduct.FirstOrDefaultAsync(x => x.Id == id);

                string image = "/jpg";
                byte[] imagebyte = Convert.FromBase64String(image);

                using (MemoryStream stream = new MemoryStream(imagebyte))
                {
                    IFormFile file = new FormFile(stream, 0, imagebyte.Length, "photo", product.ImagePath);

                    ProductVM data = new ProductVM
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        CategoryesId = product.CategoryesId,
                        ImagePath = file

                    };
                    if (product == null)
                    {
                        return NotFound();
                    }

                    ViewData["CategoryesId"] = new SelectList(_context.cakecategory, "Id", "Tittle", product.CategoryesId);
                    ViewData["SubCategoryId"] = new SelectList(_context.cakesubcategory, "Id", "tittle", product.SubCategoryId);
                    return View(data);
                }
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // POST: Products/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductVM vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var item = await _context.cakeproduct.FirstOrDefaultAsync(x => x.Id == vm.Id);
                    string filepath = "";
                    if (vm.ImagePath != null)
                    {
                        string uploadFolder = Path.Combine(env.WebRootPath, "Images/Product/");
                        filepath = Guid.NewGuid().ToString() + "_" + vm.ImagePath.FileName;
                        string fullPath = Path.Combine(uploadFolder, filepath);
                        vm.ImagePath.CopyTo(new FileStream(fullPath, FileMode.Create));


                        item.Name = vm.Name;
                        item.Description = vm.Description;
                        item.Price = vm.Price;
                        item.CategoryesId = vm.CategoryesId;
                        item.SubCategoryId = 1;
                        item.ImagePath = filepath;

                        _context.cakeproduct.Update(item);
                        await _context.SaveChangesAsync();

                        var data = await _context.cakeproduct.FirstOrDefaultAsync(x => x.Name == vm.Name);
                        TempData["EditId"] = data.Id;


                        return RedirectToAction("EditPageView");
                    }
                    else
                    {
                        item.Name = vm.Name;
                        item.Description = vm.Description;
                        item.Price = vm.Price;
                        item.CategoryesId = vm.CategoryesId;
                        item.SubCategoryId = 1;
                        item.ImagePath = item.ImagePath;


                        _context.cakeproduct.Update(item);
                        await _context.SaveChangesAsync();

                        var data = await _context.cakeproduct.FirstOrDefaultAsync(x => x.Name == vm.Name);
                        TempData["EditId"] = data.Id;


                        return RedirectToAction("EditPageView");
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(vm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["CategoryesId"] = new SelectList(_context.cakecategory, "Id", "Tittle", vm.CategoryesId);
            return View(vm);
        }

        public IActionResult EditPageView()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }



        // GET: Products/Delete/5

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.cakeproduct.FindAsync(id);
            if (product != null)
            {
                _context.cakeproduct.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.cakeproduct.Any(e => e.Id == id);
        }
    }
}
