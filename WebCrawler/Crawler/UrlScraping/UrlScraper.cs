using System.Collections.Generic;
using System.Threading.Tasks;
using WebCrawler.HttpClient;

namespace WebCrawler.Crawler.UrlScraping
{
	public class UrlScraper
	{
		private readonly UrlFilter _urlFilter;
		private readonly UrlExtractor _urlExtractor;
		private readonly IHttpClientWrapper _httpClientWrapper;

		public UrlScraper(UrlFilter urlFilter, UrlExtractor urlExtractor, IHttpClientWrapper httpClientWrapper)
		{
			_urlFilter = urlFilter;
			_urlExtractor = urlExtractor;
			_httpClientWrapper = httpClientWrapper;
		}

		public async Task<List<string>> ScrapeUrls(string domain, string url)
		{
			var fullUrl = $"https://{domain}{url}";
			var webPageContent = await _httpClientWrapper.Get(fullUrl);

			var urls = _urlExtractor.ExtractUrlsFromPage(webPageContent);

			urls = _urlFilter.Filter(domain, urls);

			return urls;
		}
	}
}