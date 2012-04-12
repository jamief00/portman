using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using thecodespring.services.Interfaces;
using thecodespring.model;
using Raven.Client;

namespace thecodespring.services.Quote
{
    public static class QuoteService 
    {
        public static HistoricalStockQuotes RetrieveQuotes(DateTime start, DateTime end, String TIDM)
        {
            IQuoteProvider provider = new YahooFinance();
            HistoricalStockQuotes historicalQuote = null;
            start = start.Date;
            end = end.Date;

            // check for previous occurrence
            using (var session = DataDocumentStore.DocStore.OpenSession())
            {
                var quotes = from h in session.Query<HistoricalStockQuotes>()
                             where h.TIDM == TIDM
                             select h;


                if (quotes.Count() == 1)
                {
                    // previous quote for this symbol
                    historicalQuote = quotes.First();
                    if(!IsRefreshRequired(historicalQuote, start, end)){
                        //remove extraneous quotes
                        var x = from c in historicalQuote.Quotes
                                where c.PriceTime >= start && c.PriceTime <= end
                                select c;

                        historicalQuote.Quotes = x.ToList<StockQuote>();

                        return historicalQuote;
                    }
                }

                List<StockQuote> stockQuotes = provider.RetrieveQuotes(start, end, TIDM);
                if(historicalQuote==null){
                    historicalQuote = new HistoricalStockQuotes
                    {
                        TIDM = TIDM,
                        Start = start,
                        End = end
               
                    };
                }

                historicalQuote.Quotes = stockQuotes;

                session.Store(historicalQuote);
                session.SaveChanges();
            }

            // if a refresh is required, grab all the data and then insert into ravendb

            return historicalQuote;

        }

        private static bool IsRefreshRequired(HistoricalStockQuotes quotes, DateTime desiredStart, DateTime desiredEnd){
            return (desiredStart < quotes.Start || desiredEnd > quotes.End);
        }


    }
}
