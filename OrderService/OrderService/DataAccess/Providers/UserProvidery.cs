using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderService.DataAccess.Providers
{
    public interface IUserProvider
    {
        ApplicationUser GetById(string id);
        List<ApplicationUser> GetAll();
    }

    public class UserProvider : IUserProvider
    {
        public List<ApplicationUser> GetAll()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Users
                    .ToList();
            }
        }

        public ApplicationUser GetById(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Users
                    .Where(u => u.Id == id)
                    .FirstOrDefault();
            }
        }
    }
}