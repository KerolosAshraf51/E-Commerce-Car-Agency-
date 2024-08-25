using CarAgency.Models;
using CarAgency.Repositories;
using CarAgency.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarAgency.Controllers
{
    public class CarController : Controller
    {
        ICarRepo carRepo;
        IPurchaseRepo purchaseRepo;
        private readonly IClassOfCarRepo classOfCar;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CarController(IPurchaseRepo pur,ICarRepo car,IClassOfCarRepo classOfCar,IWebHostEnvironment webHostEnvironment)
        {
            carRepo = car;
            this.classOfCar = classOfCar;
            this.webHostEnvironment = webHostEnvironment;
            purchaseRepo = pur;
        }
        public IActionResult Index(string Search="", int PageNo =1)
        {
            //List<cars> cars = carRepo.GetAll();
            List<cars> cars = carRepo.getAllWithSearch(Search);

            

            //pagination
            int NoOfRecordsPerPage = 3;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(cars.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
            int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;

            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;

            cars = cars.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();


            return View("allCars",cars);
        }
        [Authorize(Roles ="admin")]
        public IActionResult add()
        {
            List<classOfCar> carClasses = classOfCar.GetAll();

            List<string> transimations = ["Manual", "Automatic", "Semi-Automatic"];
            List<int> cc = [1,2,3];
            List<string> Brands = ["BMW", "Audi", "Chevrolet", "Hyundai", "Jeep", "KIA","MG","Fiat"];

            ViewData["classOfCar"] = carClasses;
            ViewData["transimations"] = transimations;
            ViewData["cc"] = cc;
            ViewData["Brands"] = Brands;

            return View("addCar");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> addCar(carVM carVM,IFormFile file)
        {
            string imagFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");

            if (!Directory.Exists(imagFolder)) 
            {
                Directory.CreateDirectory(imagFolder);
            }

            string fileName = Path.GetFileName(file.FileName);
            string fileSavePath = Path.Combine(imagFolder, fileName);

            using (FileStream stream = new FileStream(fileSavePath, FileMode.Create)) 
            {
                await file.CopyToAsync(stream);
            }

            cars car = new cars();
            Claim _id = User.Claims
                            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            car.Name = carVM.Name;
            car.Model = carVM.Model;
            car.Price = carVM.Price;
            car.Year = carVM.Year;
            car.Transmission = carVM.Transmission;
            car.Color = carVM.Color;
            car.EngineCapacity = carVM.EngineCapacity;
            car.ImageURL = fileName;
            car.classID = carVM.classID;
            car.sellerID = int.Parse(_id.Value);
            
            carRepo.Add(car);
            carRepo.Save();
            ViewBag.admin = User.Identity.Name;
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            List<classOfCar> carClasses = classOfCar.GetAll();

            List<string> transimations = ["Manual", "Automatic", "Semi-Automatic"];
            List<int> cc = [1, 2, 3];
            List<string> Brands = ["BMW", "Audi", "Chevrolet", "Hyundai", "Jeep", "KIA", "MG", "Fiat"];

            ViewData["classOfCar"] = carClasses;
            ViewData["transimations"] = transimations;
            ViewData["cc"] = cc;
            ViewData["Brands"] = Brands;

            carVM carVM = new carVM();
            cars car = carRepo.GetById(id);

            carVM.Name = car.Name;
            carVM.Price = car.Price;
            carVM.Year = car.Year;
            carVM.Transmission = car.Transmission;
            carVM.Color = car.Color;
            carVM.classID = car.classID;
            carVM.Model = car.Model;
            carVM.EngineCapacity = car.EngineCapacity;
            carVM.ImageURL = car.ImageURL;

            
            return View("edit", carVM);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> saveEdit(carVM carVM, IFormFile file) 
        {
            string imagFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");

            if (!Directory.Exists(imagFolder))
            {
                Directory.CreateDirectory(imagFolder);
            }

            string fileName =  Path.GetFileName(file.FileName);
            string fileSavePath = Path.Combine(imagFolder, fileName);

            using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            if (ModelState.IsValid == true) 
           {
                cars car = carRepo.GetByName(carVM.Model,carVM.EngineCapacity);

                Claim _id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                car.Name = carVM.Name;
                car.Price = carVM.Price;
                car.Year = carVM.Year;
                car.Transmission = carVM.Transmission;
                car.Color = carVM.Color;
                car.ImageURL = fileName;
                car.EngineCapacity = carVM.EngineCapacity;
                car.classID = carVM.classID;
                car.Model = carVM.Model;
                car.sellerID = int.Parse(_id.Value);

                if (car.ImageURL == null)
                {
                    car.ImageURL = carVM.ImageURL;
                }
                carRepo.Update(car);
                carRepo.Save();
                return RedirectToAction("Index");
           }

           return View("edit", carVM);
        }
        [Authorize(Roles = "admin")]
        public IActionResult delete(int id) 
        {
       /*     if(purchaseRepo.GetByCarId(id)!=null)
            {
                purchaseRepo.DeletebyCarId(id);
                carRepo.Delete(id);
            }*/
            carRepo.Delete(id);
            carRepo.Save();

            return RedirectToAction("Index");
        }
        
        public IActionResult Details(int id) 
        {
            cars car = carRepo.GetById(id);

            return View("Details",car);
        }

        public IActionResult Search(string name, int PageNo = 1) 
        {
            List<cars> cars = carRepo.getCarsByName(name);

            //pagination
            int NoOfRecordsPerPage = 3;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(cars.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
            int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;

            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;

            cars = cars.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();


            if (cars.Count > 0)
            {
                return View("allCars", cars);
            }

            return RedirectToAction("Index");
        }

       
    }
}
