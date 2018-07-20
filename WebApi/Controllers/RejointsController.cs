using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DomainPsr03951.Models;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Rejoints")]
    public class RejointsController : Controller
    {
        private readonly psr03951DataBaseContext _context;

        public RejointsController(psr03951DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Rejoints
        [HttpGet]
        public IEnumerable<Rejoint> GetRejoint()
        {
            return _context.Rejoint;
        }

        // GET: api/Rejoints/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRejoint([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rejoint = await _context.Rejoint.FirstOrDefaultAsync(m => m.idUser == id);

            if (rejoint == null)
            {
                return NotFound();
            }

            return Ok(rejoint);
        }

        // PUT: api/Rejoints/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRejoint([FromRoute] int id, [FromBody] Rejoint rejoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rejoint.IdGroup)
            {
                return BadRequest();
            }

            _context.Entry(rejoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RejointExists(id))
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

        // POST: api/Rejoints
        [HttpPost]
        public async Task<IActionResult> PostRejoint([FromBody] Rejoint rejoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Rejoint.Add(rejoint);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RejointExists(rejoint.IdGroup))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRejoint", new { id = rejoint.IdGroup }, rejoint);
        }

        // DELETE: api/Rejoints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRejoint(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var rejoint = await _context.Rejoint.FirstOrDefaultAsync(m => m.idUser == id);
                if (rejoint == null)
                {
                    return NotFound();
                }

                _context.Rejoint.Remove(rejoint);
                await _context.SaveChangesAsync();

                return Ok(rejoint);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool RejointExists(int id)
        {
            return _context.Rejoint.Any(e => e.IdGroup == id);
        }
    }
}