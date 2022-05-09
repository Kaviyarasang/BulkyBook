﻿using Book.Dataaccess.Repository.IRepository;
using Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Dataaccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private ApplicationDbcontext _db;
        public ProductRepository(ApplicationDbcontext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product obj)
        {
           var objFromDb = _db.Products.FirstOrDefault(u=>u.Id==obj.Id);
            if (objFromDb != null)

            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price100 = obj.Price100;             
                objFromDb.CategoryID= obj.CategoryID;
                objFromDb.Author = obj.Author;
                objFromDb.CoverTypeID = obj.CoverTypeID;
                if (objFromDb.ImageUrl !=null)
                {
                    objFromDb.ImageUrl= obj.ImageUrl;
                }



            }

        }
    


    }
}
