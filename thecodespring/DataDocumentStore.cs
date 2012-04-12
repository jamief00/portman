using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Raven.Client.Embedded;

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
            docStore = new EmbeddableDocumentStore { 
                ConnectionStringName = "RavenDB"
  
            };
            docStore.Conventions.IdentityPartsSeparator = "-";
            docStore.Initialize();
            return docStore;
        }



    }
}