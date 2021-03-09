using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CompanySales.DAL;
using CompanySales.Models;
using CompanySales.ViewModel;

namespace CompanySales.Controllers
{
    public class SalesController : Controller
    {
        private SalesContext db = new SalesContext();

        // GET: Sales
        public ActionResult Index()
        {
            IReadOnlyList<Sales> existingData = db.Sales.ToList();

            var columns = new[] { "Month" }.Union(existingData.Select(a => a.State).OrderBy(a => a)).ToList();
            var rows = new[] { "State" }.Union(existingData.Select(a => a.Month).OrderBy(a => a)).ToList();
            //var c = existingData.Select(p => p.State).Distinct();
            var result = existingData.GroupBy(g => g.Month).OrderBy(g => g.Key).Select(g => columns.Select(c =>
            {
                string result1 = "";
                if (c == "Month") result1 = g.Key;
                var val = g.FirstOrDefault(r => r.State == c);
                return val != null ? val.Sale.ToString() : "0";
            }).ToList()).ToList();

            //add row headers
            for (int i = 0; i < rows.Count - 1; i++)
            {
                result[i][0] = rows[i + 1];
            }

            List<string> average = new List<string>();
            average.Add("Average");
            List<string> total = new List<string>();
            total.Add("Total");
            List<string> median = new List<string>();
            median.Add("Median");
            List<string> empty = new List<string>();
            empty.Add(" ");
            for (int i = 1; i < result[0].Count; i++)
            {
                var sum = result.Sum(x => float.Parse(x[i]));
                var colArray = result.Select(r => r[i]).ToArray();
                Array.Sort(colArray);

                average.Add((sum / result.Count).ToString("0.00"));
                total.Add(sum.ToString());

                string med = "";

                if (colArray.Length % 2 != 0)
                    med = (colArray[colArray.Length / 2]).ToString();
                else
                    med = ((Convert.ToDouble(colArray[(colArray.Length - 1) / 2]) + Convert.ToDouble(colArray[colArray.Length / 2])) / 2).ToString();

                median.Add(med);
                empty.Add(" ");
            }

            //calculate median



            //add column headers
            result.Insert(0, columns);
            result.Add(empty);
            result.Add(average);
            result.Add(median);
            result.Add(total);

            return View(new SalesViewModel() { ExistingSaleData = result, NewSale = new Sales() });
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // POST: Sales/Create
        [HttpPost]
        public ActionResult Create(SalesViewModel salesViewModel)
        {
            db.Sales.Add(salesViewModel.NewSale);
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
