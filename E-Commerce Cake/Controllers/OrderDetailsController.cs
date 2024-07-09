using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce_Cake.Models.Database;

namespace E_Commerce_Cake.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly CakeDbContext _context;

        public OrderDetailsController(CakeDbContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var cakeDbContext = _context.cakeorderdetail.Include(o => o.inv.Customer).Include(o => o.Item.Cg);
                return View(await cakeDbContext.Where(x => x.invId == id).ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
            
        }

        public async Task<IActionResult> IndexCustomer(int? id)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = _context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                TempData["Hii"] = value.FirstName;
                var cakeDbContext = _context.cakeorderdetail.Include(o => o.inv.Customer).Include(o => o.Item.Cg);
                return View(await cakeDbContext.Where(x => x.invId == id).ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                if (id == null)
                {
                    return NotFound();
                }

                var orderDetail = await _context.cakeorderdetail
                    .Include(o => o.inv.Customer)
                    .Include(o => o.Item)
                    .Include(o => o.Item.Cg)
                    .Include(o => o.Item.Scg)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (orderDetail == null)
                {
                    return NotFound();
                }

                return View(orderDetail);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
            
        }

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

                var orderDetail = await _context.cakeorderdetail
                    .Include(o => o.inv.Customer)
                    .Include(o => o.Item)
                    .Include(o => o.Item.Cg)
                    .Include(o => o.Item.Scg)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (orderDetail == null)
                {
                    return NotFound();
                }

                return View(orderDetail);
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var orderDetail = await _context.cakeorderdetail.FindAsync(id);
            if (orderDetail != null)
            {
                _context.cakeorderdetail.Remove(orderDetail);
            }
            var data = await _context.inv.Where(x => x.Id == orderDetail.invId).FirstOrDefaultAsync();
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
                data.TotalBill = data.TotalBill - orderDetail.SubTotal;
                data.OrderStatusId = data.OrderStatusId;

                _context.Update(data);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Invoices");
        }

        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var orderDetail = await _context.cakeorderdetail.FindAsync(id);
            if (orderDetail != null)
            {
                _context.cakeorderdetail.Remove(orderDetail);
            }
            var data = await _context.inv.Where(x => x.Id == orderDetail.invId).FirstOrDefaultAsync();
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
                data.TotalBill = data.TotalBill - orderDetail.SubTotal;
                data.OrderStatusId = data.OrderStatusId;

                _context.Update(data);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("IndexCustomer","Invoices");
        }

        private bool OrderDetailExists(int id)
        {
            return _context.cakeorderdetail.Any(e => e.Id == id);
        }
    }
}
