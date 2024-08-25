using CarAgency.Controllers;
using CarAgency.Models;
using Microsoft.EntityFrameworkCore;

namespace CarAgency.Repositories
{
    public class PurchaseRepo : IPurchaseRepo
    {
        private readonly context _context;

        public PurchaseRepo(context context)
        {
            _context = context;

        }

        public void Create(purchase purchase)
        {
            _context.purchases.Add(purchase);

        }

        public void Delete(int id)
        {
            purchase purchase = GetById(id);
            if (purchase != null)
            {
                _context.purchases.Remove(purchase);
            }
        }

        public List<purchase> GetAll()
        {
            return _context.purchases
                   .Include(p => p.Client)
                   .Include(p => p.Car)
                   .ToList();
        }

        public List<purchase> GetByClientId(int id)
        {

            List<purchase> purchases = _context.purchases.Where(p => p.ClientID == id)
                .Include(p => p.Client)
                   .Include(p => p.Car)
                   .ToList();
            return purchases;
        }

        public purchase GetById(int id)
        {
            return _context.purchases
                           .Include(p => p.Client)
                           .Include(p => p.Car)
                           .FirstOrDefault(p => p.ID == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(purchase purchase)
        {
            _context.Update(purchase);
        }

        public List<purchase> GetByCarId(int id)
        {
            List<purchase> purchases = _context.purchases.Where(p => p.CarID == id)
                .Include(p => p.Client)
                   .Include(p => p.Car)
                   .ToList();
            return purchases;
        }

        public void DeletebyCarId(int carId)
        {
            List<purchase> temp = _context.purchases
                                    .Where(p => p.CarID == carId)
                                    .ToList();

            foreach (purchase purchase in temp)
            {
                _context.Remove(purchase);
            }
        }


    }
}
