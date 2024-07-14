using E_Commerce_Cake.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Cake.Controllers
{
    public class FavorateController : Controller
    {
        private readonly CakeDbContext context;

        public FavorateController(CakeDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                var countcart = context.cakecart.Where(x => x.UsersId == value.Id).ToList();
                TempData["count"] = countcart.Count;
                TempData["Hii"] = value.FirstName;

                var user = TempData["gg"];

                var u = await context.cakeuser.Where(x => x.Phone == user).FirstOrDefaultAsync();
                if (u != null)
                {
                    var value2 = await context.favo.Include(x => x.Item).Where(x => x.UsersId == u.Id).ToListAsync();
                    return View(value2);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }
        public async Task<IActionResult> AddFavo(int? id)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                TempData["Hii"] = value.FirstName;

                ViewData["Get"] = HttpContext.Session.GetString("user");
                var user = ViewData["Get"];

                var u = await context.cakeuser.Where(x => x.Phone == user).FirstOrDefaultAsync();
                var data = await context.cakeproduct.FirstOrDefaultAsync(c => c.Id == id);
                var data1 = await context.favo.Where(x => x.itemId == data.Id && x.UsersId == u.Id).FirstOrDefaultAsync();
                if (data1 != null)
                {
                    data1.itemId = data.Id;
                    data1.UsersId = (int)u.Id;
                    context.Update(data1);
                    await context.SaveChangesAsync();
                    TempData["favorateerror"] = "no";

                    return RedirectToAction("AfterLogin", "Cart");

                }
                if (data != null)
                {
                    Favorate value2 = new Favorate
                    {
                        itemId = data.Id,
                        UsersId = (int)u.Id
                    };

                    context.favo.Add(value2);
                    await context.SaveChangesAsync();
                    TempData["favorate"] = "done";

                    return RedirectToAction("AfterLogin", "Cart");
                }
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var invoice = await context.favo.Include(x => x.Item.Cg).FirstOrDefaultAsync(x => x.Id == id);

            if (invoice != null)
            {
                context.favo.Remove(invoice);
            }
            TempData["favodelete"] = invoice.Item.Cg.Tittle + " Removed In Favorate.";
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
