using E_Library.DTO;
using E_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ElibraryContext _Context;
        public UserController (ElibraryContext context)
        {
            _Context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register(RegisterDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            User user = new User
                (
                    model.Name,
                    model.UniversityId,
                    model.Role
            );

            _Context.Users.Add(user);
            await _Context.SaveChangesAsync();
            return Ok(user);
        }
    }
}
