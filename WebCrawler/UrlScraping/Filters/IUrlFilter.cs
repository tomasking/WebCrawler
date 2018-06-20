namespace WebCrawler.UrlScraping.Filters
{
	using System.Collections.Generic;

	public interface IUrlFilter {
		List<string> Filter(List<string> urls);
	}
}