namespace WebCrawler.HttpClient
{
	using System;
	using System.Collections.Generic;

	public interface IUrlScraper
	{
		List<string> ScrapeUrls(string webPageContent);
	}
}