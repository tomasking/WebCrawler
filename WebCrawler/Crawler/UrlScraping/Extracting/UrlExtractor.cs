namespace WebCrawler.Crawler.UrlScraping.Extracting
{
	using System.Collections.Generic;
	using System.Text.RegularExpressions;

	public class UrlExtractor
    {
	    private const string HrefLinkRegex = @"<a\s+(?:[^>]*?\s+)?href=([""'])(.*?)\1";

	    public List<string> ExtractUrlsFromPage(string webPageContent)
	    {
		    var urls = new List<string>();
		    MatchCollection matchCollection = Regex.Matches(webPageContent, HrefLinkRegex, RegexOptions.Singleline);
		    foreach (Match m in matchCollection)
		    {
			    string value = m.Groups[2].Value;
			    urls.Add(value);
		    }

		    return urls;
	    }
	}
}
