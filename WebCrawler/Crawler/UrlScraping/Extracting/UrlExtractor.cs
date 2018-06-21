namespace WebCrawler.Crawler.UrlScraping.Extracting
{
	using System.Collections.Generic;
	using System.Text.RegularExpressions;

	public class UrlExtractor
    {
	    public List<string> ExtractUrlsFromPage(string webPageContent)
	    {
		    var urls = new List<string>();
		    MatchCollection matchCollection = Regex.Matches(webPageContent, @"<a\s+(?:[^>]*?\s+)?href=([""'])(.*?)\1", RegexOptions.Singleline);
		    foreach (Match m in matchCollection)
		    {
			    string value = m.Groups[2].Value;
			    urls.Add(value);
		    }
		    return urls;
	    }
	}
}
