namespace WebCrawler.Crawler.UrlScraping.Filters
{
	using System.Collections.Generic;

	public class ExternalDomainFilter : IUrlFilter
	{
		public List<string> Filter(string domain, List<string> urls)
		{
			var filteredUrls = new List<string>();
			foreach (var url in urls)
			{
				if (url.StartsWith("/") || url.ToLower().StartsWith($"http://{domain}") ||
				    url.ToLower().StartsWith($"https://{domain}"))
				{
					filteredUrls.Add(url);
				}
			}
			return filteredUrls;
		}
	}
}