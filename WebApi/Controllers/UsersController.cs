using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DomainPsr03951.Models;
//using NToastNotify;
using WebApplication2.Model;
using Microsoft.AspNetCore.Identity;
using WebApplication2.Models;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly psr03951DataBaseContext _context;
        // private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserManager<ApplicationUser> _manager;

        // Inject UserManager using dependency injection.
        // Works only if you choose "Individual user accounts" during project creation.

        // You can also just take part after return and use it in async methods.
        public  async Task<ApplicationUser> GetCurrentUser()
        {
            return await _manager.GetUserAsync(HttpContext.User);
        }
        public UsersController(psr03951DataBaseContext context)
        {
            _context = context;
         
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUser()
        {
            return _context.User;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            
                if (!ModelState.IsValid)
                {
                   // _toastNotification.AddErrorToastMessage("Bad Request");

                    return BadRequest(ModelState);
                }

                var user = await _context.User.SingleOrDefaultAsync(m => m.id == id);

                if (user == null)
                {
                  //  _toastNotification.AddErrorToastMessage("user not found");
                    return NotFound();
                }
            
            
            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> postUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               
                _context.User.Add(new User {
                    CountryId=user.CountryId,
                    FirstName=user.FirstName,
                    LastName=user.LastName,
                    CreationDate=user.CreationDate,
                    EmailAdress= user.EmailAdress,
                    Gender=user.Gender,
                    PhoneNumber=user.PhoneNumber,
                    IsInactive=user.IsInactive,
                    DeactiveDate=user.DeactiveDate,
                    GravatarUrl=user.GravatarUrl,
                    IdGroup=user.IdGroup
                });
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return CreatedAtAction("GetUser", new { id = user.id }, user);
        }

        private Task GetUserAsync()
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.id == id);
        }
    }
}