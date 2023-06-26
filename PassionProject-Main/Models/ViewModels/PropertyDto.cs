using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_Main.Models
{
    public class PropertyDto
    {
        public int PropertyId { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
        public int NoOfRooms { get; set; }
        public decimal Price { get; set; }
    }
}