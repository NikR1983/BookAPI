using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
      
    //create a class bookcontroller which inherits from controllerbase create async methods 
    public class BookController : ControllerBase
    {
        //create a private readonly field of type bookcontext
        private readonly BookContext _context;

        //create a constructor
        public BookController(BookContext context)
        {
            //assign the context to the private field
            _context = context;
        }

        //create a get method to get all the books
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //return all the books
            return Ok(await _context.Books.ToListAsync());
        }

        //create a get method to get a book by id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //find the book by id
            var book = await _context.Books.FindAsync(id);

            //if book is null
            if (book == null)
            {
                //return not found
                return NotFound();
            }

            //return the book
            return Ok(book);
        }

        //create a post method to add a book
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book book)
        {
            //add the book to the database
            _context.Books.Add(book);

            //save the changes
            await _context.SaveChangesAsync();

            //return created
            return StatusCode(StatusCodes.Status201Created);
        }

        //create a put method to update a book
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Book book)
        {
            //find the book by id
            var bookInDb = await _context.Books.FindAsync(id);

            //if book is null
            if (bookInDb == null)
            {
                //return not found
                return NotFound();
            }

            //update the book
            bookInDb.Title = book.Title;
            bookInDb.AuthorName = book.AuthorName;
            bookInDb.Description = book.Description;
            bookInDb.Genre = book.Genre;

            //save the changes
            await _context.SaveChangesAsync();

            //return no content
            return NoContent();
        }

        //create a delete method to delete a book
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //find the book by id
            var bookInDb = await _context.Books.FindAsync(id);

            //if book is null
            if (bookInDb == null)
            {
                //return not found
                return NotFound();
            }

            //remove the book
            _context.Books.Remove(bookInDb);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
