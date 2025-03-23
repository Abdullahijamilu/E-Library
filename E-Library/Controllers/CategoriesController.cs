using E_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ElibraryContext _context;
        public CategoriesController(ElibraryContext context)
        {
            _context = context;
        }

        [HttpGet("{CategoryId}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetBookCategory(Category CategoryId)
        {
            var book = await _context.Books.FindAsync(CategoryId);
            if (book == null)
            {
                return NotFound("no book from this category");
            }
            return Ok(book);
        }

    }
}
