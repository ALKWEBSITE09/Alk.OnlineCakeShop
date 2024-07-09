using E_Commerce_Cake.Models.Database;
using E_Commerce_Cake.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;

namespace E_Commerce_Cake.Controllers
{
    public class PaymentController : Controller
    {
        private readonly CakeDbContext context;

        public PaymentController(CakeDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index(int? id)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                var data = context.inv.FirstOrDefault(x => x.Id == id);
                TempData["invId"] = data.Id;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }

        public IActionResult IndexChoice(int? id)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                var data = context.choices.FirstOrDefault(x => x.Id == id);
                TempData["choiceId"] = data.Id;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }
        public async Task<IActionResult> CashOnDelivery(int? id)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                
                TempData["cashon"] = "Your Order Successfully Placed....";
                return RedirectToAction("Create","FeedBacks");
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }

        

        public async Task<IActionResult> Initialorder(int? id)
        {
            var inv = await context.inv.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == id);
            

            if (inv != null)
            {

                string key = "rzp_test_GGnTRTo6WTKmZz";
                string secret = "VWr9672oKV32jTGYBPZMxTth";

                Random random = new Random();
                string TransId = random.Next(0, 1000).ToString();

                Dictionary<string, object> input = new Dictionary<string, object>();
                input.Add("amount", Convert.ToDecimal(inv.TotalBill) * 100);
                input.Add("currency", "INR");
                input.Add("receipt", TransId);

                RazorpayClient client = new RazorpayClient(key, secret);

                Razorpay.Api.Order order = client.Order.Create(input);
                ViewBag.orderId = order["id"].ToString();

                var orderId = ViewBag.orderId;

                Models.Database.Payment payment = new Models.Database.Payment
                {
                    TransId = TransId,
                    OrderId = orderId,
                    StatusId = 1002,
                    InvId = inv.Id
                };

                await context.payList.AddAsync(payment);
                await context.SaveChangesAsync();

                TempData["order"] = "done";
                return View("GetPayment", inv);
            }
            else
            {
                return RedirectToAction("Create", "FeedBacks");
            }
           
        }
    }
}
