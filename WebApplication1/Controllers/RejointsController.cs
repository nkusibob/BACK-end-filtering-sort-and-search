using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomainPsr03951.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using WebApplication1.Helper;

namespace WebApplication1.Controllers
{
    public class RejointsController : Controller
    {
        private readonly psr03951DataBaseContext _context;
        UserApi _api = new UserApi();
        public RejointsController(psr03951DataBaseContext context)
        {
            _context = context;
        }

        // GET: Rejoints
        public  ActionResult Index()
        {
            var usersperGroup =  _context.Rejoint.FromSql("EXECUTE dbo.sp_userOtherGroup").ToList();
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
                .SingleOrDefaultAsync(m => m.idUser == id);
            if (rejoint == null)
            {
                return NotFound();
            }

            return View(rejoint);
        }

        // GET: Rejoints/Create
        public IActionResult Create()
        {
            var user = _context.User;
            ViewBag.users = user.ToList();
            ViewData["IdGroup"] = new SelectList(_context.Group, "Id", "Name");
            ViewData["IdUser"] = new SelectList(_context.User, "id", "LastName");
            return View();
        }

        // POST: Rejoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGroup,idUser")] Rejoint rejoint)
        {
            if (ModelState.IsValid)
            {

                HttpClient client = _api.Initial();

                var stringContent = new StringContent(JsonConvert.SerializeObject(rejoint), Encoding.UTF8, "application/json");

                HttpResponseMessage res = await client.PostAsync("api/Rejoints", stringContent);
                if (res.IsSuccessStatusCode)
                {

                    return RedirectToAction(nameof(Index));
                }

               
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

            var rejoint = await _context.Rejoint.SingleOrDefaultAsync(m => m.idUser == id);
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
            Rejoint rejoint = new Rejoint();
            if (id == null)
            {
                return NotFound();
            }
            HttpClient client = _api.Initial();

            HttpResponseMessage res = await client.GetAsync("api/Rejoints/" + id);
            if (res.IsSuccessStatusCode)
            {

                var result = res.Content.ReadAsStringAsync().Result;
                rejoint = JsonConvert.DeserializeObject<Rejoint>(result);
                return View(rejoint);
            }
            else
            {
                return NotFound();
            }


           

          
           
        }

        // POST: Rejoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                HttpClient client = _api.Initial();


                HttpResponseMessage res = await client.DeleteAsync("api/Rejoints/" + id);
                if (res.IsSuccessStatusCode)
                {

                    return RedirectToAction(nameof(Index));
                }
 
            }
            catch (Exception ex)
            {

                throw ex;
            };
            return RedirectToAction(nameof(Index));
        }

        private bool RejointExists(int id)
        {
            return _context.Rejoint.Any(e => e.IdGroup == id);
        }
    }
}
