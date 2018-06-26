using System.Linq;

namespace WebCrawler.Crawler.UrlScraping.Filters
{
	using System.Collections.Generic;

    public class ExcludedPagesFilter : IUrlFilter
    {
	    readonly List<string> _excludedUrls = new List<string>()
		{
			"/docs/", // just hardcoding from the robots.txt 
			"/cdn-cgi/",
			"/static/"
		};
	    public List<string> Filter(string domain, List<string> urls)
	    {
			var filteredUrls = new List<string>();
		    foreach (var url in urls)
		    {
				if (!_excludedUrls.Any(u => url.StartsWith(u)))
				{ 
				    filteredUrls.Add(url);
			    }
		    }
		    return filteredUrls;
		}
    }
}
