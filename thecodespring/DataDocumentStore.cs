using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Abstractions.Data;
using Raven.Client.Document;

namespace thecodespring
{
    public class DataDocumentStore
    {
        private static IDocumentStore docStore;

        public static IDocumentStore DocStore
        {
            get
            {
                if(docStore == null){
                    throw new InvalidOperationException("Document Store not initialised");
                }

                return docStore;
            }
        }

        public static IDocumentStore Initialize()
        {
            bool embedded = false;
            var parser = ConnectionStringParser<RavenConnectionStringOptions>.FromConnectionStringName("RavenDB");
            try
            {
                parser.Parse();
            } catch {
                embedded = true;
            }

            if (embedded)
            {
                docStore = new EmbeddableDocumentStore
                {
                    ConnectionStringName = "RavenDB"
                };
            }
            else {
                

                docStore = new DocumentStore
                {
                    ApiKey = parser.ConnectionStringOptions.ApiKey,
                    Url = parser.ConnectionStringOptions.Url,
                };  

            }
            docStore.Conventions.IdentityPartsSeparator = "-";
            docStore.Initialize();
            return docStore;
        }



    }
}