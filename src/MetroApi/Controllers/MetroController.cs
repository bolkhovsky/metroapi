using HtmlAgilityPack;
using MetroApi.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApi.OutputCache.V2.TimeAttributes;
using System.Net;

namespace MetroApi.Web.Controllers
{
    /// <summary>
    /// Base controller for Metro stations queries
    /// </summary>
    [RoutePrefix("api/metro")]
    public class MetroController : ApiController
    {
        private static readonly int CountSPBMetroLines = 5;

        private static readonly string TargetUrlMoscow = "https://ru.wikipedia.org/wiki/%D0%A1%D0%BF%D0%B8%D1%81%D0%BE%D0%BA_%D1%81%D1%82%D0%B0%D0%BD%D1%86%D0%B8%D0%B9_%D0%9C%D0%BE%D1%81%D0%BA%D0%BE%D0%B2%D1%81%D0%BA%D0%BE%D0%B3%D0%BE_%D0%BC%D0%B5%D1%82%D1%80%D0%BE%D0%BF%D0%BE%D0%BB%D0%B8%D1%82%D0%B5%D0%BD%D0%B0";
        private static readonly string TargetUrlSPB = "https://ru.wikipedia.org/wiki/%D0%A1%D0%BF%D0%B8%D1%81%D0%BE%D0%BA_%D1%81%D1%82%D0%B0%D0%BD%D1%86%D0%B8%D0%B9_%D0%9F%D0%B5%D1%82%D0%B5%D1%80%D0%B1%D1%83%D1%80%D0%B3%D1%81%D0%BA%D0%BE%D0%B3%D0%BE_%D0%BC%D0%B5%D1%82%D1%80%D0%BE%D0%BF%D0%BE%D0%BB%D0%B8%D1%82%D0%B5%D0%BD%D0%B0";

        /// <summary>
        /// Get list of metro stations in target city
        /// </summary>
        /// <param name="cityId">City ID, can be taken from api/cities</param>
        /// <returns><c>IEnumerable</c> of MetroStation type</returns>
        [CacheOutputUntilThisMonth(25)]
        [Route("{cityId}")]
        public City GetCityMetro(string cityId)
        {
            // TODO: 1) Get Date of last wiki article Update and check if we have any changes
            // TODO: 2) Get more Info about metro stations and Save to our DB

            var htmlWeb = new HtmlWeb();

            City city;

            switch (cityId)
            {
                case Constants.CityIds.Moscow:
                    var htmlDoc = htmlWeb.Load(TargetUrlMoscow);

                    var contentElement = htmlDoc.GetElementbyId("mw-content-text");

                    city = City.Moscow();
                    city.MetroLines = GetMoscowMetroSchema(contentElement);
                    break;

                case Constants.CityIds.SaintPetersburg:
                    htmlDoc = htmlWeb.Load(TargetUrlSPB);

                    contentElement = htmlDoc.GetElementbyId("mw-content-text");

                    city = City.SaintPetersburg();
                    city.MetroLines = GetSpbMetroSchema(contentElement); 
                    break;

                default:
                    throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return city;
        }

        private List<MetroLine> GetMoscowMetroSchema(HtmlNode contentElement)
        {
            var lines = new List<MetroLine>();

            var wikiTable = contentElement.Element("table");
            var allMetroRows = wikiTable.Elements("tr");

            foreach (HtmlNode metroRow in allMetroRows)
            {
                if (!metroRow.ChildNodes.Any(n => n.Name == "td"))
                    continue;

                var rowCells = metroRow.Elements("td").ToList();

                var lineNameSpan = rowCells[0].ChildNodes.First(n => n.Attributes.Any(a => a.Name == "title"));
                var lineName = lineNameSpan.Attributes.First(a => a.Name == "title").Value;
                var metro = new MetroStation(rowCells[1].Element("span").Element("a").InnerText);
                var line = new MetroLine(lineName);
                line.Stations.Add(metro);
                lines.Add(line);
            }

            return lines;
        }

        private static List<MetroLine> GetSpbMetroSchema(HtmlNode contentElement)
        {
            var lines = new List<MetroLine>();

            var currentNode = contentElement.Element("h2");

            for (int i = 0; i < CountSPBMetroLines; i++)
            {
                string lineName = currentNode.Element("span").Element("a").InnerText;
                var tableNode = currentNode.NextSibling.NextSibling;
                var stationRows = tableNode.Elements("tr").Skip(2);

                var line = new MetroLine(lineName);

                foreach (var row in stationRows)
                {
                    var rowCells = row.Elements("td").ToList();
                    var stationName = rowCells[0].Descendants("a").ToList()[0].InnerText;
                    line.Stations.Add(new MetroStation(stationName));
                    // клиент просил добавить вестибюль
                    // вообще-то это не станция
                    // TODO: адаптировать BL под вестибюли
                    if (lineName == "Фрунзенско-Приморская линия" && stationName == "Спортивная")
                    {
                        line.Stations.Add(new MetroStation("Спортивная 2"));
                    }
                }

                lines.Add(line);
                currentNode = tableNode.NextSibling.NextSibling;
            }

            return lines;
        }
    }
}
