namespace WebCrawler.Crawler.Strategies
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Threading.Tasks;
	using UrlScraping;

	public class BreadthFirstSearchStrategy : ICrawlingStrategy
	{
		private readonly UrlScraper _urlScraper;
		const int ThreadCount = 4;

		public BreadthFirstSearchStrategy(UrlScraper urlScraper)
		{
			_urlScraper = urlScraper;
		}

		public async Task<PageNode> Crawl(string rootDomain)
		{
			var urlsVisited = new ConcurrentDictionary<string, bool>();
			var urlsToProcessQueue = new BlockingCollection<PageNode>();

			var initialPage = new PageNode("/");
			urlsToProcessQueue.Add(initialPage);
			
			var tasks = new List<Task>();

			List<NodeWorker> nodeWorkers = new List<NodeWorker>();
			for (int i = 0; i < ThreadCount; i++)
			{
				var nodeWorker = new NodeWorker(_urlScraper, rootDomain, urlsToProcessQueue, urlsVisited);
				nodeWorkers.Add(nodeWorker);
				
				var task = nodeWorker.Spawn();
				tasks.Add(task);
			}

			await Task.WhenAll(tasks);

			
			return initialPage;
		}
	}


	public class NodeWorker
	{
		private static int _totalWorkers = 0;
		private readonly ConcurrentDictionary<int, bool> _workersTracker = new ConcurrentDictionary<int, bool>();

		private readonly UrlScraper _urlScraper;
		private readonly string _rootDomain;
		private readonly BlockingCollection<PageNode> _urlsToProcessQueue;
		private readonly ConcurrentDictionary<string, bool> _urlsVisited;
		private readonly int _currentWorker;

		public NodeWorker(UrlScraper urlScraper, string rootDomain, BlockingCollection<PageNode> urlsToProcessQueue, ConcurrentDictionary<string, bool> urlsVisited)
		{
			_workersTracker[_totalWorkers] = false;
			_currentWorker = _totalWorkers;
			_totalWorkers++;

			_urlScraper = urlScraper;
			_rootDomain = rootDomain;
			_urlsToProcessQueue = urlsToProcessQueue;
			_urlsVisited = urlsVisited;
		}


		public async Task Spawn()
		{
			await Task.Run(async () =>
			{
				foreach (var currentNode in _urlsToProcessQueue.GetConsumingEnumerable())
				{
					if (_urlsToProcessQueue.IsCompleted)
					{
						break;
					}

					_workersTracker[_currentWorker] = true;

					await ScrapeUrl(currentNode);

					_workersTracker[_currentWorker] = false;

					if (_urlsToProcessQueue.Count == 0 && _urlsToProcessQueue.IsCompleted == false &&
					    _workersTracker.All(t => t.Value == false))
					{
						_urlsToProcessQueue.CompleteAdding();
					}
				}
			});
		}

		private async Task ScrapeUrl(PageNode currentNode)
		{
			_urlsVisited.TryAdd(currentNode.Url, true);
			var childUrls = await _urlScraper.ScrapeUrls(_rootDomain, currentNode.Url);

			foreach (var childUrl in childUrls)
			{
				if (!_urlsVisited.ContainsKey(childUrl))
				{
					var childNode = new PageNode(childUrl);
					currentNode.AddChild(childNode);
					_urlsToProcessQueue.Add(childNode);
					_urlsVisited.TryAdd(childUrl, true);
				}
			}
		}
	}
}