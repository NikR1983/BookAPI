using Microsoft.EntityFrameworkCore;

namespace BookAPI.Data
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string ModelName { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }   
        public int Year { get; set; }
        public int Price { get; set; }
    
    }

    //create class CarContext which inherits from dbcontext
    public class CarContext : DbContext
    {
        //constructor
        public CarContext(DbContextOptions<CarContext> options) : base(options)
        {

        }

        //create a protected method to override the onmodelcreating method of dbcontext
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //create a primary key for the id
            modelBuilder.Entity<Car>().HasKey(x => x.Id);

            //make make, modelname, description required field with max length of 100
            modelBuilder.Entity<Car>().Property(x => x.Make).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Car>().Property(x => x.ModelName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Car>().Property(x => x.Description).IsRequired().HasMaxLength(100);

            //make color optional field with max length of 100
            modelBuilder.Entity<Car>().Property(x => x.Color).HasMaxLength(10);

            //make year and price required
            modelBuilder.Entity<Car>().Property(x => x.Year).IsRequired();
            modelBuilder.Entity<Car>().Property(x => x.Price).IsRequired();

            
        }

        //create dbset of type car
        public DbSet<Car> Cars { get; set; }
    }
}
