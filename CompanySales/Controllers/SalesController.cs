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
using CompanySales.Service;
using CompanySales.ViewModel;

namespace CompanySales.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;
       
        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }
        public SalesController()
        {
            _salesService = new SalesService();
        }
        
        // GET: Sales
        public ActionResult Index()
        {
            try
            {

                IReadOnlyList<Sales> existingData = _salesService.GetSales();

                List<List<string>> _invertedData = _salesService.GetInvertedSales(existingData);
                List<string> _columnsHeaders = _salesService.GetColumnHeaders(existingData);
                List<string> _rowTotal = _salesService.GetTotal(_invertedData);
                List<string> _rowMedian = _salesService.GetMedian(_invertedData);
                List<string> _rowAverage = _salesService.GetAverage(_invertedData);


                //get footers rows
                List<List<string>> _footerRows = new List<List<string>>();
                _footerRows.Add(_salesService.GetAverage(_invertedData));
                _footerRows.Add(_salesService.GetMedian(_invertedData));
                _footerRows.Add(_salesService.GetTotal(_invertedData));

                //add footers
                foreach (List<string> _footerRow in _footerRows)
                {
                    _invertedData.Add(_footerRow);
                }
                _invertedData.Insert(0, _columnsHeaders);
                return View(new SalesViewModel() { ExistingSaleData = _invertedData, NewSale = new Sales() });
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        
        // POST: Sales/Create
        [HttpPost]
        public ActionResult Create(SalesViewModel salesViewModel)
        {
            try
            {
                _salesService.AddSale(salesViewModel.NewSale);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw (ex);
                //return RedirectToAction("Index");
            }
        }
    }
}