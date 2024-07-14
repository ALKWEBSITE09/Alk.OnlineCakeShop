using E_Commerce_Cake.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Cake.Controllers
{
    public class FeedBacksController : Controller
    {
        private readonly CakeDbContext _context;

        public FeedBacksController(CakeDbContext context)
        {
            _context = context;
        }

        // GET: FeedBacks
        public async Task<IActionResult> Index()
        {
            var cakeDbContext = _context.feedBack.Include(f => f.User);
            return View(await cakeDbContext.ToListAsync());
        }

        // GET: FeedBacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedBack = await _context.feedBack
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedBack == null)
            {
                return NotFound();
            }

            return View(feedBack);
        }

        // GET: FeedBacks/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.cakeuser, "Id", "ConfirmPass");
            return View();
        }

        // POST: FeedBacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeedBack feedBack)
        {
            if (ModelState.IsValid)
            {
                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = _context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                var countcart = _context.cakecart.Where(x => x.UsersId == value.Id).ToList();
                TempData["count"] = countcart.Count;
                TempData["Hii"] = value.FirstName;
                FeedBack fb = new FeedBack
                {
                    Description = feedBack.Description,
                    Rating = feedBack.Rating,
                    UserId = (int)value.Id
                };
                TempData["feed"] = "done";
                _context.Add(fb);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard", "Customer");
            }
            ViewData["UserId"] = new SelectList(_context.cakeuser, "Id", "ConfirmPass", feedBack.UserId);
            return View(feedBack);
        }



        private bool FeedBackExists(int id)
        {
            return _context.feedBack.Any(e => e.Id == id);
        }
    }
}
