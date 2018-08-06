using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomainPsr03951.Models;
using WebApplication1.Helper;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    public class GroupsController : Controller
    {
        private readonly psr03951DataBaseContext _context;
        UserApi _api = new UserApi();
        public GroupsController(psr03951DataBaseContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            return View(await _context.Group.ToListAsync());
        }
        public JsonResult GroupHandler(DTParameters param)
        {
            try
            {
               var dtsource = new List<Group>();

                dtsource = _context.Group.ToList();


                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                List<Group> data = new ResultSetGroup().GetResult(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = new ResultSetGroup().Count(param.Search.Value, dtsource, columnSearch);
                DTResult<Group> result = new DTResult<Group>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = count
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsInactive,DeactivatedDate")] Group @group)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _api.Initial();

                var stringContent = new StringContent(JsonConvert.SerializeObject(@group), Encoding.UTF8, "application/json");

                HttpResponseMessage res = await client.PostAsync("api/Groups/" , stringContent);
                if(res.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Group group = new Group();
            if (id == null)
            {
                return NotFound();
            }
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Groups/" + id);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                group = JsonConvert.DeserializeObject<Group>(result);
                return View(group);

            }
            else {
                return NotFound();
            }
          
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsInactive,DeactivatedDate")] Group @group)
        {
            if (id != @group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    HttpClient client = _api.Initial();

                    var stringContent = new StringContent(JsonConvert.SerializeObject(@group), Encoding.UTF8, "application/json");

                    HttpResponseMessage res = await client.PutAsync("api/Groups/" + id, stringContent);
                    if (res.IsSuccessStatusCode)
                    {

                        return RedirectToAction(nameof(Index));
                    }

                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Id))
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
            return View(@group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @group = await _context.Group.SingleOrDefaultAsync(m => m.Id == id);
            _context.Group.Remove(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.Group.Any(e => e.Id == id);
        }
    }
}
