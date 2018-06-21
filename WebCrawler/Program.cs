using System;

namespace WebCrawler
{
	using System.Threading.Tasks;
	using Autofac;
	using Crawler;
	using Infrastructure;

	class Program
    {
	    static void Main()
	    {
		    RunAsync().GetAwaiter().GetResult();
	    }

	    static async Task RunAsync()
	    {
		    var builder = BuildIoC();

		    using (var scope = builder.BeginLifetimeScope())
		    {
			    var crawlerService = scope.Resolve<CrawlingOrchestrator>();
			    var siteMap = await crawlerService.Crawl("http://www.smoething.com");

			    Console.WriteLine(siteMap);
		    }

		    Console.WriteLine("Press any key to exit");
		    Console.ReadKey();
		}
		
	    private static IContainer BuildIoC()
	    {
		    var builder = new ContainerBuilder();
			builder.RegisterModule<CrawlerModule>();
			return builder.Build();
	    }
	}
}
