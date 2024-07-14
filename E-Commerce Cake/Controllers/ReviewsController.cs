using E_Commerce_Cake.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Cake.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly CakeDbContext _context;

        public ReviewsController(CakeDbContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index(int? id)
        {
            TempData["idprodu"] = id;
            TempData["gg"] = HttpContext.Session.GetString("user");
            var value = _context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
            TempData["Hii"] = value.FirstName;
            var cakeDbContext = _context.review.Include(r => r.Item).Include(r => r.User).Where(x => x.ItemId == id);
            return View(await cakeDbContext.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TempData["gg"] = HttpContext.Session.GetString("user");
            var value = _context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
            TempData["Hii"] = value.FirstName;
            var review = await _context.review
                .Include(r => r.Item)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create(int? id)
        {

            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, Review review)
        {
            if (ModelState.IsValid)
            {
                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = _context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                TempData["Hii"] = value.FirstName;

                Review rv = new Review
                {
                    Description = review.Description,
                    Rating = review.Rating,
                    ItemId = (int)id,
                    UserId = (int)value.Id
                };
                _context.Add(rv);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexCustomer", "Invoices");
            }

            return View(review);
        }



        private bool ReviewExists(int id)
        {
            return _context.review.Any(e => e.Id == id);
        }
    }
}
