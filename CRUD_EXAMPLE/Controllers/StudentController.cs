using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CRUD_EXAMPLE.DAL;
using CRUD_EXAMPLE.Models;

namespace CRUD_EXAMPLE.Controllers
{
    public class StudentController : Controller
    {

        StudentDAL _studentDal = new StudentDAL();

        // GET: Student
        public ActionResult Index()
        {
            var studentList = _studentDal.GetAllStudent();
            if (studentList.Count == 0)
            {
                TempData["InfoMessage"] = "Currenly No products";
            }
            return View(studentList);
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            var student = _studentDal.GetStudentById(id);
            if (student == null)
            {
                // return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Student model, HttpPostedFileBase imageFile, HttpPostedFileBase pdfFile)

        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    Address = model.Address,
                    Gender = model.Gender,
                    Dob = model.Dob,
                    City = model.City,
                    State = model.State,
                    Pin = model.Pin,
                    Email=model.Email
                };

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(imageFile.InputStream))
                    {
                        student.ImageData = binaryReader.ReadBytes(imageFile.ContentLength);
                    }
                }

                if ( pdfFile!= null && pdfFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(pdfFile.InputStream))
                    {
                        student.PdfData = binaryReader.ReadBytes(pdfFile.ContentLength);
                    }
                }

                bool result = _studentDal.InsertStudent(student);

                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to insert student record.");
                }
            }

            return View(model);
        }








        public ActionResult GetPdf(int id)
        {
            var pdfData = _studentDal.GetPdfData(id);

            return File(pdfData, "application/pdf", "student.pdf");

        }







        //  try
        //{
        // TODO: Add insert logic here

        //    return RedirectToAction("Index");
        // }
        //  catch
        // {
        // return View();
        //  }



    








        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            var student = _studentDal.GetStudentById(id);
            if (student == null)
            {
               // return NotFound();
            }

            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public  ActionResult Edit(int id, Student student, HttpPostedFileBase imageFile, HttpPostedFileBase pdfFile)
        {
            

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(imageFile.InputStream))
                    {
                        student.ImageData = binaryReader.ReadBytes(imageFile.ContentLength);
                    }
                }

                if (pdfFile != null && pdfFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(pdfFile.InputStream))
                    {
                        student.PdfData = binaryReader.ReadBytes(pdfFile.ContentLength);
                    }
                }

                bool success = _studentDal.UpdateStudent(student);
                if (success)
                {
                    return RedirectToAction("Index"); // Redirect to a list or details page
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                }
            }

            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            var student = _studentDal.GetStudentById(id);
            if (student == null)
            {
                // return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            bool success = _studentDal.DeleteStudent(id); // Replace with your actual method to delete student by ID
            if (success)
            {
                return RedirectToAction("Index"); // Redirect to list or home page
            }
            else
            {
                return View(); // Show the delete view again if deletion failed
            }

        }
    }
}
