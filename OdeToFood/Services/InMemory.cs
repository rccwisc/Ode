using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Services
{
    public class InMemory : IResturantData
    {
        public InMemory()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant{Id=1, Name="Some place" } ,
                new Restaurant{Id=2, Name="Some place One" } ,
                new Restaurant{Id=3, Name="Some place two" } ,
                new Restaurant{Id=4, Name="Some place thrree" } ,
                new Restaurant{Id=5, Name="Some place four" } ,
            };

        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants.OrderBy(r => r.Name);
        }

        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant res)
        {
            res.Id = _restaurants.Max(r => r.Id) + 1;
            _restaurants.Add(res);
            return res;
        }

        public Restaurant Update(Restaurant res)
        {
            _restaurants.Remove(_restaurants.First(x => x.Id == res.Id));
            _restaurants.Add(res);

            return res;
        }

        List<Restaurant> _restaurants;
    }
}
