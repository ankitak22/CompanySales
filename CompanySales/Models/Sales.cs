using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompanySales.Models
{
    public class Sales
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Month is Required. It cannot be empty")]
        public string Month { get; set; }

        [Required(ErrorMessage = "State is Required. It cannot be empty")]
        public string State { get; set; }

        [Required(ErrorMessage = "Sale is Required. It cannot be empty")]
        public decimal Sale { get; set; }
    }
}