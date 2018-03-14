﻿using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OdeToFood.Services
{
    public interface IResturantData
    {
        IEnumerable<Restaurant> GetAll();

        Restaurant Get(int id);

        Restaurant Add(Restaurant res);

        Restaurant Update(Restaurant res);

    }    
}
