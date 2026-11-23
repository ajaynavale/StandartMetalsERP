using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardMetals.MODEL
{
    public class BulkUploadeModel
    {
        // Must match the exact property names used in your DAL
        public string tableName { get; set; }
        public string displayName { get; set; }
    }
}