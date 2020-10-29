using CompleteApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompleteApp.Controllers
{
    public class StudentController : Controller
    {
        StudentContext db = new StudentContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Getdata()
        {
            var students = db.Students.ToList();
            return Json(new { data = students }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Student());
            else
            {
                return View(db.Students.Where(x => x.StudentId == id).FirstOrDefault<Student>());
            }
        }

       

        [HttpPost]
        public ActionResult AddorEdit(Student student)
        {
            if(student.StudentId==0)
            {
                db.Entry(student).State = EntityState.Added;
                db.SaveChanges();
                return Json( new { success=true, message= "Saved Successfully done"}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "Updated Successfully done" });
            }
          
        }

        public ActionResult Details(int id)
        {
            return View(db.Students.Where(c => c.StudentId == id).FirstOrDefault());
        }
      /*  public ActionResult Delete(int id)
        {
            var students = db.Students.Where(c => c.StudentId == id).FirstOrDefault();
            return View(students);
        }*/

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var students = db.Students.Where(c => c.StudentId == id).FirstOrDefault();
            db.Entry(students).State = EntityState.Deleted;
            db.SaveChanges();
           
            return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            return View(db.Students.Where(c => c.StudentId == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(Student students)
        {
            db.Entry(students).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}