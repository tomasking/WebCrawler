namespace WebCrawler.Crawler
{
	public interface ICrawlingStrategy
	{
		string[] Search(string seedUrl);
	}
}