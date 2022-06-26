using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDB
{
    internal class Course
    {
        public string id { get; set; }
        public string courseid { get; set; }
        public string CourseName { get; set; }
        public decimal Rating { get; set; }
        public List<Order> Orders { get; set; }
    }
}
