using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using thecodespring.model;

namespace thecodespring.services.Interfaces
{
    public interface IQuoteProvider
    {
        List<StockQuote> RetrieveQuotes(DateTime Start, DateTime End, String TIDM);

    }
}
