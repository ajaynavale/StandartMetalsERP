using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using StandardMetals.CommonLibrary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardMetals.MODEL
{
    public class SupplierModelIM
    {
        public int SupplierID { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
    }

    public class CustomerModelIM
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
    }

    public class ItemMasterModalApril2026
    {
        public int Id { get; set; }
        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [CustomeValidationClass(ErrorMessage = "Duplicate Name are not Allowed")]
        [Required(ErrorMessage = "Please Enter Item Name")]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Please Enter Material")]
        public string Material { get; set; }

        [Display(Name = "HSN-Code")]
        public string HSNCODE { get; set; }

        [Display(Name = "GST Rate")]
        public double? GSTRate { get; set; }

        [Display(Name = "Purchase Cost")]
        public double? PurchaseCost { get; set; }

        [Display(Name = "Selling Price")]
        public double? SellingPrice { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Select Manufacture")]
        public int ManufactureId { get; set; }

        [Display(Name = "Manufacture")]
        public string ManufactureName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Select Colour")]
        public int ColourId { get; set; }

        [Display(Name = "Colour")]
        public string ColourName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Select Unit")]
        public int UnitId { get; set; }

        [Display(Name = "Unit")]
        public string UnitName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Select Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Select Sub-Category")]
        public int SubCategoryId { get; set; }

        [Display(Name = "Sub Category")]
        public string SubCategoryName { get; set; }


        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public int SupplierID { get; set; }

        public List<SupplierModelIM> SupplierList { get; set; } = new List<SupplierModelIM>();

        public List<CustomerModelIM> CustList { get; set; } = new List<CustomerModelIM>();

        
    }

    public class CustomeValidationClass : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
//            LogInfo.LogDetailsToFile("Inside ItemMasterModalApr2026 model ItemNameDuplicateCheckCustomeValidation - Start");

            if (value != null)
            {
                LogInfo.LogDetailsToFile("Inside If- Value not Null : " + value);

                string ValueName = value.ToString();
                string str = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(str);
                SqlCommand cmd = new SqlCommand("SPCustValIM", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ItamName", ValueName);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                return dt.Rows.Count == 0 ? ValidationResult.Success : new ValidationResult("Duplicate Name Not Allowed");
                //if (dt.Rows.Count > 0)
                //{
                //    LogInfo.LogDetailsToFile("Duplicate Records Finds : " + value);
                //    LogInfo.LogDetailsToFile("Inside ItemMasterModalApril2026 model CustomeValidationClass - Start-End");

                //    return new ValidationResult(ErrorMessage);
                //}
            }
//            LogInfo.LogDetailsToFile("Inside ItemMasterModalApr2026 model ItemNameDuplicateCheckCustomeValidation - Start-End");
            return new ValidationResult("Item Name Required");
        }
    }

}