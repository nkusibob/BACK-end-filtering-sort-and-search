using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomainPsr03951.Models;
using WebApplication2.Helper;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using DomainPsr03951;
using System.Text;
using WebApplication2.Model;
using Microsoft.AspNetCore.Cors;
using System.Data;
using System.IO;
using ClosedXML.Excel;
using WebApplication2.Models;
using Microsoft.AspNetCore.Identity;
//using NToastNotify;

namespace WebApplication2.Controllers
{
    
    public class UsersController : Controller
    {
        private readonly psr03951DataBaseContext _context;
        UserApi _api = new UserApi();
        private static List<User> filteredusers = new List<User>();
        private static List<User> UsersFilteredData = new List<User>();
        

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
       // private readonly IToastNotification _toastNotification;
        public UsersController(psr03951DataBaseContext context)
        {
            _context = context;
        }
       // public  IQueryable<User> FilteredUser()
       // {
       //     try
       //     {
       //         var id = Request.Form [("columns[0][search][value]")].FirstOrDefault();
       //         var CountryId = Request.Form [("columns[1][search][value]")].FirstOrDefault();
       //         var FirstName = Request.Form[("columns[2][search][value]")].FirstOrDefault();
       //         var LastName = Request.Form[("columns[3][search][value]")].FirstOrDefault();

       //         var CreationDate = Request.Form[("columns[4][search][value]")].FirstOrDefault();
       //         var EmailAdress = Request.Form[("columns[5][search][value]")].FirstOrDefault();
       //         var Gender = Request.Form[("columns[6][search][value]")].FirstOrDefault();
       //         var PhoneNumber = Request.Form[("columns[7][search][value]")].FirstOrDefault();
       //         var isInactive = Request.Form[("columns[8][search][value]")].FirstOrDefault();
       //         var DeactiveDate = Request.Form[("columns[9][search][value]")].FirstOrDefault();
       //         var GravatarUrl = Request.Form[("columns[10][search][value]")].FirstOrDefault();
       //         var IdGroup = Request.Form[("columns[11][search][value]")].FirstOrDefault();

                


       //         // List<User> userList = User.GetData();
       //         var search = Request.Form[("search[value]")].FirstOrDefault();


       //         var users = _context.User.ToList();

       //         var results = users.AsQueryable();
       //         results = results.Where
       //      (
       //        p => (search == null || (p.id != 0 && p.id.ToString().ToLower().Contains(search.ToLower())) 
       //          || p.CountryId != 0 && p.CountryId.ToString().ToLower().Contains(search.ToLower())
       //          || p.FirstName != null && p.FirstName.ToString().ToLower().Contains(search.ToLower()) 
       //          || p.LastName != null && p.LastName.ToString().ToLower().Contains(search.ToLower())
                 
       //          || p.CreationDate != null && p.CreationDate == StringToDate(search) 
       //          || p.EmailAdress != null && p.EmailAdress.ToString().ToLower().Contains(search.ToLower())
       //          || p.Gender != null && p.Gender.ToString().ToLower().Contains(search.ToLower()) 
       //          || p.PhoneNumber != null && p.PhoneNumber.ToString().ToLower().Contains(search.ToLower())
       //          || p.Country != null && p.Country.ToString().ToLower().Contains(search.ToLower()) 
       //          || p.IdGroup != 0 && p.IdGroup.ToString().ToLower().Contains(search.ToLower())
       //          || p.DeactiveDate != null && p.DeactiveDate == StringToDate(search))
                 


       //       //  && (id == null || (p.id != 0 && p.id.ToString().ToLower().Contains(id.ToLower())))
       //       //  && (CountryId == null || (p.CountryId != 0 && p.CountryId.ToString().ToLower().Contains(CountryId.ToLower())))
       //       //  && (FirstName == null || (p.FirstName != null && p.FirstName.ToString().ToLower().Contains(FirstName.ToLower())))
       //       //&& (LastName == null || (p.LastName.ToString().ToLower().Contains(LastName.ToLower())))
       //       // && (CreationDate == null || (p.CreationDate != null && p.CreationDate.ToString().ToLower().Contains(CreationDate.ToLower())))
       //       // && (EmailAdress == null || (p.EmailAdress.ToString().ToLower().Contains(EmailAdress.ToLower())))
       //       //  && (Gender == null || (p.Gender.ToString().ToLower().Contains(Gender.ToLower())))
       //       //  && (DeactiveDate == null || (p.DeactiveDate.ToString().ToLower().Contains(DeactiveDate.ToLower())))
       //       //  && (Country == null || (p.Country != null && p.Country.ToString().ToLower().Contains(Country.ToLower())))
               




       //      );
       //         return results;
       //     }
       //     catch (Exception EX)
       //     {

       //         throw EX;
       //     }

       // }
      

       // public async Task<IActionResult> UserLoadData()

       //{
       //     //Get parameters
       //     try
       //     {
       //         // get Start (paging start index) and length (page size for paging)
       //         var draw = Request.Form ["draw"].FirstOrDefault();
       //         var start = Request.Form["start"].FirstOrDefault();

       //         var search = Request.Form["search[value]"].FirstOrDefault();
       //         var length = Request.Form[("length")].FirstOrDefault();
       //         //Get Sort columns value
       //         var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
       //         var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
               
       //         int pageSize = length != null ? Convert.ToInt32(length) : 0;
       //         int skip = start != null ? Convert.ToInt32(start) : 0;
       //         int totalRecords = 0;
       //         List<User> v = _context.User.ToList();
       //         // Sorting

       //         if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
       //         {
       //           // v =v.OrderBy(sortColumn + " " + sortColumnDir).ToList();
       //         }

       //         //filtering
       //         var results = FilteredUser();
       //         await Task.Delay(1);
       //         v = results.ToList();
       //         filteredusers = v;
       //         totalRecords = v.Count();
       //         var data = v.Skip(skip).Take(pageSize).ToList();
               
       //         return Json(new {  draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data });


       //     }
       //     catch (Exception ex)
       //     {

       //         throw ex;
       //     }

       // }

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
                UsersFilteredData = data;
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
            //switch (res.StatusCode)
            //{
            //    case HttpStatusCode.Accepted:
            //        _toastNotification.AddSuccessToastMessage("Accepted");
            //        break;
            //    case HttpStatusCode.Ambiguous:
            //        _toastNotification.AddErrorToastMessage("Ambiguous");
            //        break;
            //    case HttpStatusCode.BadGateway:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.BadRequest:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.Conflict:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.Continue:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.Created:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.ExpectationFailed:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.Forbidden:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.Found:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.GatewayTimeout:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.Gone:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.HttpVersionNotSupported:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.InternalServerError:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.LengthRequired:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.MethodNotAllowed:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.Moved:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;

            //    case HttpStatusCode.NoContent:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.NonAuthoritativeInformation:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.NotAcceptable:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.NotFound:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.NotImplemented:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.NotModified:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.OK:
            //        break;
            //    case HttpStatusCode.PartialContent:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.PaymentRequired:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.PreconditionFailed:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.ProxyAuthenticationRequired:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;

            //    case HttpStatusCode.RedirectKeepVerb:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.RedirectMethod:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.RequestedRangeNotSatisfiable:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.RequestEntityTooLarge:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.RequestTimeout:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.RequestUriTooLong:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.ResetContent:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;

            //    case HttpStatusCode.ServiceUnavailable:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.SwitchingProtocols:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;

            //    case HttpStatusCode.Unauthorized:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.UnsupportedMediaType:
            //        break;
            //    case HttpStatusCode.Unused:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.UpgradeRequired:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    case HttpStatusCode.UseProxy:
            //        _toastNotification.AddErrorToastMessage("BadGateway");
            //        break;
            //    default:
            //        break;
            //}
            //var psr03951DataBaseContext = _context.User.Include(u => u.Country).Include(u => u.IdGroupNavigation);
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "CountryName", "CountryId");
            ViewData["IdGroup"] = new SelectList(_context.Group, "id", "Name", "IdGroup");
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
            ViewData["IdGroup"] = new SelectList(_context.Group, "id", "Name");
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
            else
            {
                return BadRequest(ModelState);
            }
            var test = _context.Group;
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "CountryName", user.CountryId);
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
            ViewData["IdGroup"] = new SelectList(_context.Group, "id", "Name", myuser.IdGroup);
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
         
            ViewData["IdGroup"] = new SelectList(_context.Group, "id", "Name", user.IdGroup);
            return View(user);
        }

        // GET: Users/Delete/5
        // GET: Users1/Delete/5
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

        // POST: Users1/Delete/5
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
        [HttpPost]
        public FileResult Export(int? reportID)
        {
            DataTable dt = ExportToExcel.ExportGeneric(UsersFilteredData);
            for (int i = 0; i < dt.Columns.Count; i++)
            {

                if ((dt.Columns[i].ColumnName.ToString().Contains("DATE") || (dt.Columns[i].ColumnName.ToString().Contains("Date"))))
                {
                    var name = dt.Columns[i].ColumnName;
                    if (!(dt.Columns[i].ColumnName.ToString().Contains("FORMAT")))

                    {
                        if (!(dt.Columns[i].ColumnName.ToString().Contains("Edit")))

                        {
                            if (!(dt.Columns[i].ColumnName.ToString().Contains("Delete")))

                            {
                                dt.Columns.Remove(dt.Columns[i].ColumnName.ToString());
                            }

                        }
                    }

                }


            }
            int columnsCount = dt.Columns.Count;
            int desiredCount = columnsCount - 2;
            while (dt.Columns.Count > desiredCount)
            {

                dt.Columns.RemoveAt(desiredCount);

            }
            var fileName ="ExportUsers.xlsx"; //declaration.xlsx";


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
