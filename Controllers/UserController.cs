using CompleteApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompleteApp.Controllers
{
    public class UserController : Controller
    {
        StudentContext _context= new StudentContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            return Json(_context.Users.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Json(new { success = true, message = "Added Successfully" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            return Json(_context.Users.FirstOrDefault(x => x.Id == ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Users user)
        {
            var data = _context.Users.FirstOrDefault(x => x.Id == user.Id);
            if (data != null)
            {
                data.Name = user.Name;
                data.State = user.State;
                data.Country = user.Country;
                data.Age = user.Age;
                _context.SaveChanges();
            }
            return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {

           
            var data = _context.Users.FirstOrDefault(x => x.Id == ID);
            _context.Users.Remove(data);
            _context.SaveChanges();
            return Json( new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}