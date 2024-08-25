
using CarAgency.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarAgency.Repositories
{
    
    public class CarRpo :ICarRepo    
    {
        context context;
        public CarRpo(context _con)
        {
            context = _con;   
        }
        public void Add(cars obj) 
        {
            context.Add(obj);
        }

        public void Update(cars obj)
        {
            context.Update(obj);
        }


        public void Delete(int id)
        {
            cars car = GetById(id);
            context.cars.Remove(car);
        }

        public List<cars> GetAll()
        {
            return context.cars
                            //.Where(!=)
                          .Include(c=>c._class)
                          .Include(c=>c.reviews)
                          .ToList();
        }

        public cars GetById(int id)
        {
           return  context.cars
                           .Include(c => c._class)
                           .Include(c => c.reviews)
                           .Include(c=>c.seller)
                           .FirstOrDefault(c=>c.ID == id);
   
        }

        public void Save()
        {
            context.SaveChanges();
        }

        //new pk
        public cars GetByName(string name,int engineC)
        {
            return context.cars.FirstOrDefault(c => c.Model == name && c.EngineCapacity == engineC);
        }

        public List<cars> getCarsByName(string name) 
        {
            return context.cars
                .Where(c => c.Name.StartsWith(name))
                .Include(c => c._class)
                .Include(c => c.reviews)
                .ToList();
        }

        public List<cars> getAllWithSearch(string name)
        {
            return context.cars.Where(c => c.Name.Contains(name))
                .Include(c => c._class)
                .Include(c => c.reviews)
                .ToList();
        }
    }
}
