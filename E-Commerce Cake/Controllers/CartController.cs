using E_Commerce_Cake.Models.Database;
using E_Commerce_Cake.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Razorpay.Api;
using System.Linq;
using Product = E_Commerce_Cake.Models.Database.Product;

namespace E_Commerce_Cake.Controllers
{
    public class CartController : Controller
    {
        private readonly CakeDbContext context;

        public CartController(CakeDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index(string anyone)
        {
            if (string.IsNullOrEmpty(anyone))
            {
                ViewData["CategoryesId"] = new SelectList(context.cakecategory, "Tittle", "Tittle");
                var cakeDbContext = context.cakeproduct.Include(p => p.Cg).Include(p => p.Scg);
                return View(await cakeDbContext.ToListAsync());
            }
            if (anyone == "All")
            {
                ViewData["CategoryesId"] = new SelectList(context.cakecategory, "Tittle", "Tittle");
                var cakeDbContext = context.cakeproduct.Include(p => p.Cg).Include(p => p.Scg);
                return View(await cakeDbContext.ToListAsync());
            }
            else
            {
                ViewData["CategoryesId"] = new SelectList(context.cakecategory, "Tittle", "Tittle");
                var cakeDbContext = context.cakeproduct.Include(p => p.Cg).Include(p => p.Scg).Where(x => x.Cg.Tittle == anyone || x.Scg.tittle == anyone || x.Name == anyone);
                return View(await cakeDbContext.ToListAsync());
            }
            

        }
        public async Task<IActionResult> AfterLogin(string anyone)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                TempData["Hii"] = value.FirstName;

                var countcart =await context.cakecart.Where(x => x.UsersId==value.Id).ToListAsync();
                TempData["count"] = countcart.Count;
                if (string.IsNullOrEmpty(anyone))
                {
                    ViewData["CategoryesId"] = new SelectList(context.cakecategory, "Tittle", "Tittle");
                    var cakeDbContext = context.cakeproduct.Include(p => p.Cg).Include(p => p.Scg);
                    return View(await cakeDbContext.ToListAsync());
                }
                if (anyone == "All")
                {
                    ViewData["CategoryesId"] = new SelectList(context.cakecategory, "Tittle", "Tittle");
                    var cakeDbContext = context.cakeproduct.Include(p => p.Cg).Include(p => p.Scg);
                    return View(await cakeDbContext.ToListAsync());
                }
                else
                {
                    ViewData["CategoryesId"] = new SelectList(context.cakecategory, "Tittle", "Tittle");
                    var cakeDbContext = context.cakeproduct.Include(p => p.Cg).Include(p => p.Scg).Where(x => x.Cg.Tittle == anyone || x.Scg.tittle == anyone || x.Name == anyone);
                    return View(await cakeDbContext.ToListAsync());
                }
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
            return View();
            

        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                var data = await context.cakeproduct.Include(x => x.Scg).Include(c => c.Cg).FirstOrDefaultAsync(x => x.Id == id);
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
            return View();


        }
        //public async Task<IActionResult> AddCart(int? id)
        //{
        //    if (HttpContext.Session.GetString("user") != null)
        //    {
        //        TempData["gg"] = HttpContext.Session.GetString("user");
        //        var value = context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
        //        TempData["Hii"] = value.FirstName;
        //        var data = await context.cakeproduct.Include(x => x.Cg).Include(x => x.Scg).Where(x => x.Id == id).SingleOrDefaultAsync();
        //        return View(data);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "Customer");
        //    }

        //}


        public async Task<IActionResult> AddCart(int? id,Product pc)
        {
            if(HttpContext.Session.GetString("user") != null)
            {
                Product data = await context.cakeproduct.Include(x => x.Cg).Include(x => x.Scg).Where(x => x.Id == id).SingleOrDefaultAsync();
                ViewData["Get"] = HttpContext.Session.GetString("user");
                var user = ViewData["Get"];

                var u = await context.cakeuser.Where(x => x.Phone == user).FirstOrDefaultAsync();
                

                var carts = await context.cakecart.Where(x => x.itemId == id && x.UsersId == u.Id).FirstOrDefaultAsync();
                if (carts != null)
                {
                    if (carts.itemId == id)
                    {
                        carts.Quantity += Convert.ToInt32(1);
                        carts.Price = carts.Price;
                        carts.Bill = carts.Price * carts.Quantity;
                        carts.itemId = carts.itemId;
                        carts.UsersId = carts.UsersId;
                        carts.subTotal = 0;

                        context.cakecart.Update(carts);
                        await context.SaveChangesAsync();
                        TempData["addcart"] = "done";
                    }
                    else
                    {
                        ProductCart c = new ProductCart();
                        c.Price = (float)data.Price;
                        c.Quantity = Convert.ToInt32(1);
                        c.Bill = c.Price * c.Quantity;
                        c.itemId = pc.Id;
                        c.UsersId = (int)u.Id;
                        c.subTotal = 0;

                        context.cakecart.Add(c);
                        await context.SaveChangesAsync();
                        TempData["addcart"] = "done";

                    }
                }
                else
                {
                    ProductCart c = new ProductCart();
                    c.Price = (float)data.Price;
                    c.Quantity = Convert.ToInt32(1);
                    c.Bill = c.Price * c.Quantity;
                    c.itemId = pc.Id;
                    c.UsersId = (int)u.Id;
                    c.subTotal = 0;

                    context.cakecart.Add(c);
                    await context.SaveChangesAsync();
                    TempData["addcart"] = "done";

                }


                
                
                return RedirectToAction("AfterLogin");
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }
        public async Task<IActionResult> delete(int? id)
        { 
            var data = context.cakecart.Include(c => c.Item.Cg).FirstOrDefault(c => c.Id == id);
            if (data != null)
            {
                context.cakecart.Remove(data);
            }
            TempData["cartdelete"] = data.Item.Cg.Tittle + " Removed In Cart";
            context.SaveChanges();
            return RedirectToAction("CheckOut");
        
        }

        public async Task<IActionResult> Update(int? id, string qnty)
        {
            var item = context.cakecart.Include(x => x.Item).Include(x => x.Customer).FirstOrDefault(x => x.Id==id);
            if (item !=  null)
            {
                item.Quantity = Convert.ToInt32(qnty);
                item.Price = item.Price;
                item.Bill = item.Price * item.Quantity;
                item.itemId = item.itemId;
                item.UsersId = item.UsersId;
                item.subTotal = 0;
            }
            context.SaveChanges();
            return RedirectToAction("CheckOut");
        }
       
        public async Task<IActionResult> CheckOut()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                var countcart = context.cakecart.Where(x => x.UsersId == value.Id).ToList();
                TempData["count"] = countcart.Count;
                TempData["Hii"] = value.FirstName;
                ViewData["Get"] = HttpContext.Session.GetString("user");

                var user = ViewData["Get"];

                var u = await context.cakeuser.Where(x => x.Phone == user).FirstOrDefaultAsync();

                var products = await context.cakecart.Include(x => x.Item).Where(x => x.UsersId == u.Id).ToListAsync();

                float x = 0;
                foreach (var item in products)
                {
                    x += item.Bill;
                }
                TempData["Total"] = x;

                var data = await context.cakecart.Include(x => x.Customer).Include(x => x.Item).Where(x => x.UsersId == u.Id).ToListAsync();
                ViewBag.cart = data;
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
            
        }
        
    }
}
