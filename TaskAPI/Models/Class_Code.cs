using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskAPI.Models
{
    public class Class_Code
    {
        public int Id { get; set; }
        [MaxLength(2)]
        public string Acc_Type { get; set; }
        [MaxLength(3)]
        public string Code { get; set; }
    }
}