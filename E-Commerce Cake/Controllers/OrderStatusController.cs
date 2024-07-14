using E_Commerce_Cake.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Cake.Controllers
{
    public class OrderStatusController : Controller
    {
        private readonly CakeDbContext _context;

        public OrderStatusController(CakeDbContext context)
        {
            _context = context;
        }

        // GET: OrderStatus
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                return View(await _context.ordersstatus.ToListAsync());

            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // GET: OrderStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                if (id == null)
                {
                    return NotFound();
                }

                var orderStatus = await _context.ordersstatus
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (orderStatus == null)
                {
                    return NotFound();
                }

                return View(orderStatus);

            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }


        // GET: OrderStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                if (id == null)
                {
                    return NotFound();
                }

                var orderStatus = await _context.ordersstatus.FindAsync(id);
                if (orderStatus == null)
                {
                    return NotFound();
                }
                return View(orderStatus);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        // POST: OrderStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] OrderStatus orderStatus)
        {
            if (id != orderStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderStatusExists(orderStatus.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orderStatus);
        }


        private bool OrderStatusExists(int id)
        {
            return _context.ordersstatus.Any(e => e.Id == id);
        }
    }
}
