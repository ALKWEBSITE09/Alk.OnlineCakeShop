using E_Commerce_Cake.Models.Database;
using E_Commerce_Cake.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json.Linq;

namespace E_Commerce_Cake.Controllers
{
    public class AdminController : Controller
    {
        private readonly CakeDbContext context;

        public AdminController(CakeDbContext context)
        {
            this.context = context;
        }

        //  Admin List

        public async Task<IActionResult> AdminList()
        {
            TempData["Hii"] = HttpContext.Session.GetString("admin");
            var data = await context.cakeadmin.Include(x => x.customer).Where(x => x.UserTypesId!=2).ToListAsync();
            return View(data);
        }

        // Admin Details

        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {

                TempData["Hii"] = HttpContext.Session.GetString("admin");

                if (id == null)
                {
                    return NotFound();
                }
                var data = await context.cakeadmin.FirstOrDefaultAsync(x => x.Id == id);

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

        // Admin Delete

        public async Task<IActionResult> Delete(int? id)
        {
            var data = await context.cakeadmin.FindAsync(id);
            if (data != null)
            {
                context.cakeadmin.Remove(data);
            }
            await context.SaveChangesAsync();
            return RedirectToAction("AdminList");
        }

        // Admin Register

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AdminVM admin)
        {
            if (ModelState.IsValid)
            {
                Admin user = new Admin
                {
                    UserName = admin.UserName,
                    ShopName = admin.ShopName,
                    ShopOpenDate = admin.ShopOpenDate,
                    ShopOwnerName = admin.ShopOwnerName,
                    UserTypesId = 1,
                    Pass = admin.Pass,
                    Email = admin.Email,
                    Phone = admin.Phone,
                    ConfirmPass = admin.ConfirmPass,
                };

                await context.cakeadmin.AddAsync(user);
                await context.SaveChangesAsync();
                TempData["Data"] = "You Are Successfully Admin Register";
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        // Admin Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Admin admin)
        {
            var value = await context.cakeadmin.Where(x => x.Phone == admin.Phone && x.ShopName == admin.ShopName && x.Pass == admin.Pass).FirstOrDefaultAsync();
            if (value != null)
            {
                HttpContext.Session.SetString("admin", value.UserName);
                TempData["Data2"] = "You Are Successfully Login.......";
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                ViewData["Data"] = "Data Not Found";
                return View();
            }

        }

        // Admin Logout

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                HttpContext.Session.Remove("admin");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // Admin Dashboard

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                var cate = context.cakecategory.ToList();
                var product = context.cakeproduct.ToList();
                var order = context.inv.ToList();
                var choice = context.choices.ToList();

                ViewBag.cate = cate.Count;
                ViewBag.product = product.Count;
                ViewBag.order = order.Count;
                ViewBag.choice = choice.Count;

                TempData["Hii"] = HttpContext.Session.GetString("admin");
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        //Admin User Account

        public async Task<IActionResult> AdminUser(string? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {

                TempData["Hii"] = HttpContext.Session.GetString("admin");

                if (id == null)
                {
                    return NotFound();
                }
                var data = await context.cakeadmin.FirstOrDefaultAsync(x => x.UserName == id);

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

        // Edit Admin 

        public async Task<IActionResult> EditAdmin(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {

                TempData["Hii"] = HttpContext.Session.GetString("admin");

                if (id == null || context.cakeadmin == null)
                {
                    return NotFound();
                }
                var data = await context.cakeadmin.FindAsync(id);
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
        [HttpPost]
        public async Task<IActionResult> EditAdmin(int? id, AdminVM item)
        {
            if (ModelState.IsValid)
            {
                var data = await context.cakeadmin.FirstOrDefaultAsync(x => x.Id == item.Id);
                if (data != null)
                {
                    data.UserName = item.UserName;
                    data.ShopName = item.ShopName;
                    data.ShopOwnerName = item.ShopOwnerName;
                    data.Email = item.Email;
                    data.Phone = item.Phone;
                    data.ConfirmPass = item.ConfirmPass;
                    data.Pass = item.Pass;
                    data.UserTypesId = 1;
                    data.ShopOpenDate = item.ShopOpenDate;
                }
                context.Update(data);
                await context.SaveChangesAsync();
                ViewBag.datas = "Data Successfully Update...";
                return View();

            }
            return View();
        }

        //Delete Admin

        public async Task<IActionResult> DeleteAdmin(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                var data = await context.cakeadmin.FindAsync(id);
                if (data != null)
                {
                    context.cakeadmin.Remove(data);
                }
                await context.SaveChangesAsync();
                HttpContext.Session.Remove("admin");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
