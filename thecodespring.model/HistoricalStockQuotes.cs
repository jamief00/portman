using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace thecodespring.model
{
    public class HistoricalStockQuotes
    {
        public String TIDM { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<StockQuote> Quotes { get; set; }
    }
}
