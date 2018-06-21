namespace WebCrawler.Crawler.Strategies
{
	using System;
	using System.Collections.Concurrent;
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
			var visited = new HashSet<string>(); //TODO: not concurrent
			
			var queue = new ConcurrentQueue<PageNode>();

			var seedNode = new PageNode("/");
			queue.Enqueue(seedNode);

			while (queue.Count > 0)
			{
				await Process(rootDomain, queue, visited);
			}

			return seedNode;
		}

		private async Task Process(string rootDomain, ConcurrentQueue<PageNode> queue, HashSet<string> visited)
		{
			if (queue.TryDequeue(out var currentNode))
			{
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
		}

		private async Task Som()
		{
			BlockingCollection<PageNode> pages = new BlockingCollection<PageNode>();
			var visited = new HashSet<string>(); //TODO: not concurrent

			await Task.Run(async () =>
			{
				while (!pages.IsCompleted)
				{
					PageNode currentNode = null;
					try
					{
						currentNode = pages.Take();
					}
					catch (InvalidOperationException) { }

					if (currentNode != null)
					{
						var childUrls = await _urlScraper.ScrapeUrls("rootDomain", currentNode.Url);

						foreach (var childUrl in childUrls)
						{
							if (!visited.Contains(childUrl))
							{
								var childNode = new PageNode(childUrl);
								currentNode.AddChild(childNode);
								pages.Add(childNode);
								visited.Add(childUrl);
							}
						}
					}
				}
			});
		}
	}

}