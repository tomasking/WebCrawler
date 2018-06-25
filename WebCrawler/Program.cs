using System;
using System.IO;
using System.Text;

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
			    var crawler = scope.Resolve<CrawlingOrchestrator>();
			    var visualSiteMap = await crawler.Crawl("http://www.monzo.com", numberOfThreads: 4);

			    Console.WriteLine(visualSiteMap);
			    await File.WriteAllTextAsync("./monzo_sitemap.txt", visualSiteMap, Encoding.UTF8);
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
