
using CarAgency.Models;

namespace CarAgency.Repositories
{
    public interface ICarRepo
    {
        public void Add(cars obj);

        public void Update(cars obj);


        public void Delete(int id);

        public List<cars> GetAll();

        public cars GetById(int id);

        public void Save();

        //new pk
        public cars GetByName(string name, int engineC);
        public List<cars> getCarsByName(string name);
        public List<cars> getAllWithSearch(string name);

    }
}
