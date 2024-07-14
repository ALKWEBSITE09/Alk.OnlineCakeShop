using E_Commerce_Cake.Models.Database;
using E_Commerce_Cake.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Cake.Controllers
{
    public class ChoicesController : Controller
    {
        private readonly CakeDbContext _context;
        private readonly IWebHostEnvironment env;

        public ChoicesController(CakeDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }
        // Admin ----------------------------------------------------------------------------------------->
        // GET: Choices Admin
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var cakeDbContext = _context.choices.Include(c => c.Pc).Include(c => c.Status).Include(x => x.MyProperty);

                if (cakeDbContext == null)
                {
                    return NotFound();
                }
                return View(await cakeDbContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // Delete Choice Admin

        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var choice = await _context.choices.FindAsync(id);
            if (choice != null)
            {
                _context.choices.Remove(choice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // admin change price

        public async Task<IActionResult> EnterPrice(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var data = await _context.choices.FirstOrDefaultAsync(x => x.Id == id);
                if (data != null)
                {
                    PriceEnter p = new PriceEnter
                    {
                        Id = data.Id,
                        priceId = data.PriceId
                    };
                    return View(p);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }
        [HttpPost]
        public async Task<IActionResult> EnterPrice(int? id, PriceEnter pe)
        {
            if (ModelState.IsValid)
            {
                var data = await _context.choices.FirstOrDefaultAsync(x => x.Id == pe.Id);
                if (data != null)
                {
                    data.Quantity = data.Quantity;
                    data.Description = data.Description;
                    data.PriceId = pe.priceId;
                    data.PriceCheckId = data.PriceCheckId;
                    data.ChoiceorderId = data.ChoiceorderId;
                    data.UserId = data.UserId;

                    _context.Update(data);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");

                }
            }
            ViewData["Price"] = new SelectList(_context.Cprice, "Id", "priceId", pe.priceId);
            return View();

        }


        //admin order accept

        public async Task<IActionResult> Accept(int? id)
        {
            var data = await _context.choices.Include(x => x.MyProperty).FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                data.Quantity = data.Quantity;
                data.Description = data.Description;
                data.PriceId = data.PriceId;
                data.PriceCheckId = data.PriceCheckId;
                data.ChoiceorderId = 2;
                data.UserId = data.UserId;
            }
            _context.Update(data);
            await _context.SaveChangesAsync();
            TempData["Accept"] = data.MyProperty.Email + " Order Accepted.";
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Accepted(int? id)
        {
            var data = await _context.choices.Include(x => x.MyProperty).FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                data.Quantity = data.Quantity;
                data.Description = data.Description;
                data.PriceId = data.PriceId;
                data.PriceCheckId = data.PriceCheckId;
                data.ChoiceorderId = 2;
                data.UserId = data.UserId;
            }
            _context.Update(data);
            await _context.SaveChangesAsync();
            TempData["Accept"] = data.MyProperty.Email + " Order Accepted.";
            return RedirectToAction("CashOnDelivery", "Payment");

        }

        //admin order Not accept

        public async Task<IActionResult> NotAccept(int? id)
        {
            var data = await _context.choices.Include(x => x.MyProperty).FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                data.Quantity = data.Quantity;
                data.Description = data.Description;
                data.PriceId = data.PriceId;
                data.PriceCheckId = data.PriceCheckId;
                data.ChoiceorderId = 3;
                data.UserId = data.UserId;
            }
            _context.Update(data);
            await _context.SaveChangesAsync();
            TempData["NotAccept"] = data.MyProperty.Email + "Order Not Accepted.";

            return RedirectToAction("Index");

        }
        //admin deliver order

        public async Task<IActionResult> Deliver(int? id)
        {
            var data = await _context.choices.Include(x => x.MyProperty).FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                data.Quantity = data.Quantity;
                data.Description = data.Description;
                data.PriceId = data.PriceId;
                data.PriceCheckId = data.PriceCheckId;
                data.ChoiceorderId = 4;
                data.UserId = data.UserId;
            }
            _context.Update(data);
            await _context.SaveChangesAsync();
            TempData["Deliver"] = data.MyProperty.Email + "Order Deliver Success...";

            return RedirectToAction("Index");

        }

        // details shows admin

        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                if (id == null)
                {
                    return NotFound();
                }

                var choice = await _context.choices
                    .Include(c => c.Pc)
                    .Include(c => c.Status)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (choice == null)
                {
                    return NotFound();
                }

                return View(choice);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }



        //customer sgow thois sides< ------------------------------------------------------------------------>
        // Customer Choice List

        public async Task<IActionResult> IndexCustomer()
        {

            if (HttpContext.Session.GetString("user") != null)
            {
                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = _context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                var countcart = _context.cakecart.Where(x => x.UsersId == value.Id).ToList();
                TempData["count"] = countcart.Count;
                TempData["Hii"] = value.FirstName;
                ViewData["Get"] = HttpContext.Session.GetString("user");
                var user = ViewData["Get"];

                var u = await _context.cakeuser.Where(x => x.Phone == user).FirstOrDefaultAsync();
                var cakeDbContext = _context.choices.Include(c => c.Pc).Include(c => c.Status);
                return View(await cakeDbContext.Where(x => x.UserId == u.Id).ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }



        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Choices/Details/5

        public async Task<IActionResult> DetailsCustomer(int? id)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = _context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                var countcart = _context.cakecart.Where(x => x.UsersId == value.Id).ToList();
                TempData["count"] = countcart.Count;
                TempData["Hii"] = value.FirstName;
                if (id == null)
                {
                    return NotFound();
                }

                var choice = await _context.choices
                    .Include(c => c.Pc)
                    .Include(c => c.Status)
                    .Include(c => c.subcate)
                    .Include(c => c.MyProperty)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (choice == null)
                {
                    return NotFound();
                }

                return View(choice);
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }

        // Accept Customer 

        public async Task<IActionResult> AcceptCustomer(int? id)
        {
            var data = await _context.choices.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                data.Quantity = data.Quantity;
                data.Description = data.Description;
                data.PriceId = data.PriceId;
                data.PriceCheckId = 1;
                data.ChoiceorderId = data.ChoiceorderId;
                data.UserId = data.UserId;
            }
            _context.Update(data);
            await _context.SaveChangesAsync();
            TempData["Accept"] = "Price Accepted";
            return RedirectToAction(nameof(IndexCustomer));

        }

        // customer Not Accept

        public async Task<IActionResult> NotAcceptCustomer(int? id)
        {
            var data = await _context.choices.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                data.Quantity = data.Quantity;
                data.Description = data.Description;
                data.PriceId = data.PriceId;
                data.PriceCheckId = 2;
                data.ChoiceorderId = data.ChoiceorderId;
                data.UserId = data.UserId;
            }
            _context.Update(data);
            await _context.SaveChangesAsync();
            TempData["NotAccept"] = "Price Not Accepted";

            return RedirectToAction(nameof(IndexCustomer));

        }

        // GET: Choices/Create

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = _context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                var countcart = _context.cakecart.Where(x => x.UsersId == value.Id).ToList();
                TempData["count"] = countcart.Count;
                TempData["Hii"] = value.FirstName;
                ViewData["PriceCheckId"] = new SelectList(_context.PriceCheck, "Id", "Id");
                ViewData["ChoiceorderId"] = new SelectList(_context.ordersstatus, "Id", "Id");
                ViewData["Sub"] = new SelectList(_context.cakesubcategory.Where(x => x.CategoryesId == 1), "Id", "tittle");

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }

        // POST: Choices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChoiceVM choice)
        {
            if (ModelState.IsValid)
            {
                string filepath = "";
                if (choice.Image != null)
                {
                    string uploadFolder = Path.Combine(env.WebRootPath, "Images/Choice/");
                    filepath = Guid.NewGuid().ToString() + "_" + choice.Image.FileName;
                    string fullPath = Path.Combine(uploadFolder, filepath);
                    choice.Image.CopyTo(new FileStream(fullPath, FileMode.Create));

                    ViewData["Get"] = HttpContext.Session.GetString("user");
                    var user = ViewData["Get"];

                    var u = await _context.cakeuser.Where(x => x.Phone == user).FirstOrDefaultAsync();
                    var value = 0;
                    Choice data = new Choice
                    {
                        Image = filepath,
                        Quantity = choice.Quantity,
                        Description = choice.Description,
                        PriceId = value,
                        PriceCheckId = 3,
                        ChoiceorderId = 1,
                        UserId = (int)u.Id,
                        subId = choice.subId,
                    };


                    _context.choices.Add(data);
                    await _context.SaveChangesAsync();
                    TempData["choice"] = "dpme";
                    return RedirectToAction(nameof(IndexCustomer));
                }
            }
            ViewData["PriceCheckId"] = new SelectList(_context.PriceCheck, "Id", "Id", choice.PriceCheckId);
            ViewData["ChoiceorderId"] = new SelectList(_context.ordersstatus, "Id", "Id", choice.ChoiceorderId);
            ViewData["Sub"] = new SelectList(_context.cakesubcategory.Where(x => x.CategoryesId == 1), "Id", "tittle");

            return View(choice);
        }

        // GET: Choices/Delete/5

        public async Task<IActionResult> Delete(int id)
        {
            var choice = await _context.choices.FindAsync(id);
            if (choice != null)
            {
                _context.choices.Remove(choice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexCustomer));
        }

        private bool ChoiceExists(int id)
        {
            return _context.choices.Any(e => e.Id == id);
        }
    }
}
