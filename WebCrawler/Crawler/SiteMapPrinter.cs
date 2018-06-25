using System.Linq;

namespace WebCrawler.Crawler
{
	using System.Text;
	using Strategies;

	public class SiteMapPrinter
	{
		public string Format(PageNode rootNode)
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine(rootNode.Url);
			Iterate(rootNode, builder, 1);

			return builder.ToString();
		}

		private static void Iterate(PageNode rootNode, StringBuilder builder, int depth)
		{
			foreach (var childNode in rootNode.ChildNodes)
			{
				builder.AppendLine(" ".PadLeft(depth * 2) + childNode.Url);
				if (childNode.ChildNodes.Any())
				{
					Iterate(childNode, builder, depth+1);
				}
			}
		}
	}
}