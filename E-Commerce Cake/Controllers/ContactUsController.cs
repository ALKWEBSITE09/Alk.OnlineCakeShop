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
    public class ContactUsController : Controller
    {
        private readonly CakeDbContext _context;

        public ContactUsController(CakeDbContext context)
        {
            _context = context;
        }

        // GET: ContactUs
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                return View(await _context.ContactUs.ToListAsync());

            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // GET: ContactUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                if (id == null)
                {
                    return NotFound();
                }

                var contactUs = await _context.ContactUs
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (contactUs == null)
                {
                    return NotFound();
                }
                return View(contactUs);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // GET: ContactUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactUsVM contactUs)
        {
            if (ModelState.IsValid)
            {
                ContactUs data = new ContactUs
                {
                    Email = contactUs.Email,
                    Name = contactUs.Name,
                    Description = contactUs.Description
                };
                _context.ContactUs.Add(data);
                await _context.SaveChangesAsync();
                ViewBag.ContactUs = "message successfully send.";
                return View();
            }
            return View();
        }

        // GET: ContactUs/Edit/5

        public IActionResult _Create()
        {
            return PartialView();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _Create(ContactUsVM contactUs)
        {
            if (ModelState.IsValid)
            {
                ContactUs data = new ContactUs
                {
                    Email = contactUs.Email,
                    Name = contactUs.Name,
                    Description = contactUs.Description
                };
                _context.ContactUs.Add(data);
                await _context.SaveChangesAsync();
                ViewBag.ContactUs = "message successfully send.";
                return PartialView();
            }
            return PartialView(contactUs);
        }

        // GET: ContactUs/Delete/5

        public async Task<IActionResult> Delete(int id)
        {
            var contactUs = await _context.ContactUs.FindAsync(id);
            if (contactUs != null)
            {
                _context.ContactUs.Remove(contactUs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactUsExists(int id)
        {
            return _context.ContactUs.Any(e => e.Id == id);
        }
    }
}
