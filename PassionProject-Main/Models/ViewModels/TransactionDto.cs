using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_Main.Models
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public string BuyerInfo { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TransactionAmount { get; set; }
        public int AgentId { get; set; }
        public string Name { get; set; }
        public int PropertyId { get; set; }
        public string Address { get; set; }
        public List<AgentDto> agents { get; set; }
        public List<PropertyDto> properties { get; set; }
    }
}