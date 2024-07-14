using E_Commerce_Cake.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Cake.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly CakeDbContext _context;

        public InvoicesController(CakeDbContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var cakeDbContext = _context.inv.Include(x => x.Customer).Include(x => x.Time).Include(i => i.Areas).Include(i => i.Citys).Include(i => i.States);
                return View(await cakeDbContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        public async Task<IActionResult> IndexCustomer()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = _context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                TempData["Hii"] = value.FirstName;
                ViewData["Get"] = HttpContext.Session.GetString("user");
                var user = ViewData["Get"];

                var u = await _context.cakeuser.Where(x => x.Phone == user).FirstOrDefaultAsync();
                var cakeDbContext = _context.inv.Include(x => x.Status).Include(x => x.Customer).Include(x => x.Time).Include(i => i.Areas).Include(i => i.Citys).Include(i => i.States);
                return View(await cakeDbContext.Where(x => x.UsertId == u.Id).ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }
        public async Task<IActionResult> Accept(int? id)
        {
            var data = await _context.inv.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                data.Time = data.Time;
                data.CouponCode = data.CouponCode;
                data.UsertId = data.UsertId;
                data.Address = data.Address;
                data.CityId = data.CityId;
                data.StateId = data.StateId;
                data.AreaId = data.AreaId;
                data.Name = data.Name;
                data.Phone = data.Phone;
                data.TotalBill = data.TotalBill;
                data.OrderStatusId = 2;

                _context.inv.Update(data);
                await _context.SaveChangesAsync();

                TempData["Accept"] = data.Customer.Email + "Order Accepted.";

                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> NotAccept(int? id)
        {
            var data = await _context.inv.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                data.Time = data.Time;
                data.CouponCode = data.CouponCode;
                data.UsertId = data.UsertId;
                data.Address = data.Address;
                data.CityId = data.CityId;
                data.StateId = data.StateId;
                data.AreaId = data.AreaId;
                data.Name = data.Name;
                data.Phone = data.Phone;
                data.TotalBill = data.TotalBill;
                data.OrderStatusId = 3;

                _context.inv.Update(data);
                await _context.SaveChangesAsync();

                TempData["NotAccept"] = data.Customer.Email + "Order Not Accepted.";

                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Deliver(int? id)
        {
            var data = await _context.inv.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                data.Time = data.Time;
                data.CouponCode = data.CouponCode;
                data.UsertId = data.UsertId;
                data.Address = data.Address;
                data.CityId = data.CityId;
                data.StateId = data.StateId;
                data.AreaId = data.AreaId;
                data.Name = data.Name;
                data.Phone = data.Phone;
                data.TotalBill = data.TotalBill;
                data.OrderStatusId = 4;

                _context.inv.Update(data);
                await _context.SaveChangesAsync();
                TempData["Deliver"] = data.Customer.Email + "Order Deliver.";
                return RedirectToAction("Index");
            }
            return View();
        }

        // detail admin
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                if (id == null)
                {
                    return NotFound();
                }

                var invoice = await _context.inv
                    .Include(i => i.Areas)
                    .Include(i => i.Citys)
                    .Include(i => i.States)
                    .Include(i => i.Time)
                    .Include(i => i.Customer)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (invoice == null)
                {
                    return NotFound();
                }

                return View(invoice);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }


        //detail Customer
        public async Task<IActionResult> DetailsCustomer(int? id)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = _context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                TempData["Hii"] = value.FirstName;
                if (id == null)
                {
                    return NotFound();
                }

                var invoice = await _context.inv
                    .Include(i => i.Areas)
                    .Include(i => i.Citys)
                    .Include(i => i.States)
                    .Include(i => i.Time)
                    .Include(i => i.Customer)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (invoice == null)
                {
                    return NotFound();
                }

                return View(invoice);
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }



        // delete inv admin

        public async Task<IActionResult> Delete(int id)
        {
            var invoice = await _context.inv.FindAsync(id);

            var order = await _context.cakeorderdetail.Where(x => x.invId == invoice.Id).ToListAsync();

            if (order != null)
            {
                _context.cakeorderdetail.RemoveRange(order);
            }
            if (invoice != null)
            {
                _context.inv.Remove(invoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Delete Inv Customer
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var invoice = await _context.inv.FindAsync(id);

            var order = await _context.cakeorderdetail.Where(x => x.invId == invoice.Id).ToListAsync();

            if (order != null)
            {
                _context.cakeorderdetail.RemoveRange(order);
            }
            if (invoice != null)
            {
                _context.inv.Remove(invoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("IndexCustomer", "Invoices");
        }


        //Cancel Order
        public async Task<IActionResult> DeleteInv(int id)
        {
            var invoice = await _context.inv.FindAsync(id);

            var order = await _context.cakeorderdetail.Where(x => x.invId == invoice.Id).ToListAsync();

            if (order != null)
            {
                _context.cakeorderdetail.RemoveRange(order);
            }
            if (invoice != null)
            {
                _context.inv.Remove(invoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("IndexCustomer", "Invoices");
        }
        private bool InvoiceExists(int id)
        {
            return _context.inv.Any(e => e.Id == id);
        }
    }
}
