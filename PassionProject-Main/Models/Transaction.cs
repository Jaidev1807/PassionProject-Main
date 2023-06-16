using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject_Main.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public string BuyerInfo { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TransactionAmount { get; set; }

        [ForeignKey("Agent")]
        public int AgentId { get; set; }
        public virtual Agent Agent { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }
        public virtual Property Property { get; set; }
    }
}