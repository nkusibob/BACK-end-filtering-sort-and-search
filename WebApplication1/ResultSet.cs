using DomainPsr03951.Models;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;




namespace WebApplication1
{
    public class ResultSet
    {
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
        public List<User> GetResult(string search, string sortOrder, int start, int length, List<User> dtResult, List<string> columnFilters)
        {
           return FilterResult(search, dtResult, columnFilters).Skip(start).Take(length).ToList();
        }

        public int Count(string search, List<User> dtResult, List<string> columnFilters)
        {
            return FilterResult(search, dtResult, columnFilters).Count();
        }
        private IQueryable<User> FilterResult(string search, List<User> dtResult, List<string> columnFilters)
         {
            IQueryable<User> results = dtResult.AsQueryable();

            results = results.Where
            (
              p => (search == null
                || p.id != 0 && p.id.ToString().ToLower().Contains(search.ToLower())
                || p.CountryId != 0 && p.CountryId.ToString().ToLower().Contains(search.ToLower())
                || p.FirstName != null && p.FirstName.ToString().ToLower().Contains(search.ToLower())
                || p.LastName != null && p.LastName.ToString().ToLower().Contains(search.ToLower())

                || p.CreationDate != null && p.CreationDate == StringToDate(search)
                || p.EmailAdress != null && p.EmailAdress.ToString().ToLower().Contains(search.ToLower())
                || p.Gender != null && p.Gender.ToString().ToLower().Contains(search.ToLower())
                || p.PhoneNumber != null && p.PhoneNumber.ToString().ToLower().Contains(search.ToLower())
                || p.IsInactive != null && p.IsInactive.ToString().ToLower().Contains(search.ToLower())

                || p.DeactiveDate != null && p.DeactiveDate == StringToDate(search)
                 || p.GravatarUrl != null && p.GravatarUrl.ToString().ToLower().Contains(search.ToLower())
                 || p.IdGroup != 0 && p.IdGroup.ToString().ToLower().Contains(search.ToLower()))

              && (columnFilters[0] == null || (p.id != 0 && p.id.ToString().ToLower().Contains(columnFilters[0].ToLower())))
              && (columnFilters[1] == null || (p.CountryId != 0 && p.CountryId.ToString().ToLower().Contains(columnFilters[1].ToLower())))
              && (columnFilters[2] == null || (p.FirstName != null && p.FirstName.ToString().ToLower().Contains(columnFilters[2].ToLower())))
              && (columnFilters[3] == null || (p.LastName.ToString().ToLower().Contains(columnFilters[3].ToLower())))
              && (columnFilters[4] == null || (p.CreationDate_FORMAT != null && p.CreationDate_FORMAT.ToString().ToLower().Contains(columnFilters[4].ToLower())))
              && (columnFilters[5] == null || (p.EmailAdress != null && p.EmailAdress.ToString().ToLower().Contains(columnFilters[5].ToLower())))
              && (columnFilters[6] == null || (p.Gender != null && p.Gender.ToString().ToLower().Contains(columnFilters[6].ToLower())))
              && (columnFilters[7] == null || (p.PhoneNumber != null && p.PhoneNumber.ToString().ToLower().Contains(columnFilters[7].ToLower())))
              && (columnFilters[8] == null || (p.IsInactive != null && p.IsInactive.ToString().ToLower().Contains(columnFilters[8].ToLower())))
              && (columnFilters[9] == null || (p.DeactiveDate_FORMAT != null && p.DeactiveDate_FORMAT.ToString().ToLower().Contains(columnFilters[9].ToLower())))

              && (columnFilters[10] == null || p.GravatarUrl != null && (p.GravatarUrl.ToString().ToLower().Contains(columnFilters[10].ToLower())))
              && (columnFilters[11] == null || p.IdGroup != 0 && p.IdGroup.ToString().ToLower().Contains(columnFilters[11].ToLower()))

          


            );
            return results;
        }
    }
}
