using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace StandardMetals.MODEL
{
    public class DefaultModal
    {
        public List<DefaultModal> SupplierList { get; set; } = new List<DefaultModal>();

        public string SupplierName { get; set; }

        [Display(Name = "Total Supplier")]
        public int TotalSuppCount { get; set; }
        [Display(Name = "Total Items")]
        public int TotalItemCount { get; set; }
        [Display(Name = "Items")]
        public int ItemCount { get; set; }

        public List<DefaultModal> CustomerList { get; set; } = new List<DefaultModal>();

        public string CustName { get; set; }

        [Display(Name = "Total Customer")]
        public int TotalCustCount { get; set; }
    }
}