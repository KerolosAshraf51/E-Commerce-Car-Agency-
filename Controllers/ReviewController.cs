
//using CarAgency.Models;
//using CarAgency.Repositories;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ViewEngines;
using CarAgency.Models;
using CarAgency.Repositories;
using CarAgency.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarAgency.Controllers
{
    public class ReviewController : Controller
    {
        IReviewRepo _reviewRepo;

        public ReviewController(IReviewRepo reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        public IActionResult Index()
        {
            List<review> Reviews = _reviewRepo.GetAll();
            return View("Index", Reviews);
        }


        public IActionResult Add(review Review)
        {
            return View("Add", Review);
        }
        public IActionResult SaveAdd(review Review)
        {
            review newreview = new review();
            if (Review.description != null)
            {
                newreview.description = Review.description;
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                newreview.clientID = int.Parse(userId.Value);
                newreview.carID = Review.Id;
                newreview.Date = Review.Date == default(DateTime) ? DateTime.Now : Review.Date;
                _reviewRepo.Add(newreview);
                _reviewRepo.Save();
                return RedirectToAction("Index");
            }
            return View("Add", Review);
        }


        public IActionResult Details(int id)
        {
            review Review = _reviewRepo.GetById(id);

            return View("Details", Review);
        }

        public IActionResult Delete(int id)
        {
            var review = _reviewRepo.GetById(id);
            if (review == null)
            {
                return NotFound();
            }
            return View("Delete", review);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var review = _reviewRepo.GetById(id);
            if (review != null)
            {
                _reviewRepo.Delete(id);
                _reviewRepo.Save();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var review = _reviewRepo.GetById(id);
            return View("Update", review);
        }

        [HttpPost]
        public IActionResult Update(review updatedReview , int carID)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            updatedReview.clientID = int.Parse(userId.Value);
            updatedReview.carID = carID;
            _reviewRepo.Update(updatedReview);
          
            _reviewRepo.Save();
            return RedirectToAction("Index");
        }

    }


}

