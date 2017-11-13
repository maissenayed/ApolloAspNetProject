﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Apollo.Data;
using Apollo.Domain.entities;

namespace Apollo.ASP.Controllers
{
    public class transportJobsController : Controller
    {
        private JeeModel db = new JeeModel();

        // GET: transportJobs
        public ActionResult Index()
        {
            /* if (Session["token"] != null)
             {
                 return View(db.TransportJobs.ToList());
             }
             else
             {
                 return RedirectToAction("Index", "Home");
             }*/
            var transOrder = db.TransportJobs.Include(u => u.orders.artwork);
            return View(transOrder.ToList());
        }

        // GET: transportJobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transportJob transportJob = db.TransportJobs.Find(id);
            if (transportJob == null)
            {
                return HttpNotFound();
            }
            return View(transportJob);
        }

        // GET: transportJobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: transportJobs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,DateDeDebut,DateDeDefin,title,description")] transportJob transportJob)
        {
            if (ModelState.IsValid)
            {
                db.TransportJobs.Add(transportJob);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transportJob);

        }




        
        [HttpPost]
        public JsonResult AcceptJob(int? id)
        {
            int iduser = Convert.ToInt32(Session["user"].ToString());


            transportJob transportJob = db.TransportJobs.Find(id);
            transportJob.Status = "started";
            transportJob.transporterID = iduser;

            db.TransportJobs.Attach(transportJob);





            db.Entry(transportJob).State = EntityState.Modified;


            try
            {
                db.SaveChanges();
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges


            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

            }

            return  Json(transportJob);


        }

        // GET: transportJobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transportJob transportJob = db.TransportJobs.Find(id);
            if (transportJob == null)
            {
                return HttpNotFound();
            }
            return View(transportJob);
        }

        // POST: transportJobs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,DateDeDebut,DateDeDefin,title,description")] transportJob transportJob)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportJob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transportJob);
        }

        // GET: transportJobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transportJob transportJob = db.TransportJobs.Find(id);
            if (transportJob == null)
            {
                return HttpNotFound();
            }
            return View(transportJob);
        }

        // POST: transportJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            transportJob transportJob = db.TransportJobs.Find(id);
            db.TransportJobs.Remove(transportJob);
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
