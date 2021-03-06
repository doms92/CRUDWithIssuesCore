using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDWithIssuesCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWithIssuesCore.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext dbContext)
        {
            context = dbContext;
        }

        public async Task<IActionResult> Details(int id)
        {
            Student stu = await StudentDb.GetStudent(context, id);
            return View(stu);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Student> products = await StudentDb.GetStudents(context);
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Create(Student p)
        {
            if (ModelState.IsValid)
            {
               await StudentDb.Add(p, context);
                ViewData["Message"] = $"{p.Name} was added!";
               
                return RedirectToAction("Index");
            }

            //Show web page with errors
            return View(p);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //get the product by id
            Student p = await StudentDb.GetStudent(context, id);

            //show it on web page
            return View(p);
        }

        [HttpPost]
        public async Task< IActionResult> Edit(Student p)
        {
            if (ModelState.IsValid)
            {
                await StudentDb.Update(context, p);
                ViewData["Message"] = "Product Updated!";
                return RedirectToAction("Index");
            }
            //return view with errors
            return View(p);
        }

        [HttpGet]
        public async Task< IActionResult> Delete(int id)
        {
            Student p = await StudentDb.GetStudent(context, id);
            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        public async Task< IActionResult> DeleteConfirm(int id)
        {
            //Get Product from database
            Student p = await StudentDb.GetStudent(context, id);

           await StudentDb.Delete(context, p);

            return RedirectToAction("Index");
        }
    }
}