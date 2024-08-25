using CarAgency.Models;

namespace CarAgency.Repositories
{
    public class ClassOfCarRepo : IClassOfCarRepo
    {
        private readonly context context;

        public ClassOfCarRepo(context _context)
        {
            context = _context;
        }
        public List<classOfCar> GetAll()
        {
            return context.classOfCars.ToList();
        }
    }
}
