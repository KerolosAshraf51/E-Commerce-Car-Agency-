using CarAgency.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace CarAgency.Repositories
{
    public class ReviewRepo : IReviewRepo
    {
        context context;


        public ReviewRepo(context _context)
        {

            this.context = _context;
        }


        public void Add(review Review)
        {
            context.Add(Review);

        }

        public void Delete(int id)
        {
            review Review = GetById(id);

            context.Remove(Review);
        }

        public List<review> GetAll()
        {
            return context.reviews.ToList();

        }

        public review GetById(int id)
        {
            return context.reviews.FirstOrDefault(a => a.Id == id);

        }

        public void Update(review Review)
        {
            context.reviews.Update(Review);
        }

        public void Save()
        {
            context.SaveChanges();
        }


        //List<review> IReviewRepo.GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        //review IReviewRepo.GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}


