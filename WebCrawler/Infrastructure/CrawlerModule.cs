namespace WebCrawler.Infrastructure
{
	using Autofac;
	using Crawler;
	using Crawler.UrlScraping;
	using HttpClient;

	public class CrawlerModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<HttpClientWrapper>().As<IHttpClientWrapper>().SingleInstance();
			builder.RegisterType<UrlScraper>().SingleInstance();
			builder.RegisterType<Crawler>().SingleInstance();
			builder.RegisterType<UrlExtractor>().SingleInstance();
			builder.RegisterType<UrlFilter>().SingleInstance();
			builder.RegisterType<SiteMapPrinter>().SingleInstance();
			builder.RegisterType<CrawlingOrchestrator>().SingleInstance();
		}
	}
}




