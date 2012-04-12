using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using thecodespring.model;

namespace thecodespring.Models
{
    public class StockQuoteViewModel
    {
        public HistoricalStockQuotes HistoricalQuotes { get; set; }

        public bool IsEmpty()
        {
            return (HistoricalQuotes == null || HistoricalQuotes.Quotes == null) ? true : false;
        }
    }
}