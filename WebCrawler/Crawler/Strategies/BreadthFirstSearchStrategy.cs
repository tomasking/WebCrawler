using System;

namespace WebCrawler.Crawler.Strategies
{
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using UrlScraping;
	
	public class BreadthFirstSearchStrategy : ICrawlingStrategy
	{
		private readonly UrlScraper _urlScraper;
		private ConcurrentDictionary<string, bool> _urlsVisited;
		private BlockingCollection<PageNode> _urlsToProcessQueue;
		private ConcurrentDictionary<int, bool> _doingWork;

		public BreadthFirstSearchStrategy(UrlScraper urlScraper)
		{
			_urlScraper = urlScraper;
		}

		public async Task<PageNode> Crawl(string rootDomain, int numberOfThreads)
		{
			_urlsVisited = new ConcurrentDictionary<string, bool>();
			_urlsToProcessQueue = new BlockingCollection<PageNode>();
			_doingWork = new ConcurrentDictionary<int, bool>();

			var rootNode = new PageNode("/");
			_urlsToProcessQueue.Add(rootNode);

			var tasks = new List<Task>();
			for (int i = 0; i < numberOfThreads; i++)
			{
				var task = SpawnWorker(i, rootDomain);
				tasks.Add(task);
			}

			await Task.WhenAll(tasks);
			
			return rootNode;
		}

		public async Task SpawnWorker(int workerNumber, string rootDomain)
		{
			await Task.Run(async () =>
			{
				foreach (var currentNode in _urlsToProcessQueue.GetConsumingEnumerable())
				{
					if (_urlsToProcessQueue.IsAddingCompleted)
					{
						break;
					}

					_doingWork[workerNumber] = true;

					await ScrapeUrl(rootDomain, currentNode, _urlsToProcessQueue, _urlsVisited);

					_doingWork[workerNumber] = false;

					if (CrawlingCompleted())
					{
						_urlsToProcessQueue.CompleteAdding();
					}
				}
			});
		}

		private bool CrawlingCompleted()
		{
			return _urlsToProcessQueue.Count == 0 && _urlsToProcessQueue.IsCompleted == false &&
			       _doingWork.All(t => t.Value == false);
		}

		private async Task ScrapeUrl(string rootDomain, PageNode currentNode, BlockingCollection<PageNode> urlsToProcessQueue, ConcurrentDictionary<string, bool> urlsVisited)
		{
			urlsVisited.TryAdd(currentNode.Url, true);

			Console.WriteLine("Scraping: " + currentNode.Url);
			var childUrls = await _urlScraper.ScrapeUrls(rootDomain, currentNode.Url);

			foreach (var childUrl in childUrls)
			{
				if (!urlsVisited.ContainsKey(childUrl))
				{
					var childNode = new PageNode(childUrl);
					currentNode.AddChild(childNode);
					urlsToProcessQueue.Add(childNode);
					urlsVisited.TryAdd(childUrl, true);
				}
			}
		}
	}
}