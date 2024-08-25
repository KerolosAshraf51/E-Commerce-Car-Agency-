using CarAgency.Models;

namespace CarAgency.Repositories
{
    public interface IReviewRepo
    {

        public void Add(review review);
        public void Update(review review);
        public void Delete(int id);
        public List<review> GetAll();
        public review GetById(int id);
        public void Save();
    }

}
