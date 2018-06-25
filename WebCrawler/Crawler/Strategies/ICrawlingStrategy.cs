namespace WebCrawler.Crawler.Strategies
{
	using System.Threading.Tasks;

	public interface ICrawlingStrategy
	{
		Task<PageNode> Crawl(string rootDomain, int numberOfThreads);
	}
}