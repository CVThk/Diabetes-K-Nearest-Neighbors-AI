using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDiabetes.Models
{
    public class KNN_item
    {
        public List<double> Attributes { get; set; }
        public int Val { get; set; }
        public double Distance { get; set; }

        public KNN_item()
        {
            Attributes = new List<double>();
        }
    }
}