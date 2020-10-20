using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace GeneralStoreApi.Models.Entities
{
    public class Transaction
    {[Key]
        public int TransactionId { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get;set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public DateTime DateOfTransaction { get; set; } = DateTime.Now;
    }
}