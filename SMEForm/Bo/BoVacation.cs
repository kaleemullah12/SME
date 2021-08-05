using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMEForm.Context;

namespace SMEForm.Bo
{
    public class BoVacation
    {
        public List<Vacation> GetEmployeeVacations(int workID)
        {
            var vacations = (from v in DbContext.Current().Vacations
                            where v.WorkID == workID && v.IsActive
                            select v).ToList();
            return vacations;
        }
        public void DeleteVacation(int vacationID)
        {
            var vacation = (from v in DbContext.Current().Vacations
                            where v.ID == vacationID
                           select v).FirstOrDefault();
            if (vacation != null)
            {
                vacation.IsActive = false;
                vacation.ModifiedBy = HttpContext.Current.User.Identity.Name;
                vacation.ModifiedTime = DateTime.Now;
                DbContext.Current().SaveChanges();
            }
        }
        public bool UpsertVacation(Vacation vacation)
        {
            bool isNew = false;
            if (vacation.ID > 0)
            {
                var va = (from v in DbContext.Current().Vacations
                           where v.ID == vacation.ID
                           select v).FirstOrDefault();
                va.FromDate = vacation.FromDate;
                va.ToDate = vacation.ToDate;
                va.IsActive = true;
                va.ModifiedBy = HttpContext.Current.User.Identity.Name;
                va.ModifiedTime = DateTime.Now;
            }
            else
            {
                isNew = true;
                vacation.CreatedBy = HttpContext.Current.User.Identity.Name;
                vacation.CreatedTime = DateTime.Now;
                vacation.IsActive = true;
                DbContext.Current().Vacations.AddObject(vacation);
            }
            DbContext.Current().SaveChanges();
            return isNew;
        }
    }
}