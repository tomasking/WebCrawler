namespace WebCrawler.Crawler.UrlScraping.Filters
{
	using System.Collections.Generic;

	/// <summary>
	/// Just hardcoding this for now to the value that was in the robots.txt file
	/// </summary>
    public class RobotsFileExcludedPagesFilter : IUrlFilter
    {
	    public List<string> Filter(string domain, List<string> urls)
	    {
			var filteredUrls = new List<string>();
		    foreach (var url in urls)
		    {
			    if (!url.ToLower().StartsWith("/docs/"))
			    {
				    filteredUrls.Add(url);
			    }
		    }
		    return filteredUrls;
		}
    }
}
