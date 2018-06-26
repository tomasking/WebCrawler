namespace WebCrawler.Tests.AcceptanceTests
{
	using System;
	using System.Diagnostics;
	using System.Threading.Tasks;
	using Autofac;
	using Crawler;
	using Fakes;
	using HttpClient;
	using Infrastructure;
	using Shouldly;
	using Xunit;

	public class CrawlerAcceptanceTest : IDisposable
    {
	    private readonly IContainer _container;
	    private readonly CrawlingOrchestrator _crawlingOrchestrator;
	    private string _seedUrl = "www.somedomain.com";
	    string _expectedSiteMap =
		    @"/
  /about
    /blog/2018/06/21/how-to-get-online-in-an-emergency/
    /blog/2018/06/19/gambling-block-self-exclusion/
    /blog/2018/06/14/what-is-open-banking/
  /blog
  /community
  /faq
  /download
  /-play-store-redirect
  /features/apple-pay
  /features/travel
  /features/switch
  /features/overdrafts
  /press
  /careers
  /transparency
  /university
  /tone-of-voice
  /terms
  /fscs-information
  /privacy
  /cookies
";
		public CrawlerAcceptanceTest()
	    {
		    _container = BuildIoCContainer();
			_crawlingOrchestrator = _container.Resolve<CrawlingOrchestrator>();
	    }

        [Fact]
        public async Task ShouldDisplayTheSiteMapWhenICrawlThisWebsite()
        {
	        
			var siteMap = await _crawlingOrchestrator.Crawl(_seedUrl, 6);

			
			siteMap.ShouldBe(_expectedSiteMap);
        }

	    private IContainer BuildIoCContainer()
	    {
		    var builder = new ContainerBuilder();
		    builder.RegisterModule<CrawlerModule>();

			// Overrides
			builder.RegisterType<FakeHttpClient>().As<IHttpClientWrapper>().SingleInstance();

			return builder.Build();
	    }

	    public void Dispose()
	    {
		    _container.Dispose();
	    }
	}
}
