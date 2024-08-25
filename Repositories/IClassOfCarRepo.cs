using CarAgency.Models;

namespace CarAgency.Repositories
{
    public interface IClassOfCarRepo
    {
        public List<classOfCar> GetAll(); 
    }
}
