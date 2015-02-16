using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIdentity.ViewModels;
using MyIdentity;

namespace MyIdentity.Models
{
    public class UserRepo
    {
        MyIdentityEntities db = new MyIdentityEntities();

        public void Add(RegisteredUser newUser)
        {

            AspNetUser a = new AspNetUser();
            var aspUser = db.AspNetUsers.Where(t => t.UserName == newUser.UserName).FirstOrDefault();
            aspUser.PhoneNumber = newUser.Phone;            

            MyUser u = new MyUser();
            u.ID = aspUser.Id;
            u.myUser1 = newUser.UserName.ToLower();
            u.myEmail = newUser.Email;
            u.myAddress = newUser.Address;
            u.phone = newUser.Phone;
            u.city = newUser.City;
            u.province = newUser.Province;
            u.country = newUser.Country;
            db.MyUsers.Add(u);

            db.SaveChanges();
        }

        public IEnumerable<UserDetail> AllUsers() 
        {

            var users = db.AspNetUsers.ToList();
            List<UserDetail> allUsers = new List<UserDetail>();

            foreach(AspNetUser user in users) {
                var role = (
                           from u in db.AspNetUsers
                           from r in u.AspNetRoles
                           where u.Id == user.Id
                           select r).FirstOrDefault();
                
                
                
                var userRec = db.MyUsers.Find(user.Id);

                UserDetail ud = new UserDetail();
                ud.RoleName = role.Name;
                ud.UserName = userRec.myUser1;
                ud.Email = userRec.myEmail;
                ud.Address = userRec.myAddress;
                ud.City = userRec.city;
                ud.Province = userRec.province;
                ud.Country = userRec.country;
                allUsers.Add(ud);

            }

            return allUsers;
        
        }
    }
}