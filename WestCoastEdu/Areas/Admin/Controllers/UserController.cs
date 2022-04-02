using System.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WestCoastEdu.DataAccess.Repository.IRepository;
using WestCoastEdu.Models;
using WestCoastEdu.Models.ViewModels;
using WestCoastEdu.Utility;

namespace WestCoastEdu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationUser> userList = _unitOfWork.ApplicationUser.GetAll();
            return View(userList);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _unitOfWork.ApplicationUser.GetAll();
            return Json(new { data = userList });
        }

        //GET
        public IActionResult Edit(string? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var userFromDbFirst = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);

            if (userFromDbFirst == null)
            {
                return NotFound();
            }

            return View(userFromDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ApplicationUser.Update(user);
                _unitOfWork.Save();
                TempData["success"] = "User updated successfully";
                return RedirectToAction("Index");
            }
            return View(user);
        }



        #region API CALLS

        [HttpDelete]
        public IActionResult Delete(string? id)
        {
            var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(p => p.Id == id);
            if (user == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.ApplicationUser.Remove(user);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

    }
}
