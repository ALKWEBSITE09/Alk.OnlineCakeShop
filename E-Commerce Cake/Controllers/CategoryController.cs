using E_Commerce_Cake.Models.Database;
using E_Commerce_Cake.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Cake.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CakeDbContext context;

        public CategoryController(CakeDbContext context)
        {
            this.context = context;
        }

        // Category List

        public async Task<IActionResult> CategoryList()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var data = await context.cakecategory.ToListAsync();
                if (data == null)
                {
                    return NotFound();
                }
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        // Create Category

        [HttpGet]
        public async Task<IActionResult> CreateCategory()
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
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                Category data = new Category
                {
                    Id = vm.Id,
                    Tittle = vm.Tittle
                };
                await context.cakecategory.AddAsync(data);
                await context.SaveChangesAsync();
                return RedirectToAction("CategoryList");
            }
            else
            {
                return NotFound(ModelState);
            }
        }

        // Edit Category

        [HttpGet]
        public async Task<IActionResult> EditCategory(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (id != null)
                {
                    TempData["Hii"] = HttpContext.Session.GetString("admin");
                    var data = await context.cakecategory.FirstOrDefaultAsync(x => x.Id == id);

                    CategoryVM value = new CategoryVM
                    {
                        Id = data.Id,
                        Tittle = data.Tittle
                    };
                    return View(value);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(int? id, CategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                var data = await context.cakecategory.FirstOrDefaultAsync(x => x.Id == vm.Id);
                if (data != null)
                {
                    data.Tittle = vm.Tittle;
                    context.cakecategory.Update(data);
                    await context.SaveChangesAsync();
                    return RedirectToAction("CategoryList");
                }
                else { return NotFound(); }
            }
            else
            {
                return View();
            }
        }


        // Delete Category

        public async Task<IActionResult> DeleteCateory(int? id)
        {
            var data = await context.cakecategory.FindAsync(id);
            var value = await context.cakesubcategory.Include(x => x.Cg).Where(x => x.CategoryesId == data.Id).ToListAsync();
            var value2 = await context.cakeproduct.Include(x => x.Scg).Include(x => x.Cg).Where(x => x.CategoryesId == data.Id).ToListAsync();

            if (data != null)
            {
                context.cakecategory.Remove(data);
                context.cakesubcategory.RemoveRange(value);
                context.cakeproduct.RemoveRange(value2);
            }
            await context.SaveChangesAsync();
            return RedirectToAction("CategoryList");
        }

        // Details Category

        public async Task<IActionResult> DetailsCategory(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var data = await context.cakecategory.FirstOrDefaultAsync(x => x.Id == id);
                if (data == null)
                {
                    return NotFound();
                }
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }
    }
}
