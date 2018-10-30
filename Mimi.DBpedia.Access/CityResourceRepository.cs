using Mimi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using VDS.RDF;
using VDS.RDF.Query;

namespace Mimi.DBpedia.Access
{
    public class CityResourceRepository
    {
        public List<CityTile> GetCities()
        {
            SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"));
            //String query = "SELECT ?City ?label WHERE { ?City a <http://dbpedia.org/ontology/City> . ?City rdfs:label ?label . } LIMIT 200";
            string query = $@"SELECT ?City, ?label, (COUNT(?City) as ?CityCount) WHERE {{ 
                                    ?City a <http://dbpedia.org/ontology/City>.
                                    ?City rdfs:label ?label.
                                    ?thing dbo:location ?City.
                                optional
                                {{
                                    ?thing a ?type.
                                    VALUES ?type {{<http://dbpedia.org/ontology/Hotel>}}
                                    BIND('Hotel' as ?typeName )
                                }}
                                optional
                                {{
                                    ?thing a ?type.
                                    VALUES ?type {{ dbo:Museum}}
                                    BIND('Museum' as ?typeName )
                                }}
                                optional
                                {{
                                    ?thing a ?type.
                                    VALUES ?type {{ dbo:Pyramid}}
                                    BIND('Pyramid' as ?typeName )
                                }}
                                optional
                                {{
                                    ?thing a ?type.
                                    VALUES ?type {{ yago:Skyscraper104233124 }}
                                    BIND('Skyscraper' as ?typeName )
                                }}
                                optional
                                {{
                                    ?thing a ?type.
                                    VALUES ?type {{ dbo:Park }}
                                    BIND('Park' as ?typeName )
                                }}
                                optional
                                {{
                                    ?thing a ?type.
                                    VALUES ?type {{ yago:Church103028079 }}
                                    BIND('Church' as ?typeName )
                                }}
                                optional
                                {{
                                    ?thing a ?type.
                                    VALUES ?type {{ dbo:HistoricPlace}}
                                    BIND ('Historic Place' as ?typeName )
                                }}
                                {{
                                    ?thing a dbo:Place
                                }}
                                filter (BOUND (?type))
                                FILTER (lang(?label) = 'en')
                            }}
                            GROUP BY ?City ?label
                            HAVING(COUNT(?City) > 30)
                            ORDER BY DESC (?CityCount)
                            LIMIT 200";
            query = query.Replace("\r\n", "");

            var cities = endpoint.QueryWithResultSet(query);

            return cities.Results.Select(x => new CityTile
            {
                Name =  (x["label"] as ILiteralNode).Value,
                PointsOfInterestCount = (x["CityCount"] as ILiteralNode).Value,
                ResourceName = x["City"].ToString()
            }).ToList();
        }

        public CityInfo GetCityInfo(string cityResource)
        {
            var cityInfo = new CityInfo();

            // TODO: implementar busca das informações da cidade usando sparql se ela não estiver em alguma store local (salva em cache)
            cityInfo.Name = cityResource;
            cityInfo.Summary = cityResource;
            cityInfo.PointOfInterests = GetPointsOfInterest(cityResource);

            return cityInfo;
        }

        public List<CityPointOfInterest> GetPointsOfInterest(string cityResource)
        {
            SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"));
            
            String query = $@"select ?thing ?type ?typeName where {{
                                VALUES ?city {{<{cityResource}>}}
                                    ?thing dbo:location ?city.
                                optional 
                                {{
                                    ?thing a ?type .
                                    VALUES ?type {{<http://dbpedia.org/ontology/Hotel>}}
                                    BIND( 'Hotel' as ?typeName )
                                }}
                                    optional
                                {{
                                    ?thing a ?type.
                                    VALUES ?type
                                    {{ dbo:Museum }}
                                    BIND( 'Museum' as ?typeName )
                                }}
                                optional
                                {{
                                    ?thing a ?type.
                                     VALUES ?type {{ dbo:Pyramid}}
                                BIND( 'Pyramid' as ?typeName )
                                }}
                            optional
                                {{
                                    ?thing a ?type.
                                     VALUES ?type {{ yago:Skyscraper104233124}}
                            BIND( 'Skyscraper' as ?typeName )
                                }}
                                optional
                                {{
                                    ?thing a ?type.
                                     VALUES ?type {{ dbo:Park}}
                            BIND( 'Park' as ?typeName )
                                }}
                                optional
                                {{
                                    ?thing a ?type.
                                     VALUES ?type {{ yago:Church103028079}}
                            BIND( 'Church' as ?typeName )
                                }}
                                optional
                                {{
                                    ?thing a ?type.
                                     VALUES ?type {{ dbo:HistoricPlace}}
                            BIND( 'Historic Place' as ?typeName )
                                }}
                                {{
                                    ?thing a dbo:Place
                                }}

                                filter(BOUND (?type))
                            }}";

            query = query.Replace("\r\n", "");

            var pointsOfInterest = endpoint.QueryWithResultSet(query);

            return pointsOfInterest.Results.Select(x => new CityPointOfInterest
            {
                Label = x["thing"].ToString(),
                TypeName = x["typeName"].ToString(),
                Resource = x["thing"].ToString(),
            }).ToList();
        }
    }
}
