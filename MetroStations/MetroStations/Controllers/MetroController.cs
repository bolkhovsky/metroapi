using HtmlAgilityPack;
using MetroStations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.OutputCache.V2;
using WebApi.OutputCache.V2.TimeAttributes;

namespace MetroStations.Controllers
{
    /// <summary>
    /// Base controller for Metro stations queries
    /// </summary>
    public class MetroController : ApiController
    {
        private static readonly int CountSPBMetroLines = 5;

        private static readonly string TargetUrlMoscow = "https://ru.wikipedia.org/wiki/%D0%A1%D0%BF%D0%B8%D1%81%D0%BE%D0%BA_%D1%81%D1%82%D0%B0%D0%BD%D1%86%D0%B8%D0%B9_%D0%9C%D0%BE%D1%81%D0%BA%D0%BE%D0%B2%D1%81%D0%BA%D0%BE%D0%B3%D0%BE_%D0%BC%D0%B5%D1%82%D1%80%D0%BE%D0%BF%D0%BE%D0%BB%D0%B8%D1%82%D0%B5%D0%BD%D0%B0";
        private static readonly string TargetUrlSPB = "https://ru.wikipedia.org/wiki/%D0%A1%D0%BF%D0%B8%D1%81%D0%BE%D0%BA_%D1%81%D1%82%D0%B0%D0%BD%D1%86%D0%B8%D0%B9_%D0%9F%D0%B5%D1%82%D0%B5%D1%80%D0%B1%D1%83%D1%80%D0%B3%D1%81%D0%BA%D0%BE%D0%B3%D0%BE_%D0%BC%D0%B5%D1%82%D1%80%D0%BE%D0%BF%D0%BE%D0%BB%D0%B8%D1%82%D0%B5%D0%BD%D0%B0";

        /// <summary>
        /// Get list of metro stations in target city
        /// </summary>
        /// <param name="city_id">Integer city ID, can be gotten from api/cities</param>
        /// <returns><c>IEnumerable</c> of MetroStation type</returns>
        [CacheOutputUntilThisMonth(25)]
        public IEnumerable<MetroStation> GetByCity(int city_id)
        {
            // TODO: 1) Get Date of last wiki article Update and check if we have any changes
            // TODO: 2) Get more Info about metro stations and Save to our DB

            var htmlWeb = new HtmlWeb();

            IEnumerable<MetroStation> metroStations;

            switch (city_id)
            {
                case 1:
                    var htmlDoc = htmlWeb.Load(TargetUrlMoscow);

                    var contentElement = htmlDoc.GetElementbyId("mw-content-text");

                    metroStations = GetMoscowStations(contentElement);
                    break;
                case 2:
                    htmlDoc = htmlWeb.Load(TargetUrlSPB);

                    contentElement = htmlDoc.GetElementbyId("mw-content-text");

                    metroStations = GetSPBStations(contentElement);
                    break;
                default:
                    metroStations = new List<MetroStation>();
                    break;
            }

            return metroStations;
        }

        private List<MetroStation> GetMoscowStations(HtmlNode contentElement)
        {
            var allStations = new List<MetroStation>();

            var wikiTable = contentElement.Element("table");
            var allMetroRows = wikiTable.Elements("tr");

            foreach (HtmlNode metroRow in allMetroRows)
            {
                if (!metroRow.ChildNodes.Any(n => n.Name == "td"))
                    continue;

                var metro = new MetroStation();

                var rowCells = metroRow.Elements("td").ToList();

                var lineNameSpan = rowCells[0].ChildNodes.First(n => n.Attributes.Any(a => a.Name == "title"));
                metro.LineName = lineNameSpan.Attributes.First(a => a.Name == "title").Value;
                metro.Name = rowCells[1].Element("span").Element("a").InnerText;

                allStations.Add(metro);
            }

            return allStations;
        }

        private static List<MetroStation> GetSPBStations(HtmlNode contentElement)
        {
            var allStations = new List<MetroStation>();

            var currentNode = contentElement.Element("h2");

            for (int i = 0; i < CountSPBMetroLines; i++)
            {
                string lineName = currentNode.Element("span").Element("a").InnerText;
                var tableNode = currentNode.NextSibling.NextSibling;
                var stationRows = tableNode.Elements("tr").Skip(2);

                foreach (var row in stationRows)
                {
                    var metro = new MetroStation();
                    metro.LineName = lineName;
                    var rowCells = row.Elements("td").ToList();
                    metro.Name = rowCells[0].Descendants("a").ToList()[0].InnerText;
                    allStations.Add(metro);
                }

                currentNode = tableNode.NextSibling.NextSibling;
            }

            return allStations;
        }
    }
}
