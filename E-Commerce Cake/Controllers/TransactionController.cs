using E_Commerce_Cake.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Cake.Controllers
{
    public class TransactionController : Controller
    {
        private readonly CakeDbContext _context;

        public TransactionController(CakeDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            TempData["gg"] = HttpContext.Session.GetString("user");
            var value = _context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
            TempData["Hii"] = value.FirstName;

            var cakeDbContext = _context.payList.Include(p => p.Inv).Include(p => p.Status).Include(x => x.Inv.Time).Where(x => x.Inv.Customer.Phone == value.Phone);
            return View(await cakeDbContext.ToListAsync());
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TempData["gg"] = HttpContext.Session.GetString("user");
            var value = _context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
            TempData["Hii"] = value.FirstName;
            var payment = await _context.payList
                .Include(p => p.Inv).Include(x => x.Inv.Time)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Transaction/Delete/5

        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _context.payList.FindAsync(id);
            if (payment != null)
            {
                _context.payList.Remove(payment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.payList.Any(e => e.Id == id);
        }
    }
}
