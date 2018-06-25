using System.Linq;

namespace WebCrawler.Crawler.UrlScraping.Filters
{
	using System.Collections.Generic;

	/// <summary>
	/// Just hardcoding this for now to the value that was in the robots.txt file
	/// </summary>
    public class RobotsFileExcludedPagesFilter : IUrlFilter
    {
	    readonly List<string> _excludedUrls = new List<string>()
		{
			"/docs/", // from robots.txt
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
