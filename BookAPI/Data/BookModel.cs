using Microsoft.EntityFrameworkCore;


namespace BookAPI.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }   
    
    }

    //create class bookcontext which inherits from dbcontext
    public class BookContext : DbContext
    {
        //constructor
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {

        }

        //create a protected method to override the onmodelcreating method of dbcontext
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //create a primary key for the id
            modelBuilder.Entity<Book>().HasKey(x => x.Id);

            //make title, authorname, description required field with max length of 100
            modelBuilder.Entity<Book>().Property(x => x.AuthorName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Book>().Property(x => x.Description).IsRequired().HasMaxLength(100);

            //make genre optional field with max length of 100
            modelBuilder.Entity<Book>().Property(x => x.Genre).HasMaxLength(10);
            
        }


        //create dbset of type bookmodel
        public DbSet<Book> Books { get; set; }
    }


}
