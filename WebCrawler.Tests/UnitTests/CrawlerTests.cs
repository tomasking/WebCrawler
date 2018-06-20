namespace WebCrawler.Tests.UnitTests
{
	using System.IO;
	using Crawler;
	using HttpClient;
	using Shouldly;
	using UrlScraping;
	using WebCrawler.UrlScraping;
	using WebCrawler.UrlScraping.Filters;
	using Xunit;

	public class CrawlerTests
    {
        [Fact]
        public void CanCrawlTheFrontPage()
        {
	        IWebPageLoader webPageLoader = new FakeWebPageLoader();
			IUrlFilter[] filters = new IUrlFilter[]{ new SameDomainUrlFilter("www.something.com") };
	        IUrlScraper urlScraper = new UrlScraper(filters, new UrlExtractor(), webPageLoader);
			ICrawlingStrategy crawlingStrategy = new BreadthFirstSearchStrategy(urlScraper);
			CrawlerService crawlerService = new CrawlerService(crawlingStrategy);

	        string[] urls = crawlerService.Crawl("www.something.com");

			urls.ShouldNotBeNull();
			urls.Length.ShouldBeGreaterThan(0);
        }
    }


	public class FakeWebPageLoader : IWebPageLoader {
		public string Load(string url)
		{
			if (url == "www.something.com") { 
				return File.ReadAllTextAsync("TestData/home.html").Result;
			}

			return " ";
		}
	}
}
