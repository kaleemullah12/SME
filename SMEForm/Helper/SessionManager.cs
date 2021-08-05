using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMEForm.Helper
{
    public enum SessionKey
    {
        SessionKFCImport,
        SessionEmployeeImport,
        SessionUser,
        SessionCurrentWeeks,
        SessionCurrentCostaWeeks,
        SessionCurrentHolidays
    }
    public class SessionManager
    {
        public static bool CompareSession<T>(SessionKey key, T value)
        {
            return CompareSession(key.ToString(), value);
        }
        public static bool CompareSession<T>(string key, T value)
        {
            if (CheckSession(key))
                if (GetSession<T>(key).Equals(value))
                    return true;
            return false;
        }
        public static bool CheckSession(SessionKey key)
        {
            return CheckSession(key.ToString());
        }
        public static bool CheckSession(string key)
        {
            return HttpContext.Current.Session[key] != null;
        }
        public static T GetSession<T>(SessionKey key)
        {
            return GetSession<T>(key.ToString());
        }
        public static T GetSession<T>(string key)
        {
            if (HttpContext.Current.Session[key] == null)
                return default(T);
            return (T)HttpContext.Current.Session[key];
        }
        public static void SetSession<T>(SessionKey key, T value)
        {
            SetSession<T>(key.ToString(), value);
        }
        public static void SetSession<T>(string key, T value)
        {
            HttpContext.Current.Session[key] = value;
        }
        public static void RemoveSession()
        {
            HttpContext.Current.Session.Clear();
        }
        public static void RemoveSession(SessionKey key)
        {
            HttpContext.Current.Session.Remove(key.ToString());
        }
    }
}