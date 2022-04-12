using System;
using System.ComponentModel.DataAnnotations;

namespace Payment.Models
{
    public class Payment
    {
        [Key]
        public string Id { get; set; }
        public string OrderId { get; set; }
        public DateTime Date { get; set; }
        public int Total { get; set; }
        public string Status { get; set; }
    }
}
