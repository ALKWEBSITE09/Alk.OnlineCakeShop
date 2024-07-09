using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce_Cake.Models.Database;
using E_Commerce_Cake.Models.ViewModel;

namespace E_Commerce_Cake.Controllers
{
    public class UserTypesController : Controller
    {
        private readonly CakeDbContext _context;

        public UserTypesController(CakeDbContext context)
        {
            _context = context;
        }

        // GET: UserTypes
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");

                var data = await _context.cakeusertype.ToListAsync();
                var value = await _context.cakeusertype.Where(x => x.Id == 2).FirstOrDefaultAsync();

                TempData["Customer"] = value.Type;
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // GET: UserTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");

                if (id == null)
                {
                    return NotFound();
                }

                var userType = await _context.cakeusertype.FirstOrDefaultAsync(m => m.Id == id);
                if (userType == null)
                {
                    return NotFound();
                }

                return View(userType);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // GET: UserTypes/Create
        public IActionResult Create()
        {
            TempData["Hii"] = HttpContext.Session.GetString("admin");
            return View();

        }

        // POST: UserTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserTypeVM userType)
        {
            if (ModelState.IsValid)
            {
                UserType data = new UserType
                {
                    Type = userType.Type,
                };

                _context.cakeusertype.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userType);
        }

        // GET: UserTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");

                if (id == null)
                {
                    return NotFound();
                }

                var userType = await _context.cakeusertype.FindAsync(id);

                UserTypeVM data = new UserTypeVM
                {
                    Type = userType.Type,
                };

                if (userType == null)
                {
                    return NotFound();
                }
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // POST: UserTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,UserTypeVM userType)
        {
            if (id != userType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.cakeusertype.FirstOrDefaultAsync(x => x.Id == id);

                    if (user != null)
                    {
                        user.Type = userType.Type;
                    }
                    _context.cakeusertype.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTypeExists(userType.Id))
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
            return View(userType);
        }

        // GET: UserTypes/Delete/5
        
        public async Task<IActionResult> Delete(int id)
        {
            var userType = await _context.cakeusertype.FindAsync(id);
            if (userType != null)
            {
                _context.cakeusertype.Remove(userType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTypeExists(int id)
        {
            return _context.cakeusertype.Any(e => e.Id == id);
        }
    }
}
