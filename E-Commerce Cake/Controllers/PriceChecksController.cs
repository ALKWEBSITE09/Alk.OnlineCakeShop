using E_Commerce_Cake.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Cake.Controllers
{
    public class PriceChecksController : Controller
    {
        private readonly CakeDbContext _context;

        public PriceChecksController(CakeDbContext context)
        {
            _context = context;
        }

        // GET: PriceChecks
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                return View(await _context.PriceCheck.ToListAsync());

            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // GET: PriceChecks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                if (id == null)
                {
                    return NotFound();
                }

                var priceCheck = await _context.PriceCheck
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (priceCheck == null)
                {
                    return NotFound();
                }

                return View(priceCheck);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }


        // GET: PriceChecks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                if (id == null)
                {
                    return NotFound();
                }

                var priceCheck = await _context.PriceCheck.FindAsync(id);
                if (priceCheck == null)
                {
                    return NotFound();
                }
                return View(priceCheck);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        // POST: PriceChecks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PriceCheck priceCheck)
        {
            if (id != priceCheck.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priceCheck);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceCheckExists(priceCheck.Id))
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
            return View(priceCheck);
        }



        private bool PriceCheckExists(int id)
        {
            return _context.PriceCheck.Any(e => e.Id == id);
        }
    }
}
