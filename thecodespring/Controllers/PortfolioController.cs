using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using thecodespring.services.Quote;
using thecodespring.Models;
using thecodespring.model;

namespace thecodespring.Controllers
{
    public class PortfolioController : BaseController
    {
        //
        // GET: /Portfolio/
            
        public ActionResult Index(String symbol, uint days)
        {
            HistoricalStockQuotes quotes = QuoteService.RetrieveQuotes(DateTime.Now.AddDays(-days), DateTime.Now, symbol);

            return View(new StockQuoteViewModel
            {
                HistoricalQuotes = quotes
            });

            
        }

        //
        // GET: /Portfolio/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Portfolio/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Portfolio/Create

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
        
        //
        // GET: /Portfolio/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Portfolio/Edit/5

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

        //
        // GET: /Portfolio/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Portfolio/Delete/5

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
}
