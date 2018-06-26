using System.Threading.Tasks;
using WebCrawler.Crawler.Model;

namespace WebCrawler.Crawler
{
	public class CrawlingOrchestrator
	{
		private readonly Crawler _crawler;
		private readonly SiteMapPrinter _siteMapPrinter;

		public CrawlingOrchestrator(Crawler crawler, SiteMapPrinter siteMapPrinter)
		{
			_crawler = crawler;
			_siteMapPrinter = siteMapPrinter;
		}

		public async Task<string> Crawl(string seedUrl, int numberOfThreads = 1)
		{
			PageNode rootNode = await _crawler.Start(seedUrl, numberOfThreads);
			
			string siteMap = _siteMapPrinter.Format(rootNode);

			return siteMap;
		}
	}
}