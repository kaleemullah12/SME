using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SMEForm.Bo
{
    public class BoData
    {
        protected SqlConnection _connection;
        protected SqlCommand _command;
        public BoData()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString);
        }
        protected void CreateCommond(string storeProcedureName)
        {
            _command = new SqlCommand
            {
                Connection = _connection,
                CommandText = storeProcedureName,
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 300
            };
        }
        protected void AddParameter(string parameterName, SqlDbType type, object value)
        {
            AddParameter(parameterName, type, value, ParameterDirection.Input);
        }

        protected void AddParameter(string parameterName, SqlDbType type, object value, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, value);
            param.SqlDbType = type;
            param.Direction = direction;
            _command.Parameters.Add(param);
        }

        protected void AddParameter(string parameterName, SqlDbType type, object value, ParameterDirection direction, int size)
        {
            SqlParameter param = new SqlParameter(parameterName, value);
            param.SqlDbType = type;
            param.Direction = direction;
            param.Size = size;
            _command.Parameters.Add(param);
        }
        protected void AddParameter(SqlParameter param)
        {
            _command.Parameters.Add(param);
        }
        protected void ExecuteNoReturnQuery()
        {
            _connection.Open();
            try
            {
                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _connection.Close();
            }
        }

        protected DataTable ExecuteTableReturnQuery()
        {
            SqlDataAdapter da = new SqlDataAdapter(_command);
            DataTable _dt = new DataTable();
            da.Fill(_dt);
            return _dt;
        }
    }
    public class BoSagePayroll : BoData
    {
        public DataTable GetByCompanyByWeek(int weekID, int companyID)
        {
            CreateCommond("HR.ExportPayrollByWeekByCompany");
            AddParameter("@WeekID", SqlDbType.Int, weekID);
            AddParameter("@CompanyID", SqlDbType.Int, companyID);
            return ExecuteTableReturnQuery();
        }
        public DataTable GetByCompanyByInterval(int startWeekID, int endWeekID, int companyID)
        {
            CreateCommond("HR.ExportPayrollByIntervalByCompany");
            AddParameter("@FromWeekID", SqlDbType.Int, startWeekID);
            AddParameter("@ToWeekID", SqlDbType.Int, endWeekID);
            AddParameter("@CompanyID", SqlDbType.Int, companyID);
            return ExecuteTableReturnQuery();
        } 
    }
    public class BoTimeSheet : BoData
    {
        public void BulkCopyResultsTimeSheetImport(DataTable dt)
        {
            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(_connection))
            {
                try
                {
                    _connection.Open();
                    bulkcopy.DestinationTableName = "HR.TimeSheetsImport";
                    bulkcopy.WriteToServer(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _connection.Close();
                }
            }
        }
        public void ValidateImport()
        {
            CreateCommond("HR.TimeSheetImportValidation");
            ExecuteNoReturnQuery();
        }
    }
    public class BoTimeSheet_Conflict : BoData
    {
        public void BulkCopyResultsTimeSheetImport(DataTable dt)
        {
            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(_connection))
            {
                try
                {
                    _connection.Open();
                    bulkcopy.DestinationTableName = "HR.TimeSheetsImport_conflicts";
                    bulkcopy.WriteToServer(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _connection.Close();
                }
            }
        }
        public void ValidateImport()
        {
            CreateCommond("HR.TimeSheetImportValidation");
            ExecuteNoReturnQuery();
        }
    }
    public class BoHolidays : BoData
    {
        public void BulkCopyResultsHolidaysImport(DataTable dt)
        {
            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(_connection))
            {
                try
                {
                    _connection.Open();
                    bulkcopy.DestinationTableName = "HR.Holiday";
                    bulkcopy.WriteToServer(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _connection.Close();
                }
            }
        }
        public void ValidateImport()
        {
            //CreateCommond("HR.HolidayValidation");
            //ExecuteNoReturnQuery();
        }
    }

    public class BoStatutary : BoData
    {
        public void BulkCopyResultsStatutaryImport(DataTable dt)
        {
            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(_connection))
            {
                try
                {
                    _connection.Open();
                    bulkcopy.DestinationTableName = "StatutaryLeaves";
                    bulkcopy.WriteToServer(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _connection.Close();
                }
            }
        }
        public void ValidateImport()
        {
            //CreateCommond("HR.HolidayValidation");
            //ExecuteNoReturnQuery();
        }
    }

    public class BoWorkTime : BoData
    {
        public void ClearByWeekByCompany(int weekID, int companyID)
        {
            CreateCommond("HR.ClearTimeSheetByWeekByCompany");
            AddParameter("@WeekID", SqlDbType.Int, weekID);
            AddParameter("@CompanyID", SqlDbType.Int, companyID);
            AddParameter("@ClearBy", SqlDbType.VarChar, HttpContext.Current.User.Identity.Name);
            ExecuteNoReturnQuery();
        }
    }
}