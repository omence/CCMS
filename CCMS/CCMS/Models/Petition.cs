using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.Models
{
    public class Petition
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Orginization { get; set; }

        public DateTime Date { get; set; }
    }
}
