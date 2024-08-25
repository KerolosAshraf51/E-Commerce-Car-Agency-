using CarAgency.Models;
using CarAgency.Repositories;
using CarAgency.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarAgency.Controllers
{
    public class applicationUserController : Controller
    {

        IApplicationUserRepo ApplicationUserRepo;
        ICarRepo CarRepo;
        IPurchaseRepo PurchaseRepo;
        public applicationUserController(IApplicationUserRepo applicationUserRepo, ICarRepo carRepo, IPurchaseRepo purchaseRepo)
        {
            ApplicationUserRepo = applicationUserRepo;
            CarRepo = carRepo;
            PurchaseRepo = purchaseRepo;
        }
        // Client Section

        public IActionResult confirmBuy()
        {
            return View("ConfirmBuy");
        }
        public IActionResult BuyCar(int carId)
        {
            var car = CarRepo.GetById(carId);

            if ( car != null && User.Identity.IsAuthenticated)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var user = ApplicationUserRepo.GetById(int.Parse(userId.Value));

                var purchase = new purchase();
                purchase.CarID = carId;
                purchase.ClientID = int.Parse(userId.Value);
                purchase.Date = DateTime.Now;
                purchase.price = car.Price;

                PurchaseRepo.Create(purchase);
                PurchaseRepo.Save();

                return RedirectToAction("PurchaseSuccess");
            }

            return View("PurchaseFailed");
        }

        public IActionResult PurchaseSuccess()
        {
            return View();
        }

        // GET: ApplicationUser/PurchaseFailed
        public IActionResult PurchaseFailed()
        {
            return View();
        }


        public ActionResult ViewProfile()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier); // Get the current user's ID
            var user = ApplicationUserRepo.GetById(int.Parse(userId.Value));

            if (user == null)
            {
                return NotFound();
            }

            ProfileViewModel viewProfileViewModel = new ProfileViewModel();
            viewProfileViewModel.Id= user.Id;
            viewProfileViewModel.Name = user.UserName;
            viewProfileViewModel.Email = user.Email;
            viewProfileViewModel.PhoneNumber = user.PhoneNumber;
            
            return View("Profile", viewProfileViewModel);
        }


        public IActionResult EditProfile()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

          
            var user = ApplicationUserRepo.GetById(int.Parse(userId.Value));
            ProfileViewModel editProfileViewModel = new ProfileViewModel();

            editProfileViewModel.Name = user.UserName;
            editProfileViewModel.Email = user.Email;
            editProfileViewModel.PhoneNumber = user.PhoneNumber;

            return View("EditProfile", editProfileViewModel);
        }

        public IActionResult SaveEdit(ProfileViewModel EditedVM)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);


            var user = ApplicationUserRepo.GetById(int.Parse(userId.Value));

            user.UserName= EditedVM.Name ;
            user.Email = EditedVM.Email;
            user.PhoneNumber = EditedVM.PhoneNumber;
            ApplicationUserRepo.Update(user);
            ApplicationUserRepo.Save();
            return RedirectToAction("Index","Car");
        }


        public IActionResult SearchUser()
        {
            return View("SearchUser");
        }


        [HttpPost]
        public IActionResult SearchUser(int UserId)
        {
            applicationUser user = ApplicationUserRepo.GetById(UserId);
            

            return View("SearchUserResults", user);

           
        }
    }
}
