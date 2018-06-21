namespace WebCrawler.Crawler
{
	using System.Text;
	using Strategies;

	public class SiteMapPrinter
	{
		public string Format(PageNode siteMap)
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine(siteMap.Url);
			foreach (var childNode in siteMap.ChildNodes)
			{
				builder.AppendLine("  " + childNode.Url);
				foreach (var childNodeChildNode in childNode.ChildNodes)
				{
					builder.AppendLine("    " + childNodeChildNode.Url);
				}
			}

			return builder.ToString();
		}
	}
}