using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskAPI.Models
{
    public class CustomerAccount
    {
        [Key]
        public int Acc_ID { get; set; }
        [MaxLength(2)]
        public string Acc_Type { get; set; }
        [MaxLength(3)]
        public string Class_Code { get; set; }
        public double Openning_Balance { get; set; }
        [MaxLength(12)]
        public string Acc_Number { get; set; }
        public int CustomerId { get; set; }
        public int CurrencyId { get; set; }
        public bool Status { get; set; }
        public Currency currency { get; set; }
    }
}