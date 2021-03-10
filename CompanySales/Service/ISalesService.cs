using CompanySales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.Service
{
    public interface ISalesService
    {
        bool AddSale(Sales newSale);

        IReadOnlyList<Sales> GetSales();

        List<List<string>> GetInvertedSales(IReadOnlyList<Sales> existingData);
        List<string> GetAverage(List<List<string>> _invertedData);
        List<string> GetMedian(List<List<string>> _invertedData);
        List<string> GetTotal(List<List<string>> _invertedData);
        List<string> GetColumnHeaders(IReadOnlyList<Sales> existingData);

    }

}
