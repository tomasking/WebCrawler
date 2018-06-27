using Shouldly;
using WebCrawler.Crawler;
using WebCrawler.Crawler.Model;
using Xunit;

namespace WebCrawler.Tests.UnitTests
{
	using System.Text;

	public class SiteMapPrinterTests
    {
		[Fact]
	    public void ShouldBeAbleToPrintVisualSiteMap()
	    {
			var printer = new SiteMapPrinter();

			PageNode root = new PageNode("/");
			PageNode about = new PageNode("/about/");
			root.AddChild(about);
		    PageNode blog = new PageNode("/blog/");
		    root.AddChild(blog);
			PageNode blogOne = new PageNode("/blog/one/");
			blog.AddChild(blogOne);
			PageNode blogOneB = new PageNode("/blog/one/b/");
		    blogOne.AddChild(blogOneB);

			var siteMap = printer.Format(root);

		    var expectedSiteMap = new StringBuilder();
		    expectedSiteMap.AppendLine("/");
		    expectedSiteMap.AppendLine("  /about/");
		    expectedSiteMap.AppendLine("  /blog/");
		    expectedSiteMap.AppendLine("    /blog/one/");
		    expectedSiteMap.AppendLine("      /blog/one/b/");

			siteMap.ShouldBe(expectedSiteMap.ToString());
		}

    }
}
