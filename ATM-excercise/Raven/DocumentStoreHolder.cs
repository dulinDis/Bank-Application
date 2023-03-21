using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ATM_excercise.Raven
{

    public static class DocumentStoreHolder
    {
        // Use Lazy<IDocumentStore> to initialize the document store lazily. 
        // This ensures that it is created only once - when first accessing the public `Store` property.
        private static readonly Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

        public static IDocumentStore Store => store.Value;

        private static IDocumentStore CreateStore()
        {
            IDocumentStore store = new DocumentStore()
            {
                // Define the cluster node URLs (required)
                Urls = new[] { "http://127.0.0.1:8080/", 
                           /*some additional nodes of this cluster*/ },

                // Set conventions as necessary (optional)
                Conventions =
            {
                MaxNumberOfRequestsPerSession = 10,
                UseOptimisticConcurrency = true
            },

                // Define a default database (optional)
                Database = "bank",

                // Define a client certificate (optional)
                //Certificate = new X509Certificate2("C:\\path_to_your_pfx_file\\cert.pfx"),

                // Initialize the Document Store
            }.Initialize();

            return store;
        }
    }




    //public static class DocumentStoreHolder
    //{
    //    //a class which represents database itself

    //    private static readonly Lazy<IDocumentStore> LazyStore =
    //        new Lazy<IDocumentStore>(() =>
    //        {
    //            IDocumentStore store = new DocumentStore
    //            {
    //                //this is array because if we use cluster we can lis several urls
    //                Urls = new[] { "http://127.0.0.1:8080/" },
    //                Database = "bank",
    //            };
    //            store.Initialize();
    //            return store;
    //        });



    //    //signleon on document store -if we use dependency invejction i should use life cycle og signleon object | simulaing singleon apterin a hrea safe manner
    //    public static IDocumentStore Store => LazyStore.Value;

    //}





}
