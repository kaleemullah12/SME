using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMEForm.Context;
namespace SMEForm.Bo
{
    public class BoEmployee
    {
        public static KeyValuePair<int, int> GetNewWorkIDSageID(int companyID, int shopID)
        {
            int maxSageID = (from emp in DbContext.Current().Employees
                             where emp.Shop.CompanyID == companyID
                             select emp).Max(e => e.SageID);
            maxSageID++;
            return new KeyValuePair<int, int>(companyID * 1000000 + shopID * 10000 + maxSageID, maxSageID);
        }
        public static List<Employee> GetAllEmployees()
        {
            var employees = (from e in DbContext.Current().Employees
                             where e.IsActive == true
                             select e).ToList();
            return employees;
        }
        public static List<Employee> GetEmployeesbyComapny(int companyID, int shopID)
        {
            //var employees = (from e in DbContext.Current().Employees
            //                 where (e.IsActive == true && e.Shop.CompanyID == companyID && (shopID < 0 || e.ShopID == shopID))
            //                 select e).ToList();

            var employees = (from e in DbContext.Current().Employees
                             where (e.Shop.CompanyID == companyID && (shopID < 0 || e.ShopID == shopID))
                             select e).ToList();

            return employees;
        }
        public static void DeleteEmployee(int workID)
        {
            var employee = (from e in DbContext.Current().Employees
                            where e.WorkID == workID
                            select e).SingleOrDefault();
            if (employee != null)
            {
                employee.IsActive = false;
                employee.ModifiedBy = HttpContext.Current.User.Identity.Name;
                employee.ModifiedTime = DateTime.Now;
                DbContext.Current().SaveChanges();
            }
        }
        public static bool UpSertEmployee(Employee emp)
        {
            bool isNew = false;
            var employee = (from e in DbContext.Current().Employees
                            where e.WorkID == emp.WorkID
                            select e).SingleOrDefault();
            if (employee != null)
            {
                employee.SageID = emp.SageID;
                employee.StartDate = emp.StartDate;
                employee.EndDate = emp.EndDate;
                employee.DOB = emp.DOB;
                employee.Disabled = emp.Disabled;
                employee.Initial = emp.Initial;
                employee.Forename = emp.Forename;
                employee.Surname = emp.Surname;
                employee.Address1 = emp.Address1;
                employee.Address2 = emp.Address2;
                employee.Address3 = emp.Address3;
                employee.Postcode = emp.Postcode;
                employee.Tel = emp.Tel;
                employee.Mobile = emp.Mobile;
                employee.Email = emp.Email;
                employee.Gender = emp.Gender;
                employee.Nationality = emp.Nationality;
                employee.Ethnic = emp.Ethnic;
                employee.TaxCode = emp.TaxCode;
                employee.VisaExpireDate = emp.VisaExpireDate;
                employee.VisaApplyDate = emp.VisaApplyDate;
                employee.NICat = emp.NICat;
                employee.NI = emp.NI;
                employee.JobDescriptionID = emp.JobDescriptionID;
                employee.ShopID = emp.ShopID;
                //employee.IsActive = true;
                employee.IsActive = emp.IsActive;

                if ((bool)emp.IsActive == true)
                {
                    employee.EmployeeActivation_Date = DateTime.Now;
                }
                else
                {
                    employee.EmployeeInActivation_Date = DateTime.Now;
                }

                employee.PayTypeID = emp.PayTypeID;
                employee.Note = emp.Note;
                employee.ModifiedBy = HttpContext.Current.User.Identity.Name;
                employee.ModifiedTime = DateTime.Now;
            }
            else
            {
                isNew = true;
                emp.CreatedBy = HttpContext.Current.User.Identity.Name;
                emp.CreatedTime = DateTime.Now;
                emp.IsActive = true;
                DbContext.Current().Employees.AddObject(emp);
            }
            return isNew;
        }
    }
}