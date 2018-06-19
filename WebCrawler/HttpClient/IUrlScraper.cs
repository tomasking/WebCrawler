namespace WebCrawler.HttpClient
{
	public interface IUrlScraper
	{
		string[] ScrapeUrls(string webPageContent);
	}

	public class UrlScraper : IUrlScraper
	{
		public string[] ScrapeUrls(string webPageContent)
		{
			return null;
		}
	}
}