namespace WebCrawler.UrlScraping.Filters
{
	using System.Collections.Generic;

	public class SameDomainUrlFilter : IUrlFilter
	{
		private readonly string _domain;

		public SameDomainUrlFilter(string domain)
		{
			_domain = domain;
		}

		public List<string> Filter(List<string> urls)
		{
			return urls;
		}
	}
}