using GlobalHandlerException.Data;
using GlobalHandlerException.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GlobalHandlerException.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut]
        public async Task<ActionResult<User>> Put([FromBody] User user)
        {
            var userEntity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == user.Id);

            if(userEntity == null)
            {
                return NotFound();
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }
    }
}
