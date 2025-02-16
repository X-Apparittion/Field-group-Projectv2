﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Finalv2.Models;

namespace Finalv2.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private Final_DBEntities db = new Final_DBEntities();

        // GET: Forum
        public async Task<ActionResult> Index()
        {
            var fora = db.Fora.Include(f => f.Cours).Include(f => f.Users_Stud);
            return View(await fora.ToListAsync());
        }

        // GET: Forum/Details/5
        public async Task<ActionResult> Details(decimal id)
        {
            if (id != null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = await db.Fora.FindAsync(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        // GET: Forum/Create
        public ActionResult Create()
        {
            ViewBag.Course_ID = new SelectList(db.Courses, "Course_ID", "Department");
            ViewBag.User_ID = new SelectList(db.Users_Stud, "User_ID", "Username");
            return View();
        }

        // POST: Forum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Post_ID,Post_Content,Post_date,Course_ID,User_ID")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                db.Fora.Add(forum);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Course_ID = new SelectList(db.Courses, "Course_ID", "Department", forum.Course_ID);
            ViewBag.User_ID = new SelectList(db.Users_Stud, "User_ID", "Username", forum.User_ID);
            return View(forum);
        }

        // GET: Forum/Edit/5
        public async Task<ActionResult> Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = await db.Fora.FindAsync(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            ViewBag.Course_ID = new SelectList(db.Courses, "Course_ID", "Department", forum.Course_ID);
            ViewBag.User_ID = new SelectList(db.Users_Stud, "User_ID", "Username", forum.User_ID);
            return View(forum);
        }

        // POST: Forum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Post_ID,Post_Content,Post_date,Course_ID,User_ID")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forum).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Course_ID = new SelectList(db.Courses, "Course_ID", "Department", forum.Course_ID);
            ViewBag.User_ID = new SelectList(db.Users_Stud, "User_ID", "Username", forum.User_ID);
            return View(forum);
        }

        // GET: Forum/Delete/5
        public async Task<ActionResult> Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = await db.Fora.FindAsync(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        // POST: Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(decimal id)
        {
            Forum forum = await db.Fora.FindAsync(id);
            db.Fora.Remove(forum);
            await db.SaveChangesAsync();
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
