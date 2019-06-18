using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure;
using Microsoft.Azure.Documents;
using System.Configuration;
using Microsoft.Azure.Documents.Client;
using System.Collections;


public class studentcosController : Controller
    {

        private static readonly string EndPoint = ConfigurationManager.AppSettings["EndPoint"];
        private static readonly string MasterKey = ConfigurationManager.AppSettings["MasterKey"];
        private static readonly string Database = ConfigurationManager.AppSettings["Database"];
        private static readonly string Collection = ConfigurationManager.AppSettings["Collection"];

    private static  DocumentClient client = new DocumentClient(new Uri(EndPoint), MasterKey);

    private static Uri ObjURI = UriFactory.CreateDocumentCollectionUri(Database, Collection);

    private static FeedOptions objfeed = new FeedOptions { EnableCrossPartitionQuery = true};
       

        // GET: studentcos
        public ActionResult Index()
        {
        //IList<studentz> objstudent = client.CreateDocumentQuery<studentz>(ObjURI, objfeed).ToList();
            return View();
        }

        // GET: studentcos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: studentcos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: studentcos/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: studentcos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: studentcos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: studentcos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: studentcos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }

