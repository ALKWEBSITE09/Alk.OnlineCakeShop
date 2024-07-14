using E_Commerce_Cake.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Cake.Models.Database
{
    public class CakeDbContext : DbContext
    {
        public CakeDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<UserType> cakeusertype { get; set; }
        public DbSet<User> cakeuser { get; set; }
        public DbSet<Admin> cakeadmin { get; set; }
        public DbSet<Category> cakecategory { get; set; }
        public DbSet<SubCategory> cakesubcategory { get; set; }
        public DbSet<Product> cakeproduct { get; set; }
        public DbSet<ProductCart> cakecart { get; set; }
        public DbSet<Coupon> cakecoupon { get; set; }
        public DbSet<Invoice> inv { get; set; }
        public DbSet<OrderDetail> cakeorderdetail { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<OrderStatus> ordersstatus { get; set; }
        public DbSet<Choice> choices { get; set; }
        public DbSet<ChoicePrice> Cprice { get; set; }
        public DbSet<PriceCheck> PriceCheck { get; set; }
        public DbSet<PriceEnter> PriceEnter { get; set; }
        public DbSet<OrderTime> ordertimes { get; set; }
        public DbSet<Favorate> favo { get; set; }
        public DbSet<Payment> payList { get; set; }
        public DbSet<FeedBack> feedBack { get; set; }
        public DbSet<Review> review { get; set; }
    }
}
