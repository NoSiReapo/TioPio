using System;
using HtmlAgilityPack;
using System.Net;

namespace HtmlParser
{
    internal class Parser
    {
        public static void Main()
        {
            //http://links.qatl.ru/
            //https://statler.ru/
            //https://www.travelline.ru/
            //https://station14.ru/ <<-- эта ссылка
            string url;
            Console.WriteLine("Введите url");
            url = Console.ReadLine();
            using StreamWriter goodOutput = new StreamWriter(@"C:\OP_labs\Tests\Labs\Lw_2\GoodOutput.txt");
            using StreamWriter badOutput = new StreamWriter(@"C:\OP_labs\Tests\Labs\Lw_2\BadOutput.txt");
            HashSet<string> links = new HashSet<string>();
            HashSet<string> linksVisited = new HashSet<string>();
            links.Add(url);
            var web = new HtmlWeb();
            var doc = new HtmlWeb().Load(url);
            if (doc == null)
            {
                Console.WriteLine("Bad url");
                return;
            }
            var nodes = doc.DocumentNode.SelectNodes("//a[@href]");
            GetLinks(url, ref links, url);

            int countGood = 0;
            int countBad = 0;

            foreach (var link in links)
            {
                GetStatus(url, goodOutput, badOutput, link, ref countGood, ref countBad);
            }
            goodOutput.WriteLine(countGood);
            badOutput.WriteLine(countBad);
            goodOutput.WriteLine(DateTime.Now.ToString("yyyy-MM-dd=HH:mm:ss:fff"));
            badOutput.WriteLine(DateTime.Now.ToString("yyyy-MM-dd=HH:mm:ss:fff"));
        }

        private static void GetLinks(string url, ref HashSet<string> links, string URL)
        {
            var web = new HtmlWeb();
            var doc = new HtmlWeb().Load(url);
            var nodes = doc.DocumentNode.SelectNodes("//a[@href]");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var link = node.Attributes["href"].Value;
                    if (link != "")
                    {
                        if (link.Contains(URL))
                        {
                            link = link.Substring(URL.Length);
                        }
                        if (links.Contains(link))
                        {
                            continue;
                        }
                        if (link == "/")
                        {
                            continue;
                        }
                        while (link != "" && (link[0] == '/' || link[0] == '.' || link[0] == ' '))
                        {
                            link = link.Substring(1);
                        }
                    }

                    if (link != "" && links.Contains(link) == false && (link.Contains("#") == false) && (link.Contains(':') == false))
                    {
                        if ((link.Contains("http") || link.Contains(".ru") || link.Contains(".com") || link.Contains(".org") || link.Contains(".gg")) && link.Contains(URL) == false)
                        {
                            continue;
                        }
                        links.Add(link);
                        link = URL + link;
                        GetLinks(link, ref links, URL);
                    }
                }
            }
        }

        private static void GetStatus(string url, StreamWriter goodOutput, StreamWriter badOutput, string link, ref int countGood, ref int countBad)
        {
            try
            {
                if (link != url)
                {
                    link = url + link;
                }
                else
                    link = url;
                HttpWebRequest? request = WebRequest.Create(link) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                goodOutput.Write($"{link} -- ");
                goodOutput.WriteLine(response.StatusCode);
                goodOutput.Flush();
                countGood++;
            }
            catch (WebException ex)
            {
                int statusCode = (int)(ex.Response as HttpWebResponse).StatusCode;
                if (statusCode < 400)
                {
                    goodOutput.Write($"{link} -- ");
                    goodOutput.WriteLine(statusCode);
                    goodOutput.Flush();
                    countGood++;
                }
                else
                {
                    badOutput.Write($"{link} -- ");
                    badOutput.WriteLine(statusCode);
                    badOutput.Flush();
                    countBad++;
                }
            }
        }
    }
}