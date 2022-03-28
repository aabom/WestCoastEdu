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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Upsert(ProductVM obj, IFormFile? file)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string wwwRothPath = _webHostEnvironment.WebRootPath;
        //        if (file is not null)
        //        {
        //            string fileName = Guid.NewGuid().ToString();
        //            var uploads = Path.Combine(wwwRothPath, @"images\products");
        //            var extension = Path.GetExtension(file.FileName);

        //            if (obj.Product.ImageUrl != null)
        //            {
        //                var oldImagePath = Path.Combine(wwwRothPath, obj.Product.ImageUrl.TrimStart('\\'));
        //                if (System.IO.File.Exists(oldImagePath))
        //                {
        //                    System.IO.File.Delete(oldImagePath);
        //                }
        //            }

        //            using (var fileStreams =
        //                   new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
        //            {
        //                file.CopyTo(fileStreams);
        //            }

        //            obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
        //        }

        //        if (obj.Product.Id == 0)
        //        {
        //            _unitOfWork.Product.Add(obj.Product);
        //        }
        //        else
        //        {
        //            _unitOfWork.Product.Update(obj.Product);
        //        }
        //        _unitOfWork.Save();
        //        TempData["success"] = "Course Created Successfully";
        //        return RedirectToAction("Index");

        //    }
        //    return View(obj);
        //}

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
