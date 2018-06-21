namespace WebCrawler.Crawler.Strategies
{
	using System.Collections.Generic;

	public class PageNode
	{
		public List<PageNode> ChildNodes { get; }

		public PageNode(string url)
		{
			Url = url;
			ChildNodes = new List<PageNode>();
		}

		public string Url { get; }

		public void AddChild(PageNode childNode)
		{
			ChildNodes.Add(childNode);
		}
	}
}