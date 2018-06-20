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

		public UrlScraper(IUrlFilter[] urlFilters, UrlExtractor urlExtractor)
		{
			_urlFilters = urlFilters;
			_urlExtractor = urlExtractor;
		}

		public List<string> ScrapeUrls(string webPageContent)
		{
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