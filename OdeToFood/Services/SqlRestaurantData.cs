using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Data;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    public class SqlRestaurantData : IResturantData
    {
        private OdeToFoodDbContext _context;

        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            _context = context;
        }
        public Restaurant Add(Restaurant res)
        {
            _context.Restaurant.Add(res);
            _context.SaveChanges();
            return res;
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurant.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            // ok for little DB no for big ones
            return _context.Restaurant.OrderBy(r => r.Name);
        }

        public Restaurant Update(Restaurant res)
        {
            _context.Attach(res).State =
                EntityState.Modified;
            _context.SaveChanges();

            return res;
        }
    }
}
