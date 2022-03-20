using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WestCoastEdu.DataAccess.Repository.IRepository;
using WestCoastEdu.Models;
using WestCoastEdu.Models.ViewModels;

namespace WestCoastEdu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            Models.Customer customer = new();

            if (id is null or 0)
            {
                return View(customer);
            }
            else
            {
                customer = _unitOfWork.Customer.GetFirstOrDefault(p => p.Id == id);
                return View(customer);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Models.Customer obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _unitOfWork.Customer.Add(obj);
                    TempData["success"] = "Customer Created Successfully";
                }
                else
                {
                    _unitOfWork.Customer.Update(obj);
                    TempData["success"] = "Customer Created Successfully";
                }
                _unitOfWork.Save();
                
                return RedirectToAction("Index");

            }
            return View(obj);
        }
        

        
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var customerList = _unitOfWork.Customer.GetAll();
            return Json(new { data = customerList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Customer.GetFirstOrDefault(p => p.Id == id);
            if (obj == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }

            _unitOfWork.Customer.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

    }
}
