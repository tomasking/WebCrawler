using System.Collections.Generic;
using Shouldly;
using WebCrawler.Crawler.UrlScraping;
using Xunit;

namespace WebCrawler.Tests.UnitTests.UrlScraping
{
	public class FiltersAndSanitisesUrlsTests
    {
	    private readonly UrlFilter _urlFilter;

	    public FiltersAndSanitisesUrlsTests()
	    {
		    _urlFilter = new UrlFilter(new UrlSanitiser());
	    }

	    [Fact]
	    public void SanitisesUrls()
	    {
		    List<string> inputUrls = new List<string>()
		    {
			    "/first/",
			    "/SECOND",
			    "http://MoNZO.com/third",
			    "https://MoNZO.com/fourth",
			    "http://WwW.MoNZO.com/fifth",
			    "https://WwW.MoNZO.com/sixth"
		    };

		    var filteredUrls = _urlFilter.Filter("monzo.com", inputUrls);

		    filteredUrls.Count.ShouldBe(6);
		    filteredUrls[0].ShouldBe("/first");
		    filteredUrls[1].ShouldBe("/second");
		    filteredUrls[2].ShouldBe("/third");
		    filteredUrls[3].ShouldBe("/fourth");
		    filteredUrls[4].ShouldBe("/fifth");
		    filteredUrls[5].ShouldBe("/sixth");
	    }

		[Fact]
	    public void ShouldFilterOutExternalDomains()
	    {
			List<string> inputUrls = new List<string>()
			{
				"/first",
				"http://google.com",
				"http://monzo.com/second",
				"https://google.com",
				"https://monzo.com/third",
				"http://www.monzo.com/fourth",
				"https://www.monzo.com/fifth"
			};

		    var filteredUrls = _urlFilter.Filter("monzo.com", inputUrls);

			filteredUrls.Count.ShouldBe(5);
			filteredUrls[0].ShouldBe("/first");
			filteredUrls[1].ShouldBe("/second");
			filteredUrls[2].ShouldBe("/third");
			filteredUrls[3].ShouldBe("/fourth");
			filteredUrls[4].ShouldBe("/fifth");
		}

	    [Fact]
	    public void ShouldFilterOutExcludedUrls()
	    {
		    List<string> inputUrls = new List<string>()
		    {
			    "/first",
			    "/docs/",
			    "/second",
			    "/cdn-cgi/",
			    "/static/"
			};

		    var filteredUrls = _urlFilter.Filter("monzo.com", inputUrls);

		    filteredUrls.Count.ShouldBe(2);
		    filteredUrls[0].ShouldBe("/first");
		    filteredUrls[1].ShouldBe("/second");
	    }
	}
}
