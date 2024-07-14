using E_Commerce_Cake.Models.Database;
using E_Commerce_Cake.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Cake.Controllers
{
    public class AreasController : Controller
    {
        private readonly CakeDbContext _context;

        public AreasController(CakeDbContext context)
        {
            _context = context;
        }

        // GET: Areas
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var cakeDbContext = _context.Area.Include(a => a.cities).Include(a => a.cities.States);
                return View(await cakeDbContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        // GET: Areas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var area = await _context.Area
                    .Include(a => a.cities)
                    .Include(a => a.cities.States)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (area == null)
                {
                    return NotFound();
                }

                return View(area);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        // GET: Areas/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                ViewData["CityId"] = new SelectList(_context.City, "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        // POST: Areas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AreaVM area)
        {
            if (ModelState.IsValid)
            {
                Area data = new Area()
                {
                    Name = area.Name,
                    CityId = area.CityId,
                };
                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", area.CityId);
            return View(area);
        }

        // GET: Areas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var area = await _context.Area.FindAsync(id);
                AreaVM areavm = new AreaVM
                {
                    Id = area.Id,
                    Name = area.Name,
                    CityId = area.CityId,
                };
                if (area == null)
                {
                    return NotFound();
                }
                ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", area.CityId);
                return View(areavm);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AreaVM area)
        {
            if (id != area.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _context.Area.FirstOrDefaultAsync(x => x.Id == area.Id);
                    if (data != null)
                    {
                        data.Name = area.Name;
                        data.CityId = area.CityId;
                    }
                    _context.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaExists(area.Id))
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
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", area.CityId);
            return View(area);
        }

        // GET: Areas/Delete/5

        public async Task<IActionResult> Delete(int id)
        {
            var area = await _context.Area.FindAsync(id);
            if (area != null)
            {
                _context.Area.Remove(area);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaExists(int id)
        {
            return _context.Area.Any(e => e.Id == id);
        }
    }
}
