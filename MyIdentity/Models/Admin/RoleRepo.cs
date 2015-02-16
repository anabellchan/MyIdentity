using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIdentity.ViewModels;

namespace MyIdentity.Models.Admin
{
    public class RoleRepo
    {
        MyIdentityEntities db = new MyIdentityEntities();
        public void DeleteUser(UserRole userToDelete)
        {
            //var user = db.AspNetUsers.Where(u => u.UserName == userToDelete.UserName).FirstOrDefault();
            
            AspNetRole role = (from r in db.AspNetRoles
                                   where userToDelete.RoleName == r.Name
                                   select r).FirstOrDefault();
            AspNetUser user = (from u in db.AspNetUsers
                                   where userToDelete.UserName == u.UserName
                                   select u).FirstOrDefault();

            if (role.AspNetUsers.Contains(user)) {
                role.AspNetUsers.Remove(user);
                db.SaveChanges();
            }

        
        }

        public IEnumerable<UserRole> ListUserRole()
        {
            List<UserRole> query = (from r in db.AspNetRoles
                                    from u in r.AspNetUsers
                                    select new UserRole
                                    {
                                        UserName = u.UserName,
                                        RoleName = r.Name
                                    }).ToList();
            return query;


        }
    }
}