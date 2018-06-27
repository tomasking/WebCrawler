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
			builder.RegisterType<HttpClientWrapper>().As<IHttpClientWrapper>();
			builder.RegisterType<CrawlingOrchestrator>();
			builder.RegisterType<Crawler>();
			builder.RegisterType<UrlScraper>();
			builder.RegisterType<UrlSanitiser>();
			builder.RegisterType<UrlExtractor>();
			builder.RegisterType<UrlFilter>();
			builder.RegisterType<SiteMapPrinter>();
		}
	}
}




