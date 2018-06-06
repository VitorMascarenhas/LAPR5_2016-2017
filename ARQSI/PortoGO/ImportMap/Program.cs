using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PortoGO.DB;
using PortoGO.DB.Domain;
using PortoGO.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ImportMap
{
    class Program
    {
        private static readonly IUnitOfWork uow = new UnitOfWork(PortoGoContext.Create());

        private static List<GpsCoordinate> coordinates = new List<GpsCoordinate>();

        static void Main(string[] args)
        {
            //DeletePoi();
            DeleteRoad();
            DeleteLocations();
            DeleteGps();

            var userManager = new UserManager<User>(new UserStore<User>(PortoGoContext.Create()));
            var admin = userManager.FindByName("admin");

            XDocument xml = GetMap();

            //LoadCoordinates(xml);

            LoadGpdAndLocation(xml);

            LoadRoads(xml);

        }

        private static XDocument GetMap()
        {
            //XDocument xml = XDocument.Load("http://overpass-api.de/api/map?bbox=-8.61631,41.14563,-8.61363,41.14743");
            //XDocument xml = XDocument.Load("http://overpass-api.de/api/map?bbox=-8.6931,41.1396,-8.5670,41.1823");
            XDocument xml = XDocument.Load("http://overpass-api.de/api/map?bbox=-8.61425,41.14554,-8.61264,41.14626");

            return xml;
        }

        private static void LoadCoordinates(XDocument xml)
        {
            var query = from c in xml.Root.Descendants("node")
                        select new
                        {
                            id = c.Attribute("id").Value,
                            latitude = c.Attribute("lat").Value,
                            longitude = c.Attribute("lon").Value
                        };

            foreach (var item in query)
            {
                double latitude = Double.Parse(item.latitude, CultureInfo.InvariantCulture.NumberFormat);
                double longitude = Double.Parse(item.longitude, CultureInfo.InvariantCulture.NumberFormat);

                var gps = new GpsCoordinate
                {
                    Id = Convert.ToInt64(item.id),
                    Latitude = latitude,
                    Longitude = longitude
                };

                uow.GpsCoordinateRepository.Insert(gps);
            }

            uow.SaveChanges();
        }

        private static void LoadGpdAndLocation(XDocument xml)
        {
            var query = from c in xml.Root.Descendants("node")
                        select new
                        {
                            id = c.Attribute("id").Value,
                            latitude = c.Attribute("lat").Value,
                            longitude = c.Attribute("lon").Value,
                            tags = c.Elements()
                        };

            foreach (var item in query)
            {
                double latitude = Double.Parse(item.latitude, CultureInfo.InvariantCulture.NumberFormat);
                double longitude = Double.Parse(item.longitude, CultureInfo.InvariantCulture.NumberFormat);

                var gps = new GpsCoordinate
                {
                    Id = Convert.ToInt64(item.id),
                    Latitude = latitude,
                    Longitude = longitude
                };

                coordinates.Add(gps); // adiciona à memoria para pesquisar aquando da criação das estradas

                foreach (var tag in item.tags)
                {
                    if (tag.Attribute("k").Value == "name")
                    {
                        var location = new Location(tag.Attribute("v").Value, gps);

                        uow.LocationRepository.Insert(location);
                    }
                }

                uow.GpsCoordinateRepository.Insert(gps);
            }

            uow.SaveChanges();
        }

        private static void LoadRoads(XDocument xml)
        {
            var query = from c in xml.Root.Descendants("way")
                        select new
                        {
                            nodes = c.Elements("nd"),
                            tags = c.Elements("tag")
                        };

            foreach (var item in query)
            {
                var wayCoordinates = new List<GpsCoordinate>();

                string name = "";

                int cost = 10;

                #region tags

                foreach (var tag in item.tags)
                {
                    if (tag.Attribute("k").Value == "name")
                    {
                        name = tag.Attribute("v").Value;
                    }

                    if (tag.Attribute("k").Value == "highway")
                    {
                        string value = tag.Attribute("v").Value;

                        switch (value.ToLower())
                        {
                            case "residential":
                                cost = 6;
                                break;
                            case "secondary":
                                cost = 2;
                                break;
                            case "tertiary":
                                cost = 3;
                                break;
                            case "pedestrian":
                            case "footway":
                                cost = 4;
                                break;
                            default:
                                cost = 10;
                                break;
                        }
                    }
                }

                #endregion

                foreach (var node in item.nodes)
                {
                    var id = Convert.ToInt64(node.Attribute("ref").Value);

                    var c = coordinates.Where(x => x.Id == id).FirstOrDefault();

                    if (c != null)
                    {
                        wayCoordinates.Add(c);
                    }
                }

                var road = new Road(name, cost, null);

                for (int i = 0; i < wayCoordinates.Count; i++)
                {
                    road.RoadCoordinates.Add(wayCoordinates[i]);

                    uow.RoadRepository.Insert(road);
                }
            }



            uow.SaveChanges();
        }

        private static void DeleteLocations()
        {
            var context = PortoGoContext.Create();

            context.Database.ExecuteSqlCommand("DELETE FROM Location");
        }

        private static void DeletePoi()
        {
            var context = PortoGoContext.Create();

            context.Database.ExecuteSqlCommand("DELETE FROM PointOfInterest");
        }

        private static void DeleteGps()
        {
            var context = PortoGoContext.Create();

            context.Database.ExecuteSqlCommand("DELETE FROM GpsCoordinate");
        }

        private static void DeleteRoad()
        {
            var context = PortoGoContext.Create();

            context.Database.ExecuteSqlCommand("DELETE FROM Road");
        }
    }
}
