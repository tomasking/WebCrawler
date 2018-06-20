namespace WebCrawler.Crawler
{
	using System;
	using System.Collections.Generic;
	using HttpClient;

	public class BreadthFirstSearchStrategy : ICrawlingStrategy
	{
		private readonly IUrlScraper _urlScraper;

		public BreadthFirstSearchStrategy( IUrlScraper urlScraper)
		{
			_urlScraper = urlScraper;
		}

		public string[] Search(string seedUrl)
		{
			var visited = new HashSet<string>();

			var queue = new Queue<LinkedNode>();
			queue.Enqueue(new LinkedNode(seedUrl));

			while (queue.Count > 0)
			{
				var currentUrl = queue.Dequeue();

				if (visited.Contains(currentUrl.Url))
					continue;

				visited.Add(currentUrl.Url);

				var childUrls = _urlScraper.ScrapeUrls(currentUrl.Url);

				foreach (var childUrl in childUrls)
				{
					if (!visited.Contains(childUrl))
						queue.Enqueue(new LinkedNode(childUrl));
				}
			}

			return null;
		}
	}

	public class LinkedNode
	{
		public LinkedNode(string url)
		{
			Url = url;
		}

		public string Url { get; }
	}
}