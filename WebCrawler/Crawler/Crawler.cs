using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Crawler.Model;
using WebCrawler.Crawler.UrlScraping;

namespace WebCrawler.Crawler
{
	public class Crawler 
	{
		private readonly UrlScraper _urlScraper;
		private ConcurrentDictionary<string, bool> _urlsVisited;
		private BlockingCollection<PageNode> _urlsToProcessQueue;
		private ConcurrentDictionary<int, bool> _doingWork;

		public Crawler(UrlScraper urlScraper)
		{
			_urlScraper = urlScraper;
		}

		public async Task<PageNode> Start(string rootDomain, int numberOfThreads)
		{
			_urlsVisited = new ConcurrentDictionary<string, bool>();
			_urlsToProcessQueue = new BlockingCollection<PageNode>();
			_doingWork = new ConcurrentDictionary<int, bool>();

			var rootNode = new PageNode("/");
			_urlsToProcessQueue.Add(rootNode);
			_urlsVisited.TryAdd(rootNode.Url, true);

			var tasks = new List<Task>();
			for (int i = 0; i < numberOfThreads; i++)
			{
				var task = SpawnWorker(i, rootDomain);
				tasks.Add(task);
			}

			await Task.WhenAll(tasks);
			
			return rootNode;
		}

		public async Task SpawnWorker(int threadNumber, string rootDomain)
		{
			await Task.Run(async () =>
			{
				foreach (var currentNode in _urlsToProcessQueue.GetConsumingEnumerable())
				{
					if (_urlsToProcessQueue.IsAddingCompleted)
					{
						break;
					}

					_doingWork[threadNumber] = true;

					await ScrapeUrl(rootDomain, currentNode, _urlsToProcessQueue, _urlsVisited);

					_doingWork[threadNumber] = false;

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
			Console.WriteLine("Scraping: " + currentNode.Url);
			var childUrls = await _urlScraper.ScrapeUrls(rootDomain, currentNode.Url);

			foreach (var childUrl in childUrls)
			{
				if (!urlsVisited.ContainsKey(childUrl))
				{
					var childNode = new PageNode(childUrl);
					currentNode.AddChild(childNode);
					if (urlsVisited.TryAdd(childUrl, true))
					{
						urlsToProcessQueue.Add(childNode);
					}
				}
			}
		}
	}
}