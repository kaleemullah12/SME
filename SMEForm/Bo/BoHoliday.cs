using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMEForm.Context;
namespace SMEForm.Bo
{
    public class BoHoliday
    {
        public List<Holiday> GetEmployeeHolidays(int workID)
        {
            var holidays = (from h in DbContext.Current().Holidays
                            where h.WorkID == workID && h.IsActive
                            select h).ToList();
            return holidays;
        }
        public void DeleteHoliday(int holidayID)
        {
            var holiday = (from h in DbContext.Current().Holidays
                           where h.ID == holidayID
                           select h).FirstOrDefault();
            if (holiday != null)
            {
                holiday.IsActive = false;
                holiday.ModifiedBy = HttpContext.Current.User.Identity.Name;
                holiday.ModifiedTime = DateTime.Now;
                DbContext.Current().SaveChanges();
            }
        }
        public bool UpsertHoliday(Holiday holiday)
        {
            bool isNew = false;
            if (holiday.ID > 0)
            {
                var hol = (from h in DbContext.Current().Holidays
                           where h.ID == holiday.ID
                           select h).FirstOrDefault();
                hol.FromDate = holiday.FromDate;
                hol.ToDate = holiday.ToDate;
                hol.IsActive = true;
                hol.ModifiedBy = HttpContext.Current.User.Identity.Name;
                hol.ModifiedTime = DateTime.Now;                
            }
            else
            {
                isNew = true;
                holiday.CreatedBy = HttpContext.Current.User.Identity.Name;
                holiday.CreatedTime = DateTime.Now;
                holiday.IsActive = true;
                DbContext.Current().Holidays.AddObject(holiday);

            }
            DbContext.Current().SaveChanges();
            return isNew;
        }
    }
}