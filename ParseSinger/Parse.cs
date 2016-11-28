using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using ModelSinger;

namespace ParseSinger
{
    public static class Parse
    {
        public static Encoding encode = Encoding.GetEncoding("utf-8");

        public static IEnumerable<Singer> GetSingersFromPage(string url)
        {
            var html = GetHtml(url);
            var tableNodes = html.DocumentNode.SelectNodes("//table[@class='items']//tr").ToArray();

            var singers = new List<Singer>();

            for (int i = 1; i < 3/*tableNodes.Length*/; i++)
            {
                var aNode = tableNodes[i].SelectSingleNode(".//td[@class='artist_name']//a");

                var viewsString = tableNodes[i].SelectNodes(".//td[@class='number']").Last().InnerText;
                var views = Convert.ToInt32(SelectNumerics(viewsString));

                var urlSinger = "http:" + aNode.Attributes["href"].Value;

                //var biography = GetBiography(urlSinger);
                var songs = GetSongsFromPage(urlSinger);

                singers.Add(new Singer
                {
                    Name = aNode.InnerText,
                    Url = urlSinger,
                    Views = views,
                    //Biography = biography,
                    Songs = songs.ToList()
                });
            }

            return singers;
        }

        public static IEnumerable<Song> GetSongsFromPage(string url)
        {
            var html = GetHtml(url);
            var songs = new List<Song>();

            var tableNodes = html.DocumentNode.SelectNodes("//table[@id='tablesort']//tr").ToArray();

            for (var i = 1; i < tableNodes.Length; i++)
            {
                
                var viewsString = tableNodes[i].SelectSingleNode(".//td[@class='number hidden-phone']").InnerText;
                var views = Convert.ToInt32(SelectNumerics(viewsString));

                var aNode = tableNodes[i].SelectSingleNode(".//td//a");
                var urlSong = "http:" + aNode.Attributes["href"].Value;

                var text = GetSongText(urlSong);

                var accords = GetSongAccords(urlSong);

                songs.Add(new Song
                {
                    Name = aNode.InnerText,
                    Views = views,
                    Url = urlSong,
                    Text = text,
                    Accords = accords.ToList()
                });
            }

            return songs;
        }

        public static string GetSongText(string url)
        {
            var html = GetHtml(url);
            var text = html.DocumentNode.SelectSingleNode("//div[@class='b-podbor__text']//pre").InnerText;
            return text;
        }

        public static IEnumerable<Accord> GetSongAccords(string url)
        {
            var html = GetHtml(url);
            var accords = new List<Accord>();

            var imgNodes = html.DocumentNode.SelectNodes("//div[@id='song_chords']//img").ToList();

            foreach (var imgNode in imgNodes)
            {
                var name = imgNode.Attributes["alt"].Value;
                var imageSrc = "http:" + imgNode.Attributes["src"].Value;

                var image = GetImageFromUrl(imageSrc);

                accords.Add(new Accord
                {
                    Image = image,
                    Name = name
                });
            }

            return accords;
        }

        public static IEnumerable<Singer> GetSingers(string startUrl)
        {
            var html = GetHtml(startUrl);

            var singers = new List<Singer>();

            singers.AddRange(GetSingersFromPage(startUrl));

            var countPages = Convert.ToInt32(GetCountPages(html));

            for (int i = 2; i <= countPages; i++)
            {
                var url = startUrl + "page" + i + "/";
                singers.AddRange(GetSingersFromPage(url));
            }

            return singers;
        }

        private static string GetBiography(string url)
        {
            var html = GetHtml(url);
            var aNode = html.DocumentNode.SelectSingleNode("//div[@class='artist-profile__bio']//a");

            if (aNode == null)
                return null;
            var href = aNode.Attributes["href"].Value;
            var htmlWiki = GetHtml("http://amdm.ru" + href);

            return htmlWiki.DocumentNode.SelectSingleNode("//div[@class='artist-profile__bio']").InnerText;
        }

        private static string GetCountPages(HtmlDocument html)
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
            System.Threading.Thread.Sleep(1000);
            using (var wc = new WebClient() { Encoding = encode })
            {
                var html = new HtmlDocument();
                html.LoadHtml(wc.DownloadString(url));
                return html;
            }
        }

        private static byte[] GetImageFromUrl(string url)
        {
            using (var webClient = new WebClient())
            {
                return webClient.DownloadData(url);
            }
        }

        private static void DownloadRemoteImageFile(string uri, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect) &&
                response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
            {

                // if the remote file was found, download oit
                using (Stream inputStream = response.GetResponseStream())
                using (Stream outputStream = File.OpenWrite(fileName))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                }
            }
        }


        //private static string GetHtmlString(string url)
        //{
        //    System.Threading.Thread.Sleep(500);
        //    var request = WebRequest.Create(url);
        //    request.Proxy = new WebProxy();
        //    var response = request.GetResponse();

        //    using (var sReader = new StreamReader(response.GetResponseStream(), encode))
        //    {
        //        return sReader.ReadToEnd();
        //    }
        //}
    }
}
