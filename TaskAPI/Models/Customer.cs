using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime OpenDate { get; set; }
        public string BranchCode { get; set; }
        public List<CustomerAccount> CustomerAccounts { get; set; }
    }
}