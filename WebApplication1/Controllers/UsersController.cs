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
using System.Net;
using DomainPsr03951;
using System.Text;
using WebApplication1.Model;
using Microsoft.AspNetCore.Cors;

namespace WebApplication1.Controllers
{
    
    public class UsersController : Controller
    {
        private readonly psr03951DataBaseContext _context;
        UserApi _api = new UserApi();
        private static List<User> filteredusers = new List<User>();
        public static DateTime StringToDate(string Date)
        {
            try
            {
                return DateTime.Parse(Date);
            }
            catch (FormatException)
            {
                return DateTime.Parse("1/1/0001");
            }
        }
        public UsersController(psr03951DataBaseContext context)
        {
            _context = context;
        }
        public  IQueryable<User> FilteredUser()
        {
            try
            {
                var id = Request.Form [("columns[0][search][value]")].FirstOrDefault();
                var CountryId = Request.Form [("columns[1][search][value]")].FirstOrDefault();
                var FirstName = Request.Form[("columns[2][search][value]")].FirstOrDefault();
                var LastName = Request.Form[("columns[3][search][value]")].FirstOrDefault();

                var CreationDate = Request.Form[("columns[4][search][value]")].FirstOrDefault();
                var EmailAdress = Request.Form[("columns[5][search][value]")].FirstOrDefault();
                var Gender = Request.Form[("columns[6][search][value]")].FirstOrDefault();
                var PhoneNumber = Request.Form[("columns[7][search][value]")].FirstOrDefault();
                var isInactive = Request.Form[("columns[8][search][value]")].FirstOrDefault();
                var DeactiveDate = Request.Form[("columns[9][search][value]")].FirstOrDefault();
                var GravatarUrl = Request.Form[("columns[10][search][value]")].FirstOrDefault();
                var IdGroup = Request.Form[("columns[11][search][value]")].FirstOrDefault();

                


                // List<User> userList = User.GetData();
                var search = Request.Form[("search[value]")].FirstOrDefault();


                var users = _context.User.ToList();

                var results = users.AsQueryable();
                results = results.Where
             (
               p => (search == null || (p.id != 0 && p.id.ToString().ToLower().Contains(search.ToLower())) 
                 || p.CountryId != 0 && p.CountryId.ToString().ToLower().Contains(search.ToLower())
                 || p.FirstName != null && p.FirstName.ToString().ToLower().Contains(search.ToLower()) 
                 || p.LastName != null && p.LastName.ToString().ToLower().Contains(search.ToLower())
                 
                 || p.CreationDate != null && p.CreationDate == StringToDate(search) 
                 || p.EmailAdress != null && p.EmailAdress.ToString().ToLower().Contains(search.ToLower())
                 || p.Gender != null && p.Gender.ToString().ToLower().Contains(search.ToLower()) 
                 || p.PhoneNumber != null && p.PhoneNumber.ToString().ToLower().Contains(search.ToLower())
                 || p.Country != null && p.Country.ToString().ToLower().Contains(search.ToLower()) 
                 || p.IdGroup != 0 && p.IdGroup.ToString().ToLower().Contains(search.ToLower())
                 || p.DeactiveDate != null && p.DeactiveDate == StringToDate(search))
                 


              //  && (id == null || (p.id != 0 && p.id.ToString().ToLower().Contains(id.ToLower())))
              //  && (CountryId == null || (p.CountryId != 0 && p.CountryId.ToString().ToLower().Contains(CountryId.ToLower())))
              //  && (FirstName == null || (p.FirstName != null && p.FirstName.ToString().ToLower().Contains(FirstName.ToLower())))
              //&& (LastName == null || (p.LastName.ToString().ToLower().Contains(LastName.ToLower())))
              // && (CreationDate == null || (p.CreationDate != null && p.CreationDate.ToString().ToLower().Contains(CreationDate.ToLower())))
              // && (EmailAdress == null || (p.EmailAdress.ToString().ToLower().Contains(EmailAdress.ToLower())))
              //  && (Gender == null || (p.Gender.ToString().ToLower().Contains(Gender.ToLower())))
              //  && (DeactiveDate == null || (p.DeactiveDate.ToString().ToLower().Contains(DeactiveDate.ToLower())))
              //  && (Country == null || (p.Country != null && p.Country.ToString().ToLower().Contains(Country.ToLower())))
               




             );
                return results;
            }
            catch (Exception EX)
            {

                throw EX;
            }

        }
      

        public async Task<IActionResult> UserLoadData()

       {
            //Get parameters
            try
            {
                // get Start (paging start index) and length (page size for paging)
                var draw = Request.Form ["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();

                var search = Request.Form["search[value]"].FirstOrDefault();
                var length = Request.Form[("length")].FirstOrDefault();
                //Get Sort columns value
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
               
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int totalRecords = 0;
                List<User> v = _context.User.ToList();
                // Sorting

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                  // v =v.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                }

                //filtering
                var results = FilteredUser();
                await Task.Delay(1);
                v = results.ToList();
                filteredusers = v;
                totalRecords = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
               
                return Json(new {  draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data });


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public JsonResult DataHandler(DTParameters param)
        {
            try
            {
                var dtsource = new List<User>();
                
                    dtsource = _context.User.ToList();
                

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                List<User> data = new ResultSet().GetResult(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = new ResultSet().Count(param.Search.Value, dtsource, columnSearch);
                DTResult<User> result = new DTResult<User>
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
        // GET: Users
        public async Task<IActionResult> Index()
        {

            List<User> users = new List<User>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/users");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                users =  JsonConvert.DeserializeObject<List<User>>(result);
            }
            //var psr03951DataBaseContext = _context.User.Include(u => u.Country).Include(u => u.IdGroupNavigation);
            return View(users);
        }

        // GET: Users/Details/5
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

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "CountryName");
            ViewData["IdGroup"] = new SelectList(_context.Group, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CountryId,FirstName,LastName,CreationDate,EmailAdress,Gender,PhoneNumber,IsInactive,DeactiveDate,GravatarUrl,IdGroup")] User user)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _api.Initial();
               
                var stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                HttpResponseMessage res = await client.PostAsync("api/users", stringContent);
                if (res.IsSuccessStatusCode)
                {

                    return RedirectToAction(nameof(Index));
                }
               
              
            }
            ViewData["CountryId"] = new SelectList(_context.Country, "id", "CountryName", user.CountryId);
            ViewData["IdGroup"] = new SelectList(_context.Group, "id", "Name", user.IdGroup);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            User myuser = new User();
            if (id == null)
            {
                return NotFound();
            }
            HttpClient client = _api.Initial();
            
            HttpResponseMessage res = await client.GetAsync("api/users/"+id);
            if (res.IsSuccessStatusCode)
            {
               
                var result = res.Content.ReadAsStringAsync().Result;
                myuser = JsonConvert.DeserializeObject<User>(result);
            }
           
            //var user = await _context.User.SingleOrDefaultAsync(m => m.Id == id);
            //if (user == null)
            //{
            //    return NotFound();
            //}
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "CountryName", myuser.CountryId);
            ViewData["IdGroup"] = new SelectList(_context.Group, "Id", "Name", myuser.IdGroup);
            return View(myuser);
        }

        // POST: Users/Edit/5
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
                    HttpClient client = _api.Initial();

                    var stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                    HttpResponseMessage res = await client.PutAsync("api/users/" + id, stringContent);
                    if (res.IsSuccessStatusCode)
                    {

                        return RedirectToAction(nameof(Index));
                    }

                   
                    
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
            ViewData["IdGroup"] = new SelectList(_context.Group, "Id", "Name", user.IdGroup);
            return View(user);
        }

        // GET: Users/Delete/5
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

        // POST: Users/Delete/5
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
