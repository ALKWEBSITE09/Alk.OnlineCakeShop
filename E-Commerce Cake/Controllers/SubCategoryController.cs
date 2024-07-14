using E_Commerce_Cake.Models.Database;
using E_Commerce_Cake.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Cake.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly CakeDbContext context;

        public SubCategoryController(CakeDbContext context)
        {
            this.context = context;
        }

        // subCategory List

        public async Task<IActionResult> SubCategoryList()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var data = await context.cakesubcategory.Include(x => x.Cg).ToListAsync();
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // Create Sub Category

        [HttpGet]
        public async Task<IActionResult> CreateSubCategory()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                ViewBag.data = new SelectList(context.cakecategory, "Id", "Tittle");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubCategory(SubCategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                SubCategory model = new SubCategory
                {
                    tittle = vm.tittle,
                    CategoryesId = vm.CategoryesId
                };

                await context.cakesubcategory.AddAsync(model);
                await context.SaveChangesAsync();
                return RedirectToAction("SubCategoryList");
            }
            return View();
        }

        // Edit Sub Category

        [HttpGet]
        public async Task<IActionResult> EditSubCategory(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                ViewBag.data = new SelectList(context.cakecategory, "Id", "Tittle");
                var data = await context.cakesubcategory.FirstOrDefaultAsync(x => x.Id == id);
                if (data != null)
                {
                    SubCategoryVM model = new SubCategoryVM
                    {
                        Id = data.Id,
                        tittle = data.tittle,
                        CategoryesId = data.CategoryesId
                    };
                    return View(model);
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
        public async Task<IActionResult> EditSubCategory(SubCategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                var data = await context.cakesubcategory.FirstOrDefaultAsync(x => x.Id == vm.Id);
                if (data != null)
                {
                    data.tittle = vm.tittle;
                    data.CategoryesId = vm.CategoryesId;

                    context.cakesubcategory.Update(data);
                    await context.SaveChangesAsync();
                    return RedirectToAction("SubCategoryList");
                }
                else
                {
                    return NotFound();
                }
            }
            return View();
        }

        // detete Sub Category

        public async Task<IActionResult> DeleteSubCatogary(int? id)
        {
            TempData["Hii"] = HttpContext.Session.GetString("admin");
            var data = await context.cakesubcategory.Include(x => x.Cg).FirstOrDefaultAsync(x => x.Id == id);
            var value = await context.cakeproduct.Include(x => x.Scg).Include(x => x.Cg).FirstOrDefaultAsync(x => x.SubCategoryId == data.Id);
            if (data != null)
            {
                context.cakesubcategory.Remove(data);
                context.cakeproduct.Remove(value);

            }
            await context.SaveChangesAsync();
            return RedirectToAction("SubCategoryList");
        }

        // Details Sub Category
        public async Task<IActionResult> DetailsSubCategory(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var data = await context.cakesubcategory.Include(x => x.Cg).FirstOrDefaultAsync(x => x.Id == id);
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
