namespace WebCrawler.Crawler
{
	using HttpClient;

	public class BreadthFirstSearchStrategy : ICrawlingStrategy
	{
		private readonly IWebPageLoader _webPageLoader;
		private readonly IUrlScraper _urlScraper;

		public BreadthFirstSearchStrategy(IWebPageLoader webPageLoader, IUrlScraper urlScraper)
		{
			_webPageLoader = webPageLoader;
			_urlScraper = urlScraper;
		}

		public string[] Search(string seedUrl)
		{
			var webPageContent = _webPageLoader.Load(seedUrl);
			var urls = _urlScraper.ScrapeUrls(webPageContent);

			return urls.ToArray();
		}
	}
}