using System;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // tipos de graph e hello world
            //IGraph g = new Graph();
            //IUriNode dotnetRDF = g.CreateUriNode(UriFactory.Create("http://www.dotnetrdf.org"));
            //IUriNode says = g.CreateUriNode(UriFactory.Create("http://example.org/says"));
            //ILiteralNode helloWorld = g.CreateLiteralNode("Hello World");
            //ILiteralNode bonjourMonde = g.CreateLiteralNode("Bonjour tout le Monde", "fr");

            //g.Assert(new Triple(dotnetRDF, says, helloWorld));
            //g.Assert(new Triple(dotnetRDF, says, bonjourMonde));

            // somente para debug
            //foreach (Triple t in graph.Triples)
            //{
            //    Console.WriteLine(t);
            //}

            // formato NT válido
            //NTriplesWriter nTWriter = new NTriplesWriter();
            //nTWriter.Save(g, "HelloWorld.nt");


            // definição XML
            //RdfXmlWriter rdfXmlWriter = new RdfXmlWriter();
            //rdfXmlWriter.Save(g, "HelloWorld.rdf");

            // não funcionou
            //IGraph g = new Graph();
            //var a = new TripleStore();
            //UriLoader.Load(graph2, new Uri("http://dbpedia.org/resource/Barack_Obama"));
            //UriLoader.Load(graph2, new Uri("http://dbpedia.org/page/Barack_Obama"));
            //UriLoader.Load(a, new Uri("http://dbpedia.org/page/Battle_of_Waterloo"));

            // funcionou
            //NTriplesParser nTriplesParser = new NTriplesParser();
            //nTriplesParser.Load(graph2, "Barack_Obama.ntriples");

            // working with graph
            //Graph g = new Graph();
            //g.BaseUri = new Uri("http://example.org/base");
            //var isEmpty = g.IsEmpty;
            //var triples = g.Triples;

            //First define a SPARQL Endpoint for DBPedia
            //SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"));

            ////Next define our query
            ////We're going to ask DBPedia to describe the first thing it finds which is a Person
            //String query = "DESCRIBE ?person WHERE {?person a <http://dbpedia.org/ontology/Person>} LIMIT 1";

            ////Get the result
            //var g = endpoint.QueryWithResultGraph(query);

            // navegando na triple store para pegar uma lista
            //TripleStore store = new TripleStore();
            //Graph graph = new Graph();
            //var nTriplesParser = new NTriplesParser();
            //UriLoader.Load(graph, new Uri("http://dbpedia.org/resource/List_of_ongoing_armed_conflicts"), nTriplesParser);
            //store.Add(graph);
            // store.Triples as the conflicts itself?

            // Cities
            SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"));
            String query = @"DESCRIBE ?City WHERE { ?City a <http://dbpedia.org/ontology/City> } LIMIT 10";
            var cities = endpoint.QueryWithResultGraph(query);


            Console.ReadKey();
        }
    }
}
