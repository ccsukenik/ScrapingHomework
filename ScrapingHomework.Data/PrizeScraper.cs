using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingHomework.Data
{
    public class PrizeScraper
    {
        public List<Prize> Scrape()
        {
            var html = GetOorahHTML();
            return ParseOorahHtml(html);
        }

        public List<Prize> ParseOorahHtml(string html)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);
            var resultDivs = document.QuerySelectorAll(".portfolio-item");
            var prizes = new List<Prize>();

            foreach (var div in resultDivs)
            {
                var item = new Prize();
                var title = div.QuerySelector(".portfolio-caption h4");

                if (title == null)
                {
                    continue;
                }

                if (title != null)
                {
                    item.Title = title.TextContent;
                }

                var winner = div.QuerySelector(".portfolio-caption p");

                if (winner != null)
                {
                    item.Winner = winner.TextContent;
                }

                var imageTag = div.QuerySelector(".img-responsive");
                if (imageTag != null)
                {
                    item.ImageUrl = $"https://www.oorahauction.org/{imageTag.Attributes["src"].Value}";
                }


                prizes.Add(item);

            }

            return prizes;
        }

        public string GetOorahHTML()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            };

            using var client = new HttpClient(handler);
            var url = $"https://www.oorahauction.org/";
            var html = client.GetStringAsync(url).Result;
            return html;
        }
    }
}
