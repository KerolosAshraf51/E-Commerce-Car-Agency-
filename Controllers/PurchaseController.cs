using CarAgency.Models;
using CarAgency.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarAgency.Controllers
{
    [Authorize(Roles = "admin")]
    public class PurchaseController : Controller
    {
        IPurchaseRepo _purchaseRepo;
        ICarRepo _carRepo;
        public PurchaseController(IPurchaseRepo purchaseRepo,
                                  ICarRepo carRepo)
        {
            _purchaseRepo = purchaseRepo;
            _carRepo = carRepo;
        }
        public IActionResult Index()
        {

            var purchases = _purchaseRepo.GetAll();
            if (purchases == null)
            {
                purchases = new List<purchase>();
            }
            return View(purchases);
        }
        public IActionResult Details(int id)
        {
            var purchase = _purchaseRepo.GetById(id);
            if (purchase == null)
            {
                return NotFound();
            }
            return View(purchase);


        }

        public IActionResult OpenClientID()
        {
            return View("ClientPurchase");
        }

        public IActionResult ClientPurchase(int Id)
        {
            List<purchase> purchases = _purchaseRepo.GetByClientId(Id);
            return View("Client", purchases);
        }

        public IActionResult OpenCarID()
        {
            return View("View");
        }

        public IActionResult CarPurchase(int Id)
        {
            List<purchase> purchases = _purchaseRepo.GetByCarId(Id);
            return View("Client", purchases);
        }
    }
}
