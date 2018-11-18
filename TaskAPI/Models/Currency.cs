using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskAPI.Models
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public int ISO_Code { get; set; }
    }
}