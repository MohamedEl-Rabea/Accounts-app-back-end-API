using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskAPI.Models
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
        public double Amount { get; set; }

        [MaxLength(1)]
        public string Operator { get; set; }

        [ForeignKey("FromCurrencyId")]
        public Currency FromCurrency { get; set; }

        [ForeignKey("ToCurrencyId")]
        public Currency ToCurrency { get; set; }
    }
}