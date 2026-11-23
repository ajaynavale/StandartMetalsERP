using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandardMetals.MODEL;
using StandardMetals.DAL;

namespace StandardMetals.BAL
{
    public class DefaultDashBoard_BAL
    {
        DefaultDashBoard_DAL DefaultDashboard_Dal = new DefaultDashBoard_DAL();

        public DefaultModal GetSupplierWiseCount()
        {
            return DefaultDashboard_Dal.GetSupplierWiseCount();
        }

        public DefaultModal GetCustomerWiseCount()
        {
            return DefaultDashboard_Dal.GetCustomerWiseCount();
        }
    }
}