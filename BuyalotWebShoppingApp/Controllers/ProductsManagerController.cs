﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BuyalotWebShoppingApp.Models;
using BuyalotWebShoppingApp.DAL;
using PagedList;

namespace BuyalotWebShoppingApp.Controllers
{
    //[Authorize]
    public class ProductsManagerController : Controller
    {

        private BuyalotDbContext db = new BuyalotDbContext();

        // GET: ProductsManager
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductCategory);
            return View(products.ToList());
        }

        // GET: ProductsManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: ProductsManager/Create
        public ActionResult Create()
        {
            ViewBag.ProdCategoryID = new SelectList(db.ProductCategories, "ProdCategoryID", "CategoryName");
            Product pro = new Product();
            return View(pro);

        }

        // POST: ProductsManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,ProductDescription,ProdCategoryID,Price,Vendor,QuantityInStock,ProductImage")] Product product, FormCollection collection, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    product.ProductImage = new byte[upload.ContentLength];
                    upload.InputStream.Read(product.ProductImage, 0, upload.ContentLength);
                }
                db.Products.Add(product);
                db.SaveChanges();
                ViewBag.result = "Product " + product.Vendor + " " + product.ProductName + " Added Succesfully!";

                return RedirectToAction("Index");
            }

            ViewBag.ProdCategoryID = new SelectList(db.ProductCategories, "ProdCategoryID", "CategoryName", product.ProdCategoryID);
            return View(product);
        }

        // GET: ProductsManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.prodCategoryID = new SelectList(db.ProductCategories, "ProdCategoryID", "CategoryName", product.ProdCategoryID);
            return View(product);
        }

        // POST: ProductsManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,ProductDescription,ProdCategoryID,Price,Vendor,QuantityInStock,ProductImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.prodCategoryID = new SelectList(db.ProductCategories, "ProdCategoryID", "CategoryName", product.ProdCategoryID);
            return View(product);
        }

        // GET: ProductsManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: ProductsManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
