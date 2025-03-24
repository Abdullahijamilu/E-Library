using E_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionsController : ControllerBase
    {
        private readonly ElibraryContext _context;

        public DiscussionsController(ElibraryContext context)
        {
            _context = context;
        }

        [HttpPost("question")]
        public async Task<ActionResult<Discussion>> PostQuestion([FromBody] Discussion discussion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Discussions.Add(discussion);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Discussion posted successfully" });
        }

        [HttpPost("answer")]
        public async Task<ActionResult<Discussion>> PostAnswer([FromBody] Discussion answer)
        {
            if (!ModelState.IsValid)  // Fixed incorrect condition
            {
                return BadRequest(ModelState);
            }

            _context.Discussions.Add(answer);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Answer added successfully" });
        }
    }
}
