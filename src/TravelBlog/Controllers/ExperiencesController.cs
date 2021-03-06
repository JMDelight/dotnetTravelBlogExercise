﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelBlog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelBlog.Controllers
{
    public class ExperiencesController : Controller
    {
        private TravelBlogDbContext db = new TravelBlogDbContext();
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(db.Experiences.ToList());
        }

        public IActionResult Details(int id)
        {
            var thisExperience = db.Experiences.FirstOrDefault(experiences => experiences.ExperienceId == id);
            var linkedLocation = db.Locations.FirstOrDefault(location => location.LocationId == thisExperience.LocationId);
            var theseExperiencesPeople = db.ExperiencesPeople.Where(experiencePerson => experiencePerson.ExperienceId == thisExperience.ExperienceId).ToList();
            List<Person> thesePeople = new List<Person>();

            foreach ( ExperiencePerson currentExperiencePerson in theseExperiencesPeople) {
                thesePeople.Add(db.People.FirstOrDefault(person => person.PersonId == currentExperiencePerson.PersonId));
                System.Diagnostics.Debug.WriteLine(thesePeople.Count);
            }
            Dictionary<string, object> model = new Dictionary<string, object> { };
            model.Add("experience", thisExperience);
            model.Add("location", linkedLocation);
            model.Add("people", thesePeople);
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Experience experience)
        {
            db.Experiences.Add(experience);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisExperience = db.Experiences.FirstOrDefault(experiences => experiences.ExperienceId == id);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");
            return View(thisExperience);
        }

        [HttpPost]
        public IActionResult Edit(Experience experience)
        {
            db.Entry(experience).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisExperience = db.Experiences.FirstOrDefault(experiences => experiences.ExperienceId == id);
            db.Experiences.Remove(thisExperience);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult AttachPersonSave(int id, Experience experience)
        {
            ExperiencePerson newExperiencePerson = new ExperiencePerson(experience.ExperienceId, id);
            db.ExperiencesPeople.Add(newExperiencePerson);
            db.SaveChanges();
            return RedirectToAction("AttachPerson", experience.ExperienceId);

        }

        public IActionResult AttachPerson(int id)
        {
            var thisExperience = db.Experiences.FirstOrDefault(experiences => experiences.ExperienceId == id);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");
            var theseExeriencesPeople = db.ExperiencesPeople.Where(b => b.ExperienceId == id).ToList();
            var thesePeople = db.People.ToList();
            List<Person> attachedPeople = new List<Person> ();
            List<Person> unattachedPeople = new List<Person>();
            foreach (var person in thesePeople)
            {
                Boolean isAttached = false;
                foreach (var experiencePerson in theseExeriencesPeople)
                {
                    if (experiencePerson.PersonId == person.PersonId && !isAttached)
                        //prevent extra adds with !isAttached even though theoretically unnecessary
                    {
                        attachedPeople.Add(person);
                        isAttached = true;
                    }
                }
                if (!isAttached)
                {
                    unattachedPeople.Add(person);
                }
            }
            Dictionary<string, object> model = new Dictionary<string, object> { };
            model.Add("attachedPeople", attachedPeople);
            model.Add("unattachedPeople", unattachedPeople);
            model["experience"] = thisExperience;
            return View(model);
        }

        

        public string SendMeAString()
        {
            return "Here's your string...";
        }
    }
}
