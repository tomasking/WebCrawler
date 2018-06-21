namespace WebCrawler.Crawler
{
	using System.Threading.Tasks;
	using Strategies;

	public class CrawlingOrchestrator
	{
		private readonly ICrawlingStrategy _crawlingStrategy;
		private readonly SiteMapPrinter _siteMapPrinter;

		public CrawlingOrchestrator(ICrawlingStrategy crawlingStrategy, SiteMapPrinter siteMapPrinter)
		{
			_crawlingStrategy = crawlingStrategy;
			_siteMapPrinter = siteMapPrinter;
		}

		public async Task<string> Crawl(string seedUrl)
		{
			var siteMap = await _crawlingStrategy.Crawl(seedUrl);

			var visualSiteMap = _siteMapPrinter.Format(siteMap);

			return visualSiteMap;
		}
	}
}