namespace WebCrawler.Crawler.UrlScraping
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Extracting;
	using Filters;
	using HttpClient;

	public class UrlScraper
	{
		private readonly IUrlFilter[] _urlFilters;
		private readonly UrlExtractor _urlExtractor;
		private readonly IHttpClientWrapper _httpClientWrapper;

		public UrlScraper(IUrlFilter[] urlFilters, UrlExtractor urlExtractor, IHttpClientWrapper httpClientWrapper)
		{
			_urlFilters = urlFilters;
			_urlExtractor = urlExtractor;
			_httpClientWrapper = httpClientWrapper;
		}

		public async Task<List<string>> ScrapeUrls(string domain, string url)
		{
			var webPageContent = await _httpClientWrapper.Get(domain + url);
			var urls = _urlExtractor.ExtractUrlsFromPage(webPageContent);
			urls = FilterUrls(domain, urls);
			return urls;
		}

		private List<string> FilterUrls(string domain, List<string> urls)
		{
			foreach (var urlFilter in _urlFilters)
			{
				urls = urlFilter.Filter(domain,urls);
			}
			return urls;
		}
	}
}