namespace WebCrawler.UrlScraping
{
	using System.Collections.Generic;
	using System.Text.RegularExpressions;
	using Filters;
	using HttpClient;

	public class UrlScraper : IUrlScraper
	{
		private readonly IUrlFilter[] _urlFilters;
		private readonly UrlExtractor _urlExtractor;
		private readonly IWebPageLoader _webPageLoader;

		public UrlScraper(IUrlFilter[] urlFilters, UrlExtractor urlExtractor, IWebPageLoader webPageLoader)
		{
			_urlFilters = urlFilters;
			_urlExtractor = urlExtractor;
			_webPageLoader = webPageLoader;
		}

		public List<string> ScrapeUrls(string url)
		{
			var webPageContent = _webPageLoader.Load(url);
			var urls = _urlExtractor.ExtractUrlsFromPage(webPageContent);
			urls = FilterUrls(urls);
			return urls;
		}

		private List<string> FilterUrls(List<string> urls)
		{
			foreach (var urlFilter in _urlFilters)
			{
				urls = urlFilter.Filter(urls);
			}
			return urls;
		}
	}
}