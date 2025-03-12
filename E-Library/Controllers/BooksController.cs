using E_Library.DTO;
using E_Library.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Reflection.Metadata.BlobBuilder;

namespace E_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ElibraryContext _context;
        public BooksController(ElibraryContext context)
        {
            _context = context;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(string search)
        {
            var books = await _context.Books
                .Where(b => b.Title.Contains(search) || b.Author.Contains(search))
                .ToListAsync();

            if (books == null || books.Count == 0)
            {
                return NotFound("No books found matching your search.");
            }

            return Ok(books);
        }

        [HttpGet("{BookId}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook(int BookId)
        {
            var book = await _context.Books.FindAsync(BookId);
            if (book == null)
            {
                return NotFound("No such book found");
            }
            return Ok(book);
        }
        [HttpGet("{CategoryId}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookCategory(Category CategoryId)
        {
            var book = await _context.Books.FindAsync(CategoryId);
            if (book == null)
            {
                return NotFound("no book from this category");
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(CreateDTOs model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            Book book = new Book
                (
                    model.Title,
                    model.Author,
                    model.Description
                    
                );

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBook), new { id = model }, model);

        }

    }
}
