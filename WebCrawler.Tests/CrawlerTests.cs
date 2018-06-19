using System;
using System.IO;
using Shouldly;
using WebCrawler.HttpClient;
using Xunit;

namespace WebCrawler.Tests
{
    public class CrawlerTests
    {
        [Fact]
        public void CanCrawlTheFrontPage()
        {
	        IWebPageLoader webPageLoader = new FakeWebPageLoader();
	        IUrlScraper urlScraper = new UrlScraper();
			ICrawlingStrategy crawlingStrategy = new BreadthFirstSearchStrategy(webPageLoader, urlScraper);
			CrawlerService crawlerService = new CrawlerService(crawlingStrategy);

	        string[] urls = crawlerService.Crawl("www.monzo.com");

			urls.ShouldNotBeNull();
			urls.Length.ShouldBeGreaterThan(0);
        }
    }


	public class FakeWebPageLoader : IWebPageLoader {
		public string Load(string url)
		{
			return File.ReadAllTextAsync("Pages/home.html").Result;
		}
	}
}
