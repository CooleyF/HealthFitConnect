using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthNetwork.Models;

namespace HealthNetwork.Controllers
{
    public class BlogCommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogComments
        public ActionResult Index()
        {
            var blogComments = db.BlogComments.Include(b => b.Author).Include(b => b.BlogPost);
            return View(blogComments.ToList());
        }

        // GET: BlogComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogComment blogComment = db.BlogComments.Find(id);
            if (blogComment == null)
            {
                return HttpNotFound();
            }
            return View(blogComment);
        }

        // GET: BlogComments/Create
        public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.BlogPostID = new SelectList(db.BlogPosts, "ID", "AuthorID");
            return View();
        }

        // POST: BlogComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BlogPostID,AuthorID,Body,Created,Updated,UpdateReason")] BlogComment blogComment)
        {
            if (ModelState.IsValid)
            {
                db.BlogComments.Add(blogComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.Users, "Id", "FirstName", blogComment.AuthorID);
            ViewBag.BlogPostID = new SelectList(db.BlogPosts, "ID", "AuthorID", blogComment.BlogPostID);
            return View(blogComment);
        }

        // GET: BlogComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogComment blogComment = db.BlogComments.Find(id);
            if (blogComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Users, "Id", "FirstName", blogComment.AuthorID);
            ViewBag.BlogPostID = new SelectList(db.BlogPosts, "ID", "AuthorID", blogComment.BlogPostID);
            return View(blogComment);
        }

        // POST: BlogComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BlogPostID,AuthorID,Body,Created,Updated,UpdateReason")] BlogComment blogComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Users, "Id", "FirstName", blogComment.AuthorID);
            ViewBag.BlogPostID = new SelectList(db.BlogPosts, "ID", "AuthorID", blogComment.BlogPostID);
            return View(blogComment);
        }

        // GET: BlogComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogComment blogComment = db.BlogComments.Find(id);
            if (blogComment == null)
            {
                return HttpNotFound();
            }
            return View(blogComment);
        }

        // POST: BlogComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogComment blogComment = db.BlogComments.Find(id);
            db.BlogComments.Remove(blogComment);
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
