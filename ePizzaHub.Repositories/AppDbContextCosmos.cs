using ePizzaHub.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Cosmos;



namespace ePizzaHub.Repositories
{
    public class AppDbContextCosmos : DbContext
    {
        public AppDbContextCosmos()
        {

        }
        public AppDbContextCosmos(DbContextOptions<AppDbContextCosmos> options) : base(options)
        {

        }

        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasKey(X => X.Id);
                
           
        }


        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"data source=LAPTOP-KUDMLU21\SqlExpress; initial catalog=ePizzaHub;integrated security=true;");
            }
            base.OnConfiguring(optionsBuilder);
            

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
            }
            base.OnConfiguring(optionsBuilder);
        
    }
*/
    }
}
