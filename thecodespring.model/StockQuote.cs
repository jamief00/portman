using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace thecodespring.model
{
    public class StockQuote
    {
        public String TIDM { get; set; }
        public Double Price { get; set; }
        public DateTime PriceTime { get; set; }
        public Double Volume { get; set; }
        public Double AdjustedClose { get; set; }
        public Double High { get; set; }
        public Double Low { get; set; }

    }
}
