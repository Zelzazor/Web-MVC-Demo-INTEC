using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;

namespace Web.MVC.Controllers
{
    public class PeopleController : Controller
    {
        private INTEC_PERSONASEntities db = new INTEC_PERSONASEntities();

        // GET: People
        public ActionResult Index()
        {
            var people = db.People.Include(p => p.ClientType).Include(p => p.Company1).Include(p => p.ContactType).Include(p => p.Department);
            return View(people.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            People people = db.People.Find(id);
            if (people == null)
            {
                return HttpNotFound();
            }
            return View(people);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            ViewBag.ClientTypeId = new SelectList(db.ClientType, "id", "Name");
            ViewBag.CompanyId = new SelectList(db.Company, "id", "Name");
            ViewBag.ContactTypeId = new SelectList(db.ContactType, "id", "Name");
            ViewBag.DepartmentId = new SelectList(db.Department, "id", "Name");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,FirstName,MiddleName,LastName,ClientTypeId,ContactTypeId,SupportStaff,PhoneNumber,EmailAddress,CompanyId,DepartmentId,CreatedAt,Enabled")] People people)
        {
            if (ModelState.IsValid)
            {
                people.CreatedAt = DateTime.Now;
                db.People.Add(people);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientTypeId = new SelectList(db.ClientType, "id", "Name", people.ClientTypeId);
            ViewBag.CompanyId = new SelectList(db.Company, "id", "Name", people.CompanyId);
            ViewBag.ContactTypeId = new SelectList(db.ContactType, "id", "Name", people.ContactTypeId);
            ViewBag.DepartmentId = new SelectList(db.Department, "id", "Name", people.DepartmentId);
            return View(people);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            People people = db.People.Find(id);
            if (people == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientTypeId = new SelectList(db.ClientType, "id", "Name", people.ClientTypeId);
            ViewBag.CompanyId = new SelectList(db.Company, "id", "Name", people.CompanyId);
            ViewBag.ContactTypeId = new SelectList(db.ContactType, "id", "Name", people.ContactTypeId);
            ViewBag.DepartmentId = new SelectList(db.Department, "id", "Name", people.DepartmentId);
            return View(people);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,FirstName,MiddleName,LastName,ClientTypeId,ContactTypeId,SupportStaff,PhoneNumber,EmailAddress,CompanyId,DepartmentId,CreatedAt,Enabled")] People people)
        {
            if (ModelState.IsValid)
            {
                db.Entry(people).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientTypeId = new SelectList(db.ClientType, "id", "Name", people.ClientTypeId);
            ViewBag.CompanyId = new SelectList(db.Company, "id", "Name", people.CompanyId);
            ViewBag.ContactTypeId = new SelectList(db.ContactType, "id", "Name", people.ContactTypeId);
            ViewBag.DepartmentId = new SelectList(db.Department, "id", "Name", people.DepartmentId);
            return View(people);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            People people = db.People.Find(id);
            if (people == null)
            {
                return HttpNotFound();
            }
            return View(people);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            People people = db.People.Find(id);
            db.People.Remove(people);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
