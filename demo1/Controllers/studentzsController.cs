using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Blob;

namespace demo1.Controllers
{

    public class studentzsController : Controller
    {
        private demoazureEntities1 db = new demoazureEntities1();
        

        // GET: studentzs
        public ActionResult Index()
        {
            return View(db.studentz.ToList());
        }

        // GET: studentzs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentz studentz = db.studentz.Find(id);
            if (studentz == null)
            {
                return HttpNotFound();
            }
            return View(studentz);
        }

        // GET: studentzs/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: studentzs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,id")] studentz studentz,HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                db.studentz.Add(studentz);
                db.SaveChanges();
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("storageconnectionstring"));

                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();


                CloudQueue queue = queueClient.GetQueueReference("demoqueue");


                queue.CreateIfNotExists();

                CloudQueueMessage cloudQueueMessage = new CloudQueueMessage("id:" + studentz.id.ToString() + "studentname" + studentz.name);
                queue.AddMessage(cloudQueueMessage);

                if (image != null && image.ContentLength > 0)
                {
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                    CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("newblob");
                    cloudBlobContainer.CreateIfNotExists();
                    cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(studentz.id.ToString() + ".png");
                    cloudBlockBlob.UploadFromStream(image.InputStream);
                }

             

               



                return RedirectToAction("Index");
            }

            return View(studentz);
        }

        // GET: studentzs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentz studentz = db.studentz.Find(id);
            if (studentz == null)
            {
                return HttpNotFound();
            }
            return View(studentz);
        }

        // POST: studentzs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "name,id")] studentz studentz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentz);
        }

        // GET: studentzs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentz studentz = db.studentz.Find(id);
            if (studentz == null)
            {
                return HttpNotFound();
            }
            return View(studentz);
        }

        // POST: studentzs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            studentz studentz = db.studentz.Find(id);
            db.studentz.Remove(studentz);
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
