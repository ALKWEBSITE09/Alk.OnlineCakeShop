using E_Commerce_Cake.Models.Database;
using E_Commerce_Cake.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Cake.Controllers
{
    public class CitiesController : Controller
    {
        private readonly CakeDbContext _context;

        public CitiesController(CakeDbContext context)
        {
            _context = context;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var cakeDbContext = _context.City.Include(c => c.States);
                return View(await cakeDbContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                if (id == null)
                {
                    return NotFound();
                }
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var city = await _context.City
                    .Include(c => c.States)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (city == null)
                {
                    return NotFound();
                }

                return View(city);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                ViewData["StateId"] = new SelectList(_context.State, "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CityVM city)
        {
            if (ModelState.IsValid)
            {
                City data = new City
                {
                    Name = city.Name,
                    StateId = city.StateId,
                };
                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StateId"] = new SelectList(_context.State, "Id", "Name", city.StateId);
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var city = await _context.City.FindAsync(id);
                if (city == null)
                {
                    return NotFound();
                }
                ViewData["StateId"] = new SelectList(_context.State, "Id", "Name", city.StateId);
                return View(city);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CityVM city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _context.City.FirstOrDefaultAsync(x => x.Id == city.Id);
                    if (data != null)
                    {
                        data.Name = city.Name;
                        data.StateId = city.StateId;
                    }
                    _context.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Id))
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
            ViewData["StateId"] = new SelectList(_context.State, "Id", "Name", city.StateId);
            return View(city);
        }

        // GET: Cities/Delete/5

        public async Task<IActionResult> Delete(int id)
        {
            var city = await _context.City.FindAsync(id);
            if (city != null)
            {
                _context.City.Remove(city);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(int id)
        {
            return _context.City.Any(e => e.Id == id);
        }
    }
}
