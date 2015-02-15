using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIdentity.ViewModels;
using MyIdentity;

namespace MyIdentity.Models
{
    public class User
    {
        MyIdentityEntities db = new MyIdentityEntities();

        public void Add(RegisteredUser newUser)
        {
            MyUser u = new MyUser();
            u.myUser1 = newUser.UserName;
            u.myEmail = newUser.Email;
            u.myAddress = newUser.Address;
            u.phone = newUser.Phone;
            u.city = newUser.City;
            u.province = newUser.Province;
            u.country = newUser.Country;

            db.MyUsers.Add(u);

            AspNetUser a = new AspNetUser();
            var aspUser = db.AspNetUsers.Where(t => t.UserName == newUser.UserName).FirstOrDefault();
            aspUser.PhoneNumber = newUser.Phone;
            
            db.SaveChanges();
        }
    }
}