using System;
using WebCrawler.HttpClient;

namespace WebCrawler
{
	using Crawler;
	using UrlScraping;

	class Program
    {
        static void Main(string[] args)
        {
	        IWebPageLoader webPageLoader = new WebPageLoader();
	        //IUrlScraper urlScraper = new UrlScraper();
			////var crawlingStrategy = new BreadthFirstSearchStrategy(webPageLoader,urlScraper);
			//var crawlerService = new CrawlerService(crawlingStrategy);

	       // crawlerService.Crawl("http://www.monzo.com");
		
	        Console.WriteLine("Press any key to exit");
			Console.ReadKey();
        }
    }
}
