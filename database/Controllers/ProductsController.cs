using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using database.Models;
using System.IO;




namespace database.Controllers
{
    public class ProductsController : Controller
    {
        public StoreeEntities1 db = new StoreeEntities1();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Cart).Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Products/Details/5
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

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Carts, "product_id", "product_id");
            ViewBag.category_id = new SelectList(db.Categories, "id", "name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,price,image,description,category_id")] Product product, HttpPostedFileBase imgfile)
        {
            if (ModelState.IsValid)
            {
                String path = "";
                if(imgfile.FileName.Length > 0)
                {
                    path = "~/images/" + Path.GetFileName(imgfile.FileName);
                    imgfile.SaveAs(Server.MapPath(path));
                }
                product.image = path ;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Carts, "product_id", "product_id", product.Id);
            ViewBag.category_id = new SelectList(db.Categories, "id", "name", product.category_id);
            return View(product);
        }

        // GET: Products/Edit/5
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
            ViewBag.Id = new SelectList(db.Carts, "product_id", "product_id", product.Id);
            ViewBag.category_id = new SelectList(db.Categories, "id", "name", product.category_id);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,price,image,description,category_id")] Product product, HttpPostedFileBase imagefile)
        {
            if (ModelState.IsValid)
            {
                String path = "";
                if (imagefile.FileName.Length > 0)
                {
                    path = "~/images/" + Path.GetFileName(imagefile.FileName);
                    imagefile.SaveAs(Server.MapPath(path));
                }
                product.image = path;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Carts, "product_id", "product_id", product.Id);
            ViewBag.category_id = new SelectList(db.Categories, "id", "name", product.category_id);
            return View(product);
        }

        // GET: Products/Delete/5
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

        // POST: Products/Delete/5
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
        public ActionResult Add_Cart()
        {
            return View();
        }
        public ActionResult Products(string searching)
        {

            
            var cat = from s in db.Products
                select s;
            if(!string.IsNullOrEmpty(searching))
            {
                cat = cat.Where(s => s.Category.name.Contains(searching));
            }

            return View(cat.ToList());
        }

        public ActionResult homePage()
        {
            var recs = db.Products.ToList();

            return View(recs);
        }
        public ActionResult view_details(string searching_details)
        {
            //var det = db.Products.ToList();

            //return View(det);
            var det = from s in db.Products
                      select s;
            if (!string.IsNullOrEmpty(searching_details))
            {
                det = det.Where(s => s.name.Contains(searching_details));
            }

            return View(det.ToList());
        }
        
        public ActionResult Add_toCart(int id)
        {

            List<Item> session_add = new List<Item>();
            var bb = db.Products.Find(id);
            session_add.Add(new Item()
            {
                product = bb,
               
            });
            Session["session_add"] = session_add;

            return View();
        }
        public ActionResult Remove_FromCart(int id)
        {
            List<Item> session_add = (List<Item>)Session["session_add"];
            foreach (var item in session_add)
            {
                if (item.product.Id == id)
                {
                    session_add.Remove(item);
                    break;
                }
            }
            Session["session_add"] = session_add;
            return Redirect("Products");
            
        }
    }
}
