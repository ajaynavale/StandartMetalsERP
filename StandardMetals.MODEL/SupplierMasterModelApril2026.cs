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
    public class SupplierMasterModelApril2026
    {
        public int SupplierID { get; set; }

        [Display(Name = "RawMaterial")]
        public bool RawMaterial { get; set; }

        [Display(Name = "Supplier Code")]
        public string SupplierCode { get; set; }

        [SupplierNameDuplicateValidation(ErrorMessage = "Duplicate Supplier Name is not Allowed")]
        [Required(ErrorMessage = "Please Enter Supplier Name")]
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }

        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Display(Name = "Branch")]
        public string Branch { get; set; }

        [Display(Name = "Permenant Address")]
        public string Address1 { get; set; }

        [Display(Name = "Residential Address")]
        public string Address2 { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "States")]
        public string State { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "PinCode")]
        public decimal? PinCode { get; set; }

        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please Enter Valid Email")]
        public string EmailID { get; set; }

        [Display(Name = "Telephone Number")]
        public decimal? Telephone { get; set; }

        [Display(Name = "Mobile Number")]
        public decimal? Mobile { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "Website")]
        public string Website { get; set; }

        [Display(Name = "GSTIN")]
        public string GSTIN { get; set; }

        [Display(Name = "Remark")]
        public string Remarks { get; set; }


        [Required(ErrorMessage = "Please Select Branch Name")]
        public int BranchId { get; set; }
        [Required(ErrorMessage = "Please Select State Name")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "Please Select City Name")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "Please Select Country Name")]
        public int CountryId { get; set; }

    }

    public class SupplierNameDuplicateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
//            LogInfo.LogDetailsToFile("Inside SupplierMasterModelApril2026 model SupplierNameDuplicateValidation - Start");

            if (value != null)
            {
//                LogInfo.LogDetailsToFile("Inside If- Value not Null : " + value);

                string ValueName = value.ToString();
                string str = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(str);
                SqlCommand cmd = new SqlCommand("SPCustValSM", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SupplierName", ValueName);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                return dt.Rows.Count == 0 ? ValidationResult.Success : new ValidationResult("Duplicate Name Not Allowed");

            }
 //           LogInfo.LogDetailsToFile("Inside SupplierMasterModelApril2026 model SupplierNameDuplicateValidationCheckCustomeValidation - Start-End");
            return new ValidationResult("Supplier Name Required");
        }


    }
}