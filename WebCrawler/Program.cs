using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using WebCrawler.Crawler;
using WebCrawler.Infrastructure;

namespace WebCrawler
{
	class Program
    {
	    static void Main()
	    {
		    try
		    {
			    RunAsync().GetAwaiter().GetResult();
		    }
		    catch (Exception e)
		    {
				Console.WriteLine($"Fatal Error: {e}"); //TODO: proper logging
		    }

		    Console.WriteLine("Press any key to exit"); 
		    Console.ReadKey();
		}

	    static async Task RunAsync()
	    {
		    var builder = BuildIoC();

		    using (var scope = builder.BeginLifetimeScope())
		    {
			    var crawler = scope.Resolve<CrawlingOrchestrator>();

				Stopwatch sw = new Stopwatch();
			    sw.Start();

				var siteMap = await crawler.Crawl("monzo.com", numberOfThreads: 4);

			    sw.Stop();
				Console.WriteLine(siteMap);
			    Console.WriteLine($"Time taken {sw.Elapsed.TotalSeconds} seconds");

				await File.WriteAllTextAsync("./monzo_sitemap.txt", siteMap, Encoding.UTF8);
		    }
		}
		
	    private static IContainer BuildIoC()
	    {
		    var builder = new ContainerBuilder();
			builder.RegisterModule<CrawlerModule>();
			return builder.Build();
	    }
	}
}
