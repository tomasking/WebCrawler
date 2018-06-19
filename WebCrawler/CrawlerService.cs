namespace WebCrawler
{
	public class CrawlerService
	{
		private readonly ICrawlingStrategy _crawlingStrategy;

		public CrawlerService(ICrawlingStrategy crawlingStrategy)
		{
			_crawlingStrategy = crawlingStrategy;
		}

		public string[] Crawl(string seedUrl)
		{
			// check hosts file for excluded urls
			var links =_crawlingStrategy.Search(seedUrl);

			return null;
		}
	}
}