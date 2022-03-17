using Microsoft.AspNetCore.Mvc;
using WestCoastEdu.DataAccess.Repository.IRepository;
using WestCoastEdu.Models;

namespace WestCoastEdu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatusController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Status> statusList = _unitOfWork.Status.GetAll();
            return View(statusList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Status status)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Status.Add(status);
                _unitOfWork.Save();
                TempData["success"] = "status created successfully";
                return RedirectToAction("Index");
            }
            return View(status);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var statusFromDbFirst = _unitOfWork.Status.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (statusFromDbFirst == null)
            {
                return NotFound();
            }

            return View(statusFromDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Status status)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Status.Update(status);
                _unitOfWork.Save();
                TempData["success"] = "Status updated successfully";
                return RedirectToAction("Index");
            }
            return View(status);
        }

        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var statusFromDbFirst = _unitOfWork.Location.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (statusFromDbFirst == null)
            {
                return NotFound();
            }

            return View(statusFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Status.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Status.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Status deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
