using E_Commerce_Cake.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Cake.Controllers
{
    public class OrderController : Controller
    {
        private readonly CakeDbContext context;

        public OrderController(CakeDbContext context)
        {
            this.context = context;
        }

        // Order List

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                var countcart = context.cakecart.Where(x => x.UsersId == value.Id).ToList();
                TempData["count"] = countcart.Count;
                TempData["Hii"] = value.FirstName;
                ViewBag.Coupon = new SelectList(context.cakecoupon, "CouponCode", "CouponCode");
                ViewBag.City = new SelectList(context.City, "Id", "Name");
                ViewBag.State = new SelectList(context.State, "Id", "Name");
                ViewBag.Area = new SelectList(context.Area, "Id", "Name");

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Index(string address, int city, int area, int state, string phone, string name, string coupon)
        {
            ViewData["Get"] = HttpContext.Session.GetString("user");
            var user = ViewData["Get"];

            var u = await context.cakeuser.Where(x => x.Phone == user).FirstOrDefaultAsync();

            var products = await context.cakecart.Where(x => x.UsersId == u.Id).ToListAsync();

            float billtotal = 0;
            foreach (var item in products)
            {
                billtotal += item.Bill;
            }
            ViewData["Total"] = billtotal;


            var data = await context.cakecart.Include(x => x.Customer).Include(x => x.Item).Where(x => x.UsersId == u.Id).ToListAsync();

            var coupons = await context.cakecoupon.Where(x => x.CouponCode == coupon).FirstOrDefaultAsync();

            if (coupons == null)
            {
                OrderTime times = new OrderTime
                {
                    Name = System.DateTime.Now
                };

                context.ordertimes.Add(times);
                await context.SaveChangesAsync();


                Invoice bills = new Invoice()
                {
                    TimeId = times.Id,
                    CouponCode = null,
                    UsertId = (int)u.Id,
                    Address = address,
                    CityId = city,
                    AreaId = area,
                    StateId = state,
                    Phone = phone,
                    Name = name,
                    TotalBill = billtotal,
                    OrderStatusId = 1

                };


                context.inv.Add(bills);
                await context.SaveChangesAsync();

                var value = await context.ordertimes.FirstOrDefaultAsync(x => x.Id == bills.TimeId);
                var inv = await context.inv.Where(c => c.UsertId == u.Id && c.TimeId == value.Id).FirstOrDefaultAsync();

                foreach (var item in products)
                {
                    OrderDetail detail = new OrderDetail()
                    {
                        itemId = item.itemId,
                        invId = inv.Id,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        SubTotal = item.Quantity * item.Price
                    };
                    context.cakeorderdetail.Add(detail);

                }
                await context.SaveChangesAsync();

                var carts = await context.cakecart.Where(x => x.UsersId == u.Id).ToListAsync();

                if (carts.Any())
                {
                    context.cakecart.RemoveRange(carts);
                }
                await context.SaveChangesAsync();
                return RedirectToAction("OrderDetail", "Order", new { id = inv.Id });
            }
            else
            {
                return NotFound();
            }
            return View();
        }



        //orderdetail
        public async Task<IActionResult> OrderDetail(int? id)
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

                var order = await context.cakeorderdetail.Include(x => x.Item.Cg).Include(x => x.inv).Include(x => x.inv.Customer).Include(x => x.Item).Where(x => x.invId == id).ToListAsync();
                if (order == null)
                {
                    return NotFound();
                }
                TempData["invid"] = id;
                return View(order);
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }

        public IActionResult Coupon(int? id, string coupon)
        {
            var data = context.cakecoupon.FirstOrDefault(x => x.CouponCode == coupon);
            if (data != null)
            {

                var inv = context.inv.Where(x => x.Id == id).FirstOrDefault();

                if (data.Type == CouponType.Currency)
                {
                    if (inv.TotalBill >= data.MinimumAmount && data.NoofCoupon > 0 && data.IsActive == true)
                    {
                        inv.TotalBill = inv.TotalBill - data.Discount;
                        inv.Name = inv.Name;
                        inv.UsertId = inv.UsertId;
                        inv.Address = inv.Address;
                        inv.StateId = inv.StateId;
                        inv.CityId = inv.CityId;
                        inv.AreaId = inv.AreaId;
                        inv.Phone = inv.Phone;

                        data.CouponImage = data.CouponImage;
                        data.CouponCode = data.CouponCode;
                        data.NoofCoupon = data.NoofCoupon - 1;
                        data.Type = data.Type;
                        data.Discount = data.Discount;
                        data.MinimumAmount = data.MinimumAmount;
                        data.IsActive = data.IsActive;

                        context.Update(inv);
                        context.Update(data);
                        TempData["couponapply"] = "done;";

                    }
                    else
                    {
                        TempData["ccc"] = "coupon not active";
                    }
                }
                else
                {
                    if (inv.TotalBill >= data.MinimumAmount && data.NoofCoupon > 0 && data.IsActive == true)
                    {
                        inv.TotalBill = inv.TotalBill - (inv.TotalBill * (data.Discount / 100));
                        inv.Name = inv.Name;
                        inv.UsertId = inv.UsertId;
                        inv.Address = inv.Address;
                        inv.StateId = inv.StateId;
                        inv.CityId = inv.CityId;
                        inv.AreaId = inv.AreaId;
                        inv.Phone = inv.Phone;

                        data.CouponImage = data.CouponImage;
                        data.CouponCode = data.CouponCode;
                        data.NoofCoupon = data.NoofCoupon - 1;
                        data.Type = data.Type;
                        data.Discount = data.Discount;
                        data.MinimumAmount = data.MinimumAmount;
                        data.IsActive = data.IsActive;

                        context.Update(inv);
                        context.Update(data);
                        TempData["couponapply"] = "done;";
                    }
                    else
                    {
                        TempData["ccc"] = "coupon not active";
                    }
                }
                context.SaveChanges();

            }
            else
            {
                TempData["coupon"] = "coupon code invalid";
            }
            return RedirectToAction("Invoicebill", "Order", new { id = id });
        }

        //invbills
        public async Task<IActionResult> Invoicebill(int? id)
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
                var inv = await context.inv.Include(x => x.Time).Include(x => x.Citys).Include(x => x.States).Include(x => x.Areas).Where(x => x.UsertId == u.Id && x.Id == id).FirstOrDefaultAsync();
                if (inv == null)
                {
                    return NotFound();
                }

                return View(inv);
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }

    }
}
