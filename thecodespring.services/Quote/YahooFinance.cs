using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using thecodespring.model;
using thecodespring.services.Interfaces;

namespace thecodespring.services.Quote
{
    public class YahooFinance : IQuoteProvider
    {
        public List<StockQuote> RetrieveQuotes(DateTime Start, DateTime End, String TIDM)
        {
            //DateTime startDate = DateTime.Parse("1900-01-01");
            List<StockQuote> results = new List<StockQuote>();

            string baseURL = "http://ichart.finance.yahoo.com/table.csv?";
            string queryText = BuildHistoricalDataRequest(TIDM, Start, End);
            string url = string.Format("{0}{1}", baseURL, queryText);

            //Get page showing the table with the chosen indices
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader stReader = null;

            //csv content
            string docText = string.Empty;
            string csvLine = null;
            try
            {
                request = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
                request.Timeout = 300000;
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;


                response = (HttpWebResponse)request.GetResponse();

                stReader = new StreamReader(response.GetResponseStream(), true);

                stReader.ReadLine();//skip the first (header row)
                while ((csvLine = stReader.ReadLine()) != null)
                {
                    string[] sa = csvLine.Split(new char[] { ',' });

                    DateTime date = DateTime.Parse(sa[0].Trim('"'));
                    Double open = double.Parse(sa[1]);
                    Double high = double.Parse(sa[2]);
                    Double low = double.Parse(sa[3]);
                    Double close = double.Parse(sa[4]);
                    Double volume = double.Parse(sa[5]);
                    Double adjClose = double.Parse(sa[6]);
                    // Process the data (e.g. insert into DB)

                    StockQuote quote = new StockQuote()
                    {
                        TIDM = TIDM,
                        Price = close,
                        PriceTime = date,
                        Volume = volume,
                        AdjustedClose = adjClose, 
                        High = high,
                        Low = low
                    };

                    results.Add(quote);
                }

                return results;
            }
            catch
            {
                return null;
            }

        }

        static string BuildHistoricalDataRequest(string symbol, DateTime startDate, DateTime endDate)
        {
            // We're subtracting 1 from the month because yahoo
            // counts the months from 0 to 11 not from 1 to 12.
            StringBuilder request = new StringBuilder();
            request.AppendFormat("s={0}", symbol);
            request.AppendFormat("&a={0}", startDate.Month - 1);
            request.AppendFormat("&b={0}", startDate.Day);
            request.AppendFormat("&c={0}", startDate.Year);
            request.AppendFormat("&d={0}", endDate.Month - 1);
            request.AppendFormat("&e={0}", endDate.Day);
            request.AppendFormat("&f={0}", endDate.Year);
            request.AppendFormat("&g={0}", "d"); //daily

            return request.ToString();
        }
    }
}
