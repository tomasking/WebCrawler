using Shouldly;
using WebCrawler.Crawler.UrlScraping;
using Xunit;

namespace WebCrawler.Tests.UnitTests.UrlScraping
{
    public class UrlExtractingTests
    {
	    [Fact]
	    public void ShouldExtractOnlyHrefLinks()
	    {
		    string inputHtml = @"<html><body><a href='first' /><img src=''><a href=""second""</body></html>";
			var urlExtractor = new UrlExtractor();

		    var urls = urlExtractor.ExtractUrlsFromPage(inputHtml);

			urls.Count.ShouldBe(2);
			urls[0].ShouldBe("first");
			urls[1].ShouldBe("second");
	    }
    }
}
