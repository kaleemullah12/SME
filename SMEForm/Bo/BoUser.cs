using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMEForm.Context;
using SMEForm.Helper;

namespace SMEForm.Bo
{
    public class BoUser
    {
        public static bool IsAdmin()
        {
            BoUser user = new BoUser();
            SMEForm.Context.User u = user.Get(HttpContext.Current.User.Identity.Name);
            if (u != null && u.RoleID == 1)
                return true;
            else
                return false;
        }
        public User Get(string username)
        {
            var user = DbContext.Current().Users.FirstOrDefault(u => u.Username == username && u.IsActive);
            return user;
        }
        public User Get(string username, string password)
        {
            var user = DbContext.Current().Users.FirstOrDefault(u => u.Username == username && u.Password == password && u.IsActive);
            return user;
        }
        public bool ChangePassword(string username, string oldpassword, string newpassword)
        {
            var user = DbContext.Current().Users.FirstOrDefault(u => u.Username == username && u.Password == oldpassword);
            if (user == null)
                return false;
            else
            {
                user.Password = newpassword;
                DbContext.Current().SaveChanges();
                return true;
            }
        }
        public static List<User> GetAllUsers()
        {
            return DbContext.Current().Users.ToList();
        }
        public static bool UpSertUser(User user)
        {
            bool isNew = false;
            var u = (from usr in DbContext.Current().Users
                     where usr.ID == user.ID
                     select usr).FirstOrDefault();
            if (u != null)
            {
                u.Password = user.Password;
                u.Email = user.Email;
                u.RoleID = user.RoleID;
                u.ModifiedBy = HttpContext.Current.User.Identity.Name;
                u.ModifiedTime = DateTime.Now;
            }
            else
            {
                user.CreatedBy = HttpContext.Current.User.Identity.Name;
                user.CreatedTime = DateTime.Now;
                user.IsActive = true;
                isNew = true;
                DbContext.Current().Users.AddObject(user);
            }

            DbContext.Current().SaveChanges();
            return isNew;
        }
        public static void DeleteUser(int userID)
        {
            var u = (from usr in DbContext.Current().Users
                     where usr.ID == userID
                     select usr).FirstOrDefault();
            if (u != null)
            {
                u.IsActive = false;
                u.ModifiedBy = HttpContext.Current.User.Identity.Name;
                u.ModifiedTime = DateTime.Now;
                DbContext.Current().SaveChanges();
            }
        }
    }
}