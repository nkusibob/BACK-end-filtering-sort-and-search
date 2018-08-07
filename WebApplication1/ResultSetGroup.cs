using DomainPsr03951.Models;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;




namespace WebApplication1
{
    public class ResultSetGroup
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
        public List<Group> GetResult(string search, string sortOrder, int start, int length, List<Group> dtResult, List<string> columnFilters)
        {
           return FilterResult(search, dtResult, columnFilters).Skip(start).Take(length).ToList();
        }

        public int Count(string search, List<Group> dtResult, List<string> columnFilters)
        {
            return FilterResult(search, dtResult, columnFilters).Count();
        }
        private IQueryable<Group> FilterResult(string search, List<Group> dtResult, List<string> columnFilters)
         {
            IQueryable<Group> results = dtResult.AsQueryable();

            results = results.Where
            (
              p => (search == null
                || p.id != 0 && p.id.ToString().ToLower().Contains(search.ToLower())
                || p.Name != null && p.Name.ToString().ToLower().Contains(search.ToLower())
                
                || p.IsInactive != false && p.IsInactive.ToString().ToLower().Contains(search.ToLower())

                || p.DeactivatedDate != null && p.DeactivatedDate == StringToDate(search)
                )

              && (columnFilters[0] == null || (p.id != 0 && p.id.ToString().ToLower().Contains(columnFilters[0].ToLower())))
              && (columnFilters[1] == null || (p.Name != null && p.Name.ToString().ToLower().Contains(columnFilters[1].ToLower())))
             
              && (columnFilters[2] == null || (p.IsInactive != false && p.IsInactive.ToString().ToLower().Contains(columnFilters[2].ToLower())))
              && (columnFilters[3] == null || (p.DeactiveDate_FORMAT != null && p.DeactiveDate_FORMAT.ToString().ToLower().Contains(columnFilters[3].ToLower())))

              


            );
            return results;
        }
    }
}
