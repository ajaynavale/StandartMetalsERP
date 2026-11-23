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
    public class CustomerMasterModelApril2026
    {
        public int Id { get; set; }
        [Display(Name = "CustCode")]
        public string CustCode { get; set; }

        [CustNameDuplicateValidation(ErrorMessage = "Duplicate Customer Name is not Allowed")]
        [Required(ErrorMessage = "Please Enter Customer Name ")]
        [Display(Name = "CustName")]
        public string CustName { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        [Display(Name = "Branch")]
        public string BranchName { get; set; }
        [Required]
        [Display(Name = "Permanent Address")]
        public string Address1 { get; set; }
        [Display(Name = "Residential Address")]
        public string Address2 { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string StateName { get; set; }
        [Display(Name = "PinCode")]
        public string PinCode { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please Enter Valid Email")]
        [Display(Name = "Email")]
        public string EmailId { get; set; }
        [Display(Name = "Telephone")]
        public string Telephone { get; set; }
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        [Display(Name = "FAX")]
        public string Fax { get; set; }
        [Display(Name = "WebSite")]
        public string WebSite { get; set; }
        [Display(Name = "GSTIN")]
        public string GSTIN { get; set; }
        [Display(Name = "Remarks")]
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

    public class CustNameDuplicateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
//            LogInfo.LogDetailsToFile("Inside CustomerMasterModelApril2026 model CustNameDuplicateValidation - Start");

            if (value != null)
            {
                LogInfo.LogDetailsToFile("Inside If- Value not Null : " + value);

                string ValueName = value.ToString();
                string str = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(str);
                SqlCommand cmd = new SqlCommand("SPCustValCM", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerName", ValueName);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                return dt.Rows.Count == 0 ? ValidationResult.Success : new ValidationResult("Duplicate Name Not Allowed");

            }
//            LogInfo.LogDetailsToFile("Inside CustomerMasterModelApril2026 model CustNameDuplicateValidationCheckCustomeValidation - Start-End");
            return new ValidationResult("Item Name Required");
        }

        
    }
}