using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WestCoastEdu.DataAccess.Repository.IRepository;
using WestCoastEdu.Models;
using WestCoastEdu.Models.ViewModels;

namespace WestCoastEdu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LocationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LocationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Location> locationList = _unitOfWork.Location.GetAll();
            return View(locationList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Location location)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Location.Add(location);
                _unitOfWork.Save();
                TempData["success"] = "Location created successfully";
                return RedirectToAction("Index");
            }
            return View(location);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var locationFromDbFirst = _unitOfWork.Location.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (locationFromDbFirst == null)
            {
                return NotFound();
            }

            return View(locationFromDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Location.Update(location);
                _unitOfWork.Save();
                TempData["success"] = "Location updated successfully";
                return RedirectToAction("Index");
            }
            return View(location);
        }

        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var locationFromDbFirst = _unitOfWork.Location.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (locationFromDbFirst == null)
            {
                return NotFound();
            }

            return View(locationFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Location.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Location.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Location deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
