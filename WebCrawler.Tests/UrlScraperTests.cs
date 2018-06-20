using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler.Tests
{
	using System.IO;
	using HttpClient;
	using Shouldly;
	using Xunit;

	public class UrlScraperTests
    {
		[Fact]
	    public void GetsUrlFromString()
	    {
		    UrlScraper urlScraper = new UrlScraper();

		    var content= File.ReadAllTextAsync("Pages/home.html").Result;
			//string content = "<th><a href=\"Boot_53.html\">135 Boot</a></th>";
		    var urls = urlScraper.ScrapeUrls(content);


			urls.ShouldNotBeNull();
	    }
    }
}
