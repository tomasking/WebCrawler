namespace WebCrawler.Crawler.Strategies
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using UrlScraping;

	public class BreadthFirstSearchStrategy : ICrawlingStrategy
	{
		private readonly UrlScraper _urlScraper;

		public BreadthFirstSearchStrategy(UrlScraper urlScraper)
		{
			_urlScraper = urlScraper;
		}

		public async Task<PageNode> Crawl(string rootDomain)
		{
			var visited = new HashSet<string>();

			var queue = new Queue<PageNode>();

			var seedNode = new PageNode("/");
			queue.Enqueue(seedNode);

			while (queue.Count > 0)
			{
				var currentNode = queue.Dequeue();
				visited.Add(currentNode.Url);

				var childUrls = await _urlScraper.ScrapeUrls(rootDomain, currentNode.Url);

				foreach (var childUrl in childUrls)
				{
					if (!visited.Contains(childUrl))
					{
						var childNode = new PageNode(childUrl);
						currentNode.AddChild(childNode);
						queue.Enqueue(childNode);
						visited.Add(childUrl);
					}
				}				
			}

			return seedNode;
		}
	}
}