using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomainPsr03951.Models;

namespace WebApplication1.Controllers
{
    public class TestUsers : Controller
    {
        private readonly psr03951DataBaseContext _context;

        public TestUsers(psr03951DataBaseContext context)
        {
            _context = context;
        }

        // GET: TestUsers
        public async Task<IActionResult> Index()
        {
            var psr03951DataBaseContext = _context.User.Include(u => u.Country).Include(u => u.IdGroupNavigation);
            return View(await psr03951DataBaseContext.ToListAsync());
        }

        // GET: TestUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.Country)
                .Include(u => u.IdGroupNavigation)
                .SingleOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: TestUsers/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "CountryName");
            ViewData["IdGroup"] = new SelectList(_context.Group, "id", "Name");
            return View();
        }

        // POST: TestUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,CountryId,FirstName,LastName,CreationDate,EmailAdress,Gender,PhoneNumber,IsInactive,DeactiveDate,GravatarUrl,IdGroup")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "CountryName", user.CountryId);
            ViewData["IdGroup"] = new SelectList(_context.Group, "id", "Name", user.IdGroup);
            return View(user);
        }

        // GET: TestUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "CountryName", user.CountryId);
            ViewData["IdGroup"] = new SelectList(_context.Group, "id", "Name", user.IdGroup);
            return View(user);
        }

        // POST: TestUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,CountryId,FirstName,LastName,CreationDate,EmailAdress,Gender,PhoneNumber,IsInactive,DeactiveDate,GravatarUrl,IdGroup")] User user)
        {
            if (id != user.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "CountryName", user.CountryId);
            ViewData["IdGroup"] = new SelectList(_context.Group, "id", "Name", user.IdGroup);
            return View(user);
        }

        // GET: TestUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.Country)
                .Include(u => u.IdGroupNavigation)
                .SingleOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: TestUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.SingleOrDefaultAsync(m => m.id == id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.id == id);
        }
    }
}
