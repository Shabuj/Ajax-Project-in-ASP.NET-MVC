using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using CompleteApp.Models;
using System.Data.Entity;
using System.Net;

namespace CompleteApp.Controllers
{
    public class ImageController : Controller
    {
        StudentContext db = new StudentContext();
        // GET: Image
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Create(HttpPostedFileBase file, Employee image )
        {
            string filename  = Path.GetFileName(file.FileName);
            string _filename = DateTime.Now.ToString("yymmfff") + filename;
            string extension = Path.GetExtension(file.FileName);
            string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
            image.Image = "~/Images/" + _filename;
             
            if(extension.ToLower() == ".jpg" || extension.ToLower() == ".JPG" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".JPEG" || extension.ToLower() == ".png")
            {
                if(file.ContentLength <= 1000000)
                {
                    db.Employees.Add(image);
                    if(db.SaveChanges()>0)
                    {
                        file.SaveAs(path);
                        ViewBag.msg = "Record Added Successfully Done ..";
                        ModelState.Clear();
                    }
                }
                else
                {
                    ViewBag.msg = "Size is not valid ";
                }
            }
            return View();

        }

        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var emp = db.Employees.Find(id);
            Session["imgPath"] = emp;
            if(emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        [HttpPost]

        public ActionResult Edit(HttpPostedFileBase file, Employee image)
        {

            string filename = Path.GetFileName(file.FileName);
            string _filename = DateTime.Now.ToString("yymmfff") + filename;
            string extension = Path.GetExtension(file.FileName);
            string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
            image.Image = "~/Images/" + _filename;

            if (ModelState.IsValid)
            {
                if (file != null)
                {

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".JPG" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".JPEG" || extension.ToLower() == ".png")
                    {
                        if (file.ContentLength <= 1000000)
                        {
                            db.Entry(image).State = EntityState.Modified;
                            string oldImgPath = Request.MapPath(Session["imgPath"].ToString());
                            if (db.SaveChanges() > 0)
                            {
                                file.SaveAs(path);
                                if (System.IO.File.Exists(oldImgPath))
                                {
                                    System.IO.File.Delete(oldImgPath);
                                }
                                TempData["msg"] = " Image Updated Successfully Done";
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            ViewBag.msg = "Size is not Valid";
                        }
                    }
                }
                else
                {
                    db.Entry(image).State = EntityState.Modified;
                    if(db.SaveChanges()>0)
                    {
                        TempData["msg"] = "Image Updated Successfully Done";
                        return RedirectToAction("Index");
                    }
                }

            }

            return View();

        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var details = db.Employees.Find(id);
            if(details == null)
            {
                return HttpNotFound();
            }
            return View(details);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            string currentImage = Request.MapPath(employee.Image);
            db.Entry(employee).State = EntityState.Deleted;

            if(db.SaveChanges()>0)
            {
                if(System.IO.File.Exists(currentImage))
                {
                    System.IO.File.Delete(currentImage);
                }
                TempData["msg"] = "Data Deleted Sccessfully ";
                return RedirectToAction("Index");
            }
            return View();

        }

       

                //public ActionResult Create(Employee image, HttpPostedFileBase file)
                //{
                //    if(ModelState.IsValid)
                //    {
                //        if (file != null)
                //        {

                //            file.SaveAs(HttpContext.Server.MapPath("~/Images/") + file.FileName);
                //            image.Image = file.FileName;
                //        }
                //        db.Employees.Add(image);
                //        db.SaveChanges();
                //        return RedirectToAction("Index");
                //    }
                //    return View();


                //}

    }

}