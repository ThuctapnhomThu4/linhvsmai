﻿using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class AdminDAO
    {
        FashionShopDbContext db = null;
        public AdminDAO()
        {
            db = new FashionShopDbContext();
        }
        public long Insert(Admin entity)
        {
            db.Admin.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public Admin GetByID(string userName)
        {
            return db.Admin.SingleOrDefault(x => x.UserName == userName);
        }

        public IEnumerable<Admin> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Admin> model = db.Admin;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public int Login(string userName, string passWord)
        {
         //   string pashPassword = passWord.GetHashCode(passWord);
            var result = db.Admin.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if(result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else
                        return -2;
                }
            }
        }
    }
}
