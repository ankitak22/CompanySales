using CompanySales.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanySales.ViewModel
{
    public class SalesViewModel
    {
        public Sales NewSale { get; set; }

        public List<List<string>> ExistingSaleData { get; set; }

    }
}