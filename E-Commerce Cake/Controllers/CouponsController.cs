using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce_Cake.Models.Database;
using E_Commerce_Cake.Models.ViewModel;
using System.Runtime.CompilerServices;

namespace E_Commerce_Cake.Controllers
{
    public class CouponsController : Controller
    {
        private readonly CakeDbContext _context;
        private readonly IWebHostEnvironment env;

        public CouponsController(CakeDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Coupons
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                return View(await _context.cakecoupon.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // GET: Coupons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var coupon = await _context.cakecoupon
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (coupon == null)
                {
                    return NotFound();
                }

                return View(coupon);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // GET: Coupons/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // POST: Coupons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CouponVM coupon)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            coupon.CouponCode = finalString;
           

            if (ModelState.IsValid)
            {

                string filepath = "";
                if (coupon.CouponImage != null)
                {
                    string uploadFolder = Path.Combine(env.WebRootPath, "Images/Coupons/");
                    filepath = Guid.NewGuid().ToString() + "_" + coupon.CouponImage.FileName;
                    string fullPath = Path.Combine(uploadFolder, filepath);
                    coupon.CouponImage.CopyTo(new FileStream(fullPath, FileMode.Create));


                    


                    Coupon data = new Coupon
                    {
                        Id = coupon.Id,
                        Name = coupon.Tittle.ToString(),
                        NoofCoupon = coupon.NoofCoupon,
                        Type = coupon.Type,
                        CouponCode = coupon.CouponCode,
                        Discount = coupon.Discount,
                        MinimumAmount = coupon.MinimumAmount,
                        CouponImage = filepath,
                        IsActive = coupon.IsActive,
                    };


                    _context.cakecoupon.Add(data);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(coupon);
        }

            // GET: Coupons/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var coupon = await _context.cakecoupon.FirstOrDefaultAsync(x => x.Id == id);

                if (coupon != null)
                {
                    CouponVM data = new CouponVM
                    {
                        Id = coupon.Id,
                        Tittle = coupon.Name,
                        NoofCoupon = coupon.NoofCoupon,
                        CouponCode = coupon.CouponCode,
                        Type = coupon.Type,
                        Discount = coupon.Discount,
                        MinimumAmount = coupon.MinimumAmount,
                        IsActive = coupon.IsActive,
                    };

                    return View(data);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // POST: Coupons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,CouponVM vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var item = await _context.cakecoupon.FirstOrDefaultAsync(x => x.Id == vm.Id);
                    string filepath = "";
                    if (vm.CouponImage != null)
                    {
                        string uploadFolder = Path.Combine(env.WebRootPath, "Images/Coupons/");
                        filepath = Guid.NewGuid().ToString() + "_" + vm.CouponImage.FileName;
                        string fullPath = Path.Combine(uploadFolder, filepath);
                        vm.CouponImage.CopyTo(new FileStream(fullPath, FileMode.Create));

                        item.Name = vm.Tittle;
                        item.NoofCoupon = item.NoofCoupon;
                        item.Type = vm.Type;
                        item.MinimumAmount = vm.MinimumAmount;
                        item.CouponCode = item.CouponCode;
                        item.CouponImage = filepath;
                        item.IsActive = vm.IsActive;
                        item.Discount = vm.Discount;

                        _context.cakecoupon.Update(item);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        item.Name = vm.Tittle;
                        item.NoofCoupon = vm.NoofCoupon;
                        item.Type = vm.Type;
                        item.MinimumAmount = vm.MinimumAmount;
                        item.CouponCode = item.CouponCode;
                        item.CouponImage = item.CouponImage;
                        item.IsActive = vm.IsActive;
                        item.Discount = vm.Discount;


                        _context.cakecoupon.Update(item);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CouponExists(vm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(vm);
        }

        // GET: Coupons/Delete/5
       
        public async Task<IActionResult> Delete(int id)
        {
            var coupon = await _context.cakecoupon.FindAsync(id);
            if (coupon != null)
            {
                _context.cakecoupon.Remove(coupon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CouponExists(int id)
        {
            return _context.cakecoupon.Any(e => e.Id == id);
        }
    }
}
