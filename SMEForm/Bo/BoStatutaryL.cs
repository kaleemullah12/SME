using SMEForm.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMEForm.Bo
{
    public class BoStatutaryL
    {
        public List<StatutaryLeaf> GetEmployeeStatList(int workID)
        {
            var Statutary = (from h in DbContext.Current().StatutaryLeaves
                            where h.WorkID == workID && h.IsActive==true
                            select h).ToList();
            return Statutary;
        }
        public void DeleteStatutary(int holidayID)
        {
            var holiday = (from h in DbContext.Current().StatutaryLeaves
                           where h.ID == holidayID
                           select h).FirstOrDefault();
            if (holiday != null)
            {
                holiday.IsActive = false;
               
                DbContext.Current().SaveChanges();
            }
        }
        public bool UpsertStatutary(StatutaryLeaf Statutary)
        {
            bool isNew = false;
            if (Statutary.ID > 0)
            {
                var hol = (from h in DbContext.Current().StatutaryLeaves
                           where h.ID == Statutary.ID
                           select h).FirstOrDefault();
                hol.FromDate =Convert.ToDateTime(Statutary.FromDate);
                hol.ToDate =Convert.ToDateTime(Statutary.ToDate);
                hol.IsActive = true;
                //hol.ModifiedBy = HttpContext.Current.User.Identity.Name;
                //hol.ModifiedTime = DateTime.Now;
            }
            else
            {
                isNew = true;
                Statutary.CreatedBy = HttpContext.Current.User.Identity.Name;
                Statutary.CreatedTime = DateTime.Now;
                Statutary.IsActive = true;
                DbContext.Current().StatutaryLeaves.AddObject(Statutary);

            }
            DbContext.Current().SaveChanges();
            return isNew;
        }
    }
}