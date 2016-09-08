using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelBlog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelBlog.Controllers
{
    public class PeopleController : Controller
    {
        private TravelBlogDbContext db = new TravelBlogDbContext();
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(db.People.ToList());
        }

        public IActionResult Details(int id)
        {
            var thisPerson = db.People.FirstOrDefault(people => people.PersonId == id);
            var linkedLocation = db.Locations.FirstOrDefault(location => location.LocationId == thisPerson.LocationId);
            Dictionary<string, object> model = new Dictionary<string, object> { };
            model.Add("person", thisPerson);
            model.Add("location", linkedLocation);
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            db.People.Add(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisPerson = db.People.FirstOrDefault(people => people.PersonId == id);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");
            return View(thisPerson);
        }

        [HttpPost]
        public IActionResult Edit(Person person)
        {
            db.Entry(person).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisPerson = db.People.FirstOrDefault(people => people.PersonId == id);
            db.People.Remove(thisPerson);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public string SendMeAString()
        {
            return "Here's your string...";
        }
    }
}
