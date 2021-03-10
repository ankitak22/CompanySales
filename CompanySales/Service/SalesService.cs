using System;
using System.Collections.Generic;
using System.Linq;
using CompanySales.DAL;
using CompanySales.Models;

namespace CompanySales.Service
{
    public class SalesService : ISalesService
    {
        private SalesContext db = new SalesContext();

        public bool AddSale(Sales newSale)
        {
            db.Sales.Add(newSale);
            db.SaveChanges();
            return true;
        }

        public IReadOnlyList<Sales> GetSales()
        {
            IReadOnlyList<Sales> existingData = db.Sales.ToList();
            return existingData;
        }

        public List<List<string>> GetInvertedSales(IReadOnlyList<Sales> existingData)
        {
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

            return result;
        }

        public List<string> GetColumnHeaders(IReadOnlyList<Sales> existingData)
        {
            var columns = new[] { "Month" }.Union(existingData.Select(a => a.State).OrderBy(a => a)).ToList();
            return columns;
        }

        public List<string> GetAverage(List<List<string>> _invertedData)
        {
            //List<string> _footerAverageRow = new List<string>();

            List<string> _footerAverageRow = new List<string>();
            _footerAverageRow.Add("Average");
            
            for (int i = 1; i < _invertedData[0].Count; i++)
            {
                var sum = _invertedData.Sum(x => float.Parse(x[i]));
                var colArray = _invertedData.Select(r => r[i]).ToArray();
                Array.Sort(colArray);

                _footerAverageRow.Add((sum / _invertedData.Count).ToString("0.00"));

            }

            return _footerAverageRow;
        }

        public List<string> GetMedian(List<List<string>> _invertedData)
        {
            List<string> _footerMedianRow = new List<string>();

            //List<string> median = new List<string>();
            _footerMedianRow.Add("Median");
            for (int i = 1; i < _invertedData[0].Count; i++)
            {
                var colArray = _invertedData.Select(r => r[i]).ToArray();
                Array.Sort(colArray);

                
                string med = "";

                if (colArray.Length % 2 != 0)
                    med = (colArray[colArray.Length / 2]).ToString();
                else
                    med = ((Convert.ToDouble(colArray[(colArray.Length - 1) / 2]) + Convert.ToDouble(colArray[colArray.Length / 2])) / 2).ToString();

                _footerMedianRow.Add(med);
            }

            return _footerMedianRow;
        }

        public List<string> GetTotal(List<List<string>> _invertedData)
        {
            List<string> _footerTotalRow = new List<string>();
            _footerTotalRow.Add("Total");
            
            for (int i = 1; i < _invertedData[0].Count; i++)
            {
                var sum = _invertedData.Sum(x => float.Parse(x[i]));
                var colArray = _invertedData.Select(r => r[i]).ToArray();
                Array.Sort(colArray);

                _footerTotalRow.Add(sum.ToString());

            }
            return _footerTotalRow;
        }
    }
}