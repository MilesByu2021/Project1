using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {
        private TaskContext BlahContext { get; set; }

        public HomeController(TaskContext daContext)
        {
            BlahContext = daContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Form()
        {
            ViewBag.Categories = BlahContext.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Form(TaskResponse add)
        {
            if (ModelState.IsValid)
            {
                BlahContext.Add(add);
                BlahContext.SaveChanges();
                return View("Confirmation", add);
            }
            else
            {
                ViewBag.Categories = BlahContext.Categories.ToList();
                return View(add);
            }
            
        }

        public IActionResult ViewTasks()
        {
            var applications = BlahContext.Responses
                    .Where(a => a.Completed == false)
                    .Include(x => x.Category)
                    .ToList();

            return View(applications);
        }

        [HttpGet]
        public IActionResult Edit(int taskid)
        {
            ViewBag.Categories = BlahContext.Categories.ToList();
            var application = BlahContext.Responses.Single(x => x.TaskId == taskid);
            return View("Form", application);
        }

        [HttpPost]
        public IActionResult Edit(TaskResponse tr)
        {
            BlahContext.Update(tr);
            BlahContext.SaveChanges();
            return RedirectToAction("ViewTasks");
        }

        [HttpGet]
        public IActionResult Delete(int taskid)
        {
            var application = BlahContext.Responses
                .Single(x => x.TaskId == taskid);
            return View("Delete", application);
        }

        [HttpPost]
        public IActionResult Delete(TaskResponse tr)
        {

            BlahContext.Responses.Remove(tr);
            BlahContext.SaveChanges();

            return RedirectToAction("ViewTasks");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
