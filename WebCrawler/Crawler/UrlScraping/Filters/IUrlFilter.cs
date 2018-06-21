namespace WebCrawler.Crawler.UrlScraping.Filters
{
	using System.Collections.Generic;

	public interface IUrlFilter {
		List<string> Filter(string domain, List<string> urls);
	}
}