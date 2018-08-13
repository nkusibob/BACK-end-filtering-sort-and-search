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
using ClosedXML.Excel;
using System.IO;
using System.Data;
//using NToastNotify;

namespace WebApplication1.Controllers
{
    public class GroupsController : Controller
    {
        private readonly psr03951DataBaseContext _context;
        UserApi _api = new UserApi();
        private static List<Group> GroupsFilteredData = new List<Group>();

        public GroupsController(psr03951DataBaseContext context)//, IToastNotification toastNotification)
        {
            _context = context;
            //_toastNotification = toastNotification;
        }
       // private readonly IToastNotification _toastNotification;

       
           
       
        // GET: Groups
        public async Task<IActionResult> Index()
        {
            //_toastNotification.AddSuccessToastMessage("Same for success message");
            //// Success with default options (taking into account the overwritten defaults when initializing in Startup.cs)
            //_toastNotification.AddSuccessToastMessage();

            ////Info
            //_toastNotification.AddInfoToastMessage();

            ////Warning
            //_toastNotification.AddWarningToastMessage();

            ////Error
            //_toastNotification.AddErrorToastMessage();
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
                GroupsFilteredData = data;
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
                .SingleOrDefaultAsync(m => m.id == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,IsInactive,DeactivatedDate")] Group @group)
        {
            if (id != @group.id)
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
                    if (!GroupExists(@group.id))
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
            int idbis = 0;
            
            if (id == null)
            {
                return NotFound();
            }
            bool parse = int.TryParse(id.ToString(), out idbis);

            var @group = await _context.Group
                .SingleOrDefaultAsync(m => m.id == id);
            await DeleteConfirmed(idbis);
           
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
            var @group = await _context.Group.SingleOrDefaultAsync(m => m.id == id);
            _context.Group.Remove(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.Group.Any(e => e.id == id);
        }

        [HttpPost]
        public FileResult Export(int? reportID)
        {
            DataTable dt = ExportToExcel.ExportGeneric(GroupsFilteredData);
            for (int i = 0; i < dt.Columns.Count; i++)
            {

                if ((dt.Columns[i].ColumnName.ToString().Contains("DATE") || (dt.Columns[i].ColumnName.ToString().Contains("Date"))))
                {
                    var name = dt.Columns[i].ColumnName;
                    if (!(dt.Columns[i].ColumnName.ToString().Contains("FORMAT")))
                    {
                        dt.Columns.Remove(dt.Columns[i].ColumnName.ToString());
                    }

                }


            }
            int columnsCount = dt.Columns.Count;
            int desiredCount = columnsCount - 1;
            while (dt.Columns.Count > desiredCount)
            {

                dt.Columns.RemoveAt(desiredCount);

            }
            var fileName = "ExportGroups.xlsx"; //declaration.xlsx";


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }
    }
}
