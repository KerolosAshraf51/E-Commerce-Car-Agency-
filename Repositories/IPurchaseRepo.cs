using Microsoft.AspNetCore.Mvc;
using CarAgency.Models;
using CarAgency.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace CarAgency.Controllers

{
    public interface IPurchaseRepo
    {
        public List<purchase> GetAll();
        public purchase GetById(int id);
        public void Create(purchase purchase);
        public void Update(purchase purchase);
        public void Delete(int id);
        public void Save();
        public List<purchase> GetByClientId(int id);
        public List<purchase> GetByCarId(int id);

        public void DeletebyCarId(int carId);


    }
}
