using System;
using System.Collections.Generic;
using System.Linq;

namespace WebCrawler.Crawler.UrlScraping
{
	public class UrlFilter 
	{
		private readonly UrlSanitiser _urlSanitiser;

		public UrlFilter(UrlSanitiser urlSanitiser)
		{
			_urlSanitiser = urlSanitiser;
		}

		readonly List<string> _excludedUrls = new List<string>()
		{
			"/docs", // just hardcoding from the robots.txt 
			"/cdn-cgi",
			"/static"
		};

		public List<string> Filter(string domain, List<string> urls)
		{
			var filteredUrls = new List<string>();
			
			var onlyAllowSameDomain = new Predicate<string>(url => url.StartsWith("/"));
			var removeExcludedUrls = new Predicate<string>(url => !_excludedUrls.Any(url.StartsWith));

			var predicates = new[] {onlyAllowSameDomain, removeExcludedUrls};
			
			foreach (var url in urls)
			{
				var santisedUrl = _urlSanitiser.Sanitise(domain, url);

				if (predicates.All(p=>p(santisedUrl)))
				{
					filteredUrls.Add(santisedUrl);
				}
			}

			return filteredUrls;
		}
	}
}