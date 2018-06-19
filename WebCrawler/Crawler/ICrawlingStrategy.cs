namespace WebCrawler
{
	public interface ICrawlingStrategy
	{
		string[] Search(string seedUrl);
	}
}