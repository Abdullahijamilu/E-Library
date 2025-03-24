using E_Library.DTO;
using E_Library.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
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

        [HttpGet("\"search/{search}\"")]
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

       
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(CreateDTOs model)
        {
            if (model == null)
            {
                return BadRequest(new { Message = "No books was added" });
            }

            Book book = new Book
                (
                    model.Title,
                    model.Author,
                    model.Description,
                    model.FileUrl,
                    model.Year
                );

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook(string id)
        {
            if(!Guid.TryParse(id, out Guid BookId))
            {
                return BadRequest("Invalid id has been passed");
            }
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == BookId);
            if (book == null)
            {
                return NotFound("No such book found");
            }
            return Ok(book);
        }

    }
}
