﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuyalotWebShoppingApp.Models;
using BuyalotWebShoppingApp.DAL;

namespace BuyalotWebShoppingApp.Controllers
{
    public class CustomerManagementController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: CustomerManagement
        public ActionResult Index()
        {
            if (Session["adminName"] != null)
            {

                var customers = unitOfWork.CustomerRepository.Get();
                foreach (var item in customers)
                {
                    Session["CusCount"] = customers.Count();
                }
                return View(customers.ToList());
            }
            else
            {
                return RedirectToAction("Login", "AdminAccount");
            }

            
        }

        // GET: CustomerManagement/Details/5
        public ActionResult Details(int id)
        {
            if (Session["adminName"] != null)
            {

                Customer customer = unitOfWork.CustomerRepository.GetByID(id);
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "AdminAccount");
            }

            
        }

        // GET: CustomerManagement/Create
        public ActionResult Create()
        {
            if (Session["adminName"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "AdminAccount");
            }

            
        }

        // POST: CustomerManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customerID,FirstName,LastName,Phone,Email,State")]Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.CustomerRepository.Insert(customer);
                    unitOfWork.Save();

                    return RedirectToAction("Index");

                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, contact your system administrator.");
            }
            return View(customer);
        }

        // GET: CustomerManagement/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["adminName"] != null)
            {

                Customer customer = unitOfWork.CustomerRepository.GetByID(id);
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "AdminAccount");
            }

        }

        // POST: CustomerManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customerID,FirstName,LastName,Phone,Email,State")]Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.CustomerRepository.Update(customer);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, contact your system administrator.");
            }
            return View(customer);
        }

        // GET: CustomerManagement/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["adminName"] != null)
            {

                Customer customer = unitOfWork.CustomerRepository.GetByID(id);
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "AdminAccount");
            }

        }

        // POST: CustomerManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = unitOfWork.CustomerRepository.GetByID(id);
            unitOfWork.CustomerRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
