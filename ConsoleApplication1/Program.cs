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
            wClient.Proxy = null;
            wClient.Encoding = encode;

            HtmlDocument html = new HtmlDocument();

            html.LoadHtml(wClient.DownloadString("http://amdm.ru/chords/"));

            HtmlNode bodyNod = html.DocumentNode.SelectSingleNode("//table[@class='items']");
            Console.WriteLine(bodyNod.ChildNodes[1].InnerText);
        }
    }
}
