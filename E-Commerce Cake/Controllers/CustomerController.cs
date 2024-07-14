using E_Commerce_Cake.Models.Database;
using E_Commerce_Cake.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Cake.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CakeDbContext context;
        private readonly IWebHostEnvironment env;

        public CustomerController(CakeDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["Hii"] = HttpContext.Session.GetString("admin");
                var data = await context.cakeuser.Include(x => x.customer).Where(x => x.UserTypesId == 2).ToListAsync();
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        // User Register

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserVM user)
        {
            if (ModelState.IsValid)
            {
                User data = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    BirthDate = user.BirthDate,
                    Username = user.Username,
                    Pass = user.Pass,
                    ConfirmPass = user.ConfirmPass,
                    UserTypesId = 2,
                };
                var phone = await context.cakeuser.FirstOrDefaultAsync(x => x.Phone == user.Phone);
                var email = await context.cakeuser.FirstOrDefaultAsync(x => x.Email == user.Email);

                if (phone != null || email != null)
                {
                    if (phone != null)
                    {
                        TempData["phone"] = user.Phone + "Already Create Accont";
                    }
                    else
                    {
                        TempData["email"] = user.Email + "Already Create Accont";

                    }
                }
                else
                {
                    await context.cakeuser.AddAsync(data);
                    await context.SaveChangesAsync();
                    TempData["Register"] = "You Are Successfully Register";
                    return RedirectToAction("Login", "Customer");
                }
            }
            return View();
        }



        // User Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var value = await context.cakeuser.Where(x => x.Phone == user.Phone && x.Pass == user.Pass).FirstOrDefaultAsync();
            if (value != null)
            {
                HttpContext.Session.SetString("user", value.Phone);
                TempData["Data2"] = "You Are Successfully Login.......";
                return RedirectToAction("Dashboard", "Customer");
            }
            else
            {
                ViewData["Not"] = "Data Not Found";
                return View();
            }

        }

        // User Logout

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                HttpContext.Session.Remove("user");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // User Dashboard

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                var countcart = context.cakecart.Where(x => x.UsersId == value.Id).ToList();
                TempData["count"] = countcart.Count;
                TempData["Hii"] = value.FirstName;
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
            return View();
        }

        //Customer User Account

        public async Task<IActionResult> CustomerUser(string? id)
        {
            if (HttpContext.Session.GetString("user") != null)
            {

                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                TempData["Hii"] = value.FirstName;

                if (id == null)
                {
                    return NotFound();
                }
                var data = await context.cakeuser.FirstOrDefaultAsync(x => x.Phone == id);

                if (data == null)
                {
                    return NotFound();
                }
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }


        public async Task<IActionResult> EditUser(int? id)
        {
            if (HttpContext.Session.GetString("user") != null)
            {

                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                var countcart = context.cakecart.Where(x => x.UsersId == value.Id).ToList();
                TempData["count"] = countcart.Count;
                TempData["Hii"] = value.FirstName;

                if (id == null || context.cakeuser == null)
                {
                    return NotFound();
                }
                var data = await context.cakeuser.FindAsync(id);

                string image = "/jpg";
                byte[] imagebyte = Convert.FromBase64String(image);

                using (MemoryStream stream = new MemoryStream(imagebyte))
                {
                    IFormFile file = new FormFile(stream, 0, imagebyte.Length, "photo", data.Image);

                    UserVM value2 = new UserVM
                    {
                        Id = data.Id,
                        photo = file,
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        Email = data.Email,
                        Username = data.Username,
                        Phone = data.Phone,
                        ConfirmPass = data.ConfirmPass,
                        Pass = data.Pass,
                        BirthDate = data.BirthDate,
                        UserTypesId = 2
                    };
                    return View(value2);

                }

                if (data == null)
                {
                    return NotFound();
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }
        [HttpPost]
        public async Task<IActionResult> EditUser(int? id, UserVM item)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                if (ModelState.IsValid)
                {
                    TempData["gg"] = HttpContext.Session.GetString("user");
                    var value = context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                    TempData["Hii"] = value.FirstName;

                    var data = await context.cakeuser.FirstOrDefaultAsync(x => x.Id == item.Id);
                    string filepath = "";
                    if (item.photo != null)
                    {
                        string uploadFolder = Path.Combine(env.WebRootPath, "Images/User/");
                        filepath = Guid.NewGuid().ToString() + "_" + item.photo.FileName;
                        string fullPath = Path.Combine(uploadFolder, filepath);
                        item.photo.CopyTo(new FileStream(fullPath, FileMode.Create));

                        if (data != null)
                        {
                            data.Username = item.Username;
                            data.FirstName = item.FirstName;
                            data.LastName = item.LastName;
                            data.Email = item.Email;
                            data.Phone = item.Phone;
                            data.Pass = item.Pass;
                            data.ConfirmPass = item.ConfirmPass;
                            data.BirthDate = item.BirthDate;
                            data.UserTypesId = 2;
                            data.Image = filepath;

                            context.cakeuser.Update(data);
                            await context.SaveChangesAsync();
                            TempData["edit"] = "Data Successfully Update...";
                            return RedirectToAction("CustomerUser", "Customer", new { id = data.Phone });
                        }

                    }
                }
                return View(item);
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }

        public IActionResult MobileCheck()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MobileCheck(User item)
        {
            var data = await context.cakeuser.FirstOrDefaultAsync(x => x.Phone == item.Phone);
            if (data != null)
            {
                TempData["Pass"] = data.Id;
                return RedirectToAction("Page", "Customer");
            }
            else
            {
                ViewBag.msg = "Mobile Number Not Match.";
                return View();
            }
        }
        public IActionResult Page()
        {
            return View();
        }

        public async Task<IActionResult> ForgotPass()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPass(int? id, UserVM item)
        {
            var data = await context.cakeuser.FirstOrDefaultAsync(x => x.Id == id);

            if (data != null)
            {
                data.FirstName = data.FirstName;
                data.LastName = data.LastName;
                data.Email = data.Email;
                data.Phone = data.Phone;
                data.BirthDate = data.BirthDate;
                data.Username = data.Username;
                data.Pass = item.Pass;
                data.ConfirmPass = item.ConfirmPass;
                data.UserTypesId = 2;

                context.cakeuser.Update(data);
                await context.SaveChangesAsync();
                TempData["Forgot"] = "done";
                return RedirectToAction("Login", "Customer");
            }
            return View();
        }




        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                var data = await context.cakeuser.FindAsync(id);
                if (data != null)
                {
                    context.cakeuser.Remove(data);
                }
                await context.SaveChangesAsync();
                HttpContext.Session.Remove("user");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["gg"] = HttpContext.Session.GetString("user");
                var value = context.cakeuser.FirstOrDefault(x => x.Phone == TempData["gg"]);
                var countcart = context.cakecart.Where(x => x.UsersId == value.Id).ToList();
                TempData["count"] = countcart.Count;
                TempData["Hii"] = value.FirstName;
                var data = await context.cakeuser.FirstOrDefaultAsync(x => x.Id == id);
                if (data == null)
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
    }
}
