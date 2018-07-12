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
    public class RejointsController : Controller
    {
        private readonly psr03951DataBaseContext _context;

        public RejointsController(psr03951DataBaseContext context)
        {
            _context = context;
        }

        // GET: Rejoints
        public  ActionResult Index()
        {
            var usersperGroup =  _context.Rejoint.FromSql("EXECUTE dbo.sp_userPerGroup").ToList();
            ViewBag.userPerGroup = usersperGroup;
            return View(usersperGroup);
        }

        // GET: Rejoints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rejoint = await _context.Rejoint
                .SingleOrDefaultAsync(m => m.IdGroup == id);
            if (rejoint == null)
            {
                return NotFound();
            }

            return View(rejoint);
        }

        // GET: Rejoints/Create
        public IActionResult Create()
        {
            ViewData["IdGroup"] = new SelectList(_context.Group, "Id", "Name");
            return View();
        }

        // POST: Rejoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGroup,IdUser")] Rejoint rejoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rejoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(rejoint);
        }

        // GET: Rejoints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rejoint = await _context.Rejoint.SingleOrDefaultAsync(m => m.IdGroup == id);
            if (rejoint == null)
            {
                return NotFound();
            }
            return View(rejoint);
        }

        // POST: Rejoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGroup,IdUser")] Rejoint rejoint)
        {
            if (id != rejoint.IdGroup)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rejoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RejointExists(rejoint.IdGroup))
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
            return View(rejoint);
        }

        // GET: Rejoints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rejoint = await _context.Rejoint
                .SingleOrDefaultAsync(m => m.IdGroup == id);
            if (rejoint == null)
            {
                return NotFound();
            }

            return View(rejoint);
        }

        // POST: Rejoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rejoint = await _context.Rejoint.SingleOrDefaultAsync(m => m.IdGroup == id);
            _context.Rejoint.Remove(rejoint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RejointExists(int id)
        {
            return _context.Rejoint.Any(e => e.IdGroup == id);
        }
    }
}
