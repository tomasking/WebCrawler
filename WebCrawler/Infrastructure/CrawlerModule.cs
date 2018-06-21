namespace WebCrawler.Infrastructure
{
	using Autofac;
	using Crawler;
	using Crawler.Strategies;
	using Crawler.UrlScraping;
	using Crawler.UrlScraping.Extracting;
	using Crawler.UrlScraping.Filters;
	using HttpClient;

	public class CrawlerModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<HttpClientWrapper>().As<IHttpClientWrapper>().SingleInstance();
			builder.RegisterType<UrlScraper>().SingleInstance();
			builder.RegisterType<BreadthFirstSearchStrategy>().As<ICrawlingStrategy>().SingleInstance();
			builder.RegisterType<UrlExtractor>().SingleInstance();
			builder.RegisterType<ExternalDomainFilter>().As<IUrlFilter>().SingleInstance();
			builder.RegisterType<RobotsFileExcludedPagesFilter>().As<IUrlFilter>().SingleInstance();
			builder.RegisterType<SiteMapPrinter>().SingleInstance();
			builder.RegisterType<CrawlingOrchestrator>().SingleInstance();
		}
	}
}




