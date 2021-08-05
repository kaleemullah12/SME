using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMEForm.Context;
namespace SMEForm.Bo
{
    public class BoTimesheetImport
    {
        public TimeSheetsImport TimeSheet
        {
            get;
            set;
        }
        public Employee Employee
        {
            get;
            set;
        }
        public List<WorkTime> ClockOverlaps
        {
            get;
            set;
        }
        public int SucceedCount { get; set; }
        public int FailCount { get; set; }
        public string Error
        {
            get
            {
                return GetError(TimeSheet.ValidationBitString);
            }
        }
        public BoTimesheetImport()
        {
        }
        public BoTimesheetImport(TimeSheetsImport timeSheet)
        {
            TimeSheet = timeSheet;
            TimeSheet.ValidationBitString = 0;
            Validate();
        }
        public BoTimesheetImport(string workid, string date, string clockin, string clockout, string workhour, string filename)
        {
            TimeSheet = new TimeSheetsImport
            {
                WorkID = workid,
                Date = date,
                ClockIn = clockin,
                ClockOut = clockout,
                ImportedBy = HttpContext.Current.User.Identity.Name,
                ImportedTime = DateTime.Now,
                WorkHour = workhour,
                ValidationBitString = 0,
                FileName = filename
            };
            Validate();
        }
        public void Validate()
        {
            int workID = 0;
            if (!int.TryParse(TimeSheet.WorkID, out workID))
            {
                TimeSheet.ValidationBitString = TimeSheet.ValidationBitString | 1;
                Save();
                return;
            }
            Employee = DbContext.Current().Employees.FirstOrDefault(e => e.WorkID == workID);
            if (Employee == null)
            {
                TimeSheet.ValidationBitString = TimeSheet.ValidationBitString | 1;
                Save();
                return;
            }
            ValidateWorkTime();
        }
        public void ValidateWorkTime()
        {
            int workID = Employee.WorkID;
            int weekType = 7;
            if (Employee.Shop.CompanyID == 18 || Employee.Shop.CompanyID == 40)
                weekType = 4;
            DateTime date;
            TimeSpan clockin, clockout;
            decimal workhour;
            if (DateTime.TryParse(TimeSheet.Date, out date) && TimeSpan.TryParse(TimeSheet.ClockIn, out clockin)
                && TimeSpan.TryParse(TimeSheet.ClockOut, out clockout) && decimal.TryParse(TimeSheet.WorkHour, out workhour))
            {
                DateTime startTime = new DateTime(date.Ticks);
                DateTime endTime = new DateTime(date.Ticks);
                startTime = startTime.AddTicks(clockin.Ticks);
                if (clockout < clockin || clockout == TimeSpan.FromTicks(0))
                    endTime = endTime.AddDays(1);
                endTime = endTime.AddTicks(clockout.Ticks);

                workhour = (decimal)endTime.Subtract(startTime).TotalHours;
                ClockOverlaps = (from c in DbContext.Current().WorkTimes
                                 where c.WorkID == workID && c.Date == date
                                 where (c.ClockIn >= startTime && c.ClockIn < endTime) || (c.ClockOut <= endTime && c.ClockOut > startTime) || (c.ClockIn <= startTime && c.ClockOut >= endTime)
                                 select c).ToList();
                if (ClockOverlaps.Count > 0)
                {
                    TimeSheet.ValidationBitString = TimeSheet.ValidationBitString | 2;
                    Save();
                }
                else
                {
                    var holidayOverlaps = (from h in DbContext.Current().Holidays
                                           where h.WorkID == workID
                                           where (h.FromDate <= date && h.ToDate >= date)
                                           select h).ToList();
                    if (holidayOverlaps.Count > 0)
                    {
                        TimeSheet.ValidationBitString = TimeSheet.ValidationBitString | 4;
                        Save();
                    }
                    else
                    {
                        WorkWeek ww = DbContext.Current().WorkWeeks.First((w) => w.StartDate <= date && w.EndDate >= date && w.Type == weekType);
                        DbContext.Current().WorkTimes.AddObject(new WorkTime
                        {
                            WorkID = Employee.WorkID,
                            ShopID = Employee.ShopID,
                            Date = date,
                            ClockIn = startTime,
                            ClockOut = endTime,
                            CreatedBy = HttpContext.Current.User.Identity.Name,
                            CreatedTime = DateTime.Now,
                            WeekID = ww.ID,
                            WorkHour = workhour
                        });
                        if (TimeSheet.ID > 0)
                            Delete(TimeSheet.ID);
                    }
                }
            }
        }
        public void Save()
        {
            if (TimeSheet.ID > 0)
            {
                var timeSheetImport = DbContext.Current().TimeSheetsImports.Single(i => i.ID == TimeSheet.ID);
                timeSheetImport.WorkID = TimeSheet.WorkID;
                timeSheetImport.Date = TimeSheet.Date;
                timeSheetImport.ClockIn = TimeSheet.ClockIn;
                timeSheetImport.ClockOut = TimeSheet.ClockOut;
                timeSheetImport.WorkHour = TimeSheet.WorkHour;
                timeSheetImport.ValidationBitString = TimeSheet.ValidationBitString;
                DbContext.Current().SaveChanges();
            }         
        }
        public static void Delete(int ID)
        {
            if (ID > 0)
            {
                var timesheet = DbContext.Current().TimeSheetsImports.First(i => i.ID == ID);
                DbContext.Current().TimeSheetsImports.DeleteObject(timesheet);
                DbContext.Current().SaveChanges();
            }
        }
        public static string GetError(int? bitstring)
        {
            string _error = string.Empty;
            if ((bitstring & 1) == 1)
                _error += "Employee WorkID not in database!";
            if ((bitstring & 2) == 2)
                _error += "Worktime overlaps for this employee!";
            if ((bitstring & 4) == 4)
                _error += "This employee is on holiday!";
            if ((bitstring & 8) == 8)
                _error += "One or more field is invalid!";
            return _error;
        }
    }
}