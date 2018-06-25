using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using WebCrawler.Crawler;
using WebCrawler.Crawler.Strategies;
using Xunit;

namespace WebCrawler.Tests.UnitTests
{
    public class SiteMapPrinterTests
    {

		[Fact]
	    public void ShouldBeAbleToPrintVisualSiteMap()
	    {
			SiteMapPrinter printer = new SiteMapPrinter();

			PageNode root = new PageNode("/");
			PageNode about = new PageNode("/about/");
			root.AddChild(about);
		    PageNode blog = new PageNode("/blog/");
		    root.AddChild(blog);

		    PageNode blogOne = new PageNode("/blog/one/");
			blog.AddChild(blogOne);

		    PageNode blogOneB = new PageNode("/blog/one/b/");
		    blogOne.AddChild(blogOneB);

			var visualSiteMap = printer.Format(root);

		    string _expectedSiteMap = @"/
  /about/   
  /blog/
    /blog/one/
      /blog/one/b/";

			visualSiteMap.ShouldBe(_expectedSiteMap);
		}

    }
}
