using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Program
    {
        public static Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
        public static WebClient wClient;
        public static WebRequest request;
        public static WebResponse response;

        static void Main(string[] args)
        {
            wClient = new WebClient();
            wClient.Encoding = encode;

            //var singers = GetSingers("http://amdm.ru/chords/");

            var html = GetHtml("http://amdm.ru/chords/");
            var singers = GetSingersFromPage(html);

            //foreach (var item in singers)
            //{
            //    Console.WriteLine(item.Name + " " + item.Views + " " + item.Url + "\n\n" + item.Biography);
            //}
        }

        public static IEnumerable<Singer> GetSingersFromPage(HtmlDocument html)
        {
            var tableNodes = html.DocumentNode.SelectNodes("//table[@class='items']//tr").ToArray();

            var singers = new List<Singer>();

            for (int i = 1; i < tableNodes.Length; i++)
            {
                var aNode = tableNodes[i].SelectSingleNode(".//td[@class='artist_name']//a");
                var viewsString = tableNodes[i].SelectNodes(".//td[@class='number']").Last().InnerText;
                var views = Convert.ToInt32(SelectNumerics(viewsString));
                var url = aNode.Attributes["href"].Value;

                //var htmlBiography = GetHtml("http:" + url);
                //var biography = GetBiography(htmlBiography);

                var htmldoc = new HtmlDocument();
                htmldoc.LoadHtml(GetHtmlString("http:" + url));
                var biography = GetBiography(htmldoc);

                singers.Add(new Singer
                {
                    Name = aNode.InnerText,
                    Url = url,
                    Views = views,
                    Biography = biography
                });
            }

            return singers;
        }

        public static IEnumerable<Song> GetSongsFromPage(HtmlDocument html)
        {
            var songs = new List<Song>();

            var tableNodes = html.DocumentNode.SelectNodes("//table[@id='tablesort']//tr").ToArray();
            
            for (int i = 1; i < tableNodes.Length; i++)
            {
                var aNode = tableNodes[i].SelectSingleNode(".//td//a");
                var viewsString = tableNodes[i].SelectSingleNode(".//td[@class='number hidden-phone']").InnerText;
                var views = Convert.ToInt32(SelectNumerics(viewsString));

                songs.Add(new Song
                {
                    Name = aNode.InnerText,
                    Views = views,
                    Url = aNode.Attributes["href"].Value
                });
            }

            return songs;            
        }

        public static IEnumerable<Singer> GetSingers(string startUrl)
        {
            var html = GetHtml(startUrl);

            var singers = new List<Singer>();

            singers.AddRange(GetSingersFromPage(html));

            var countPages = Convert.ToInt32(GetCountPages(html));

            for (int i = 2; i <= countPages; i++)
            {
                var url = startUrl + "page" + i.ToString() + "/";
                html = GetHtml(url);
                singers.AddRange(GetSingersFromPage(html));
            }

            return singers;
        }

        public static string GetBiography(HtmlDocument html)
        {
            var aNode = html.DocumentNode.SelectSingleNode("//div[@class='artist-profile__bio']//a");

            if (aNode == null)
                return null;
            else
            {
                var href = aNode.Attributes["href"].Value;
                //var htmlWiki = GetHtml("http://amdm.ru" + href);

                var htmldoc = new HtmlDocument();
                htmldoc.LoadHtml(GetHtmlString("http://amdm.ru" + href));
                return htmldoc.DocumentNode.SelectSingleNode("//div[@class='artist-profile__bio']").InnerHtml;
            }
        }

        public static string GetCountPages(HtmlDocument html)
        {
            var a = html.DocumentNode.SelectNodes("//ul[@class='nav-pages']//li//a").Last();
            var url = a.Attributes["href"].Value;
            var count = SelectNumerics(url);
            return count;
        }

        private static string SelectNumerics(string s)
        {
            var pattern = @"[0-9]";
            var regex = new Regex(pattern);

            var str = "";
            var match = regex.Matches(s);
            foreach (var item in match)
            {
                str += item.ToString();
            }
            return str;
        }

        private static HtmlDocument GetHtml(string url)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(wClient.DownloadString(url));
            return html;
        }

        public static string GetHtmlString(string url)
        {
            request = WebRequest.Create(url);
            request.Proxy = null;
            response = request.GetResponse();
            using (StreamReader sReader = new StreamReader(response.GetResponseStream(), encode))
            {
                return sReader.ReadToEnd();
            }
        }
    }
}
