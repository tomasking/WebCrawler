namespace WebCrawler.HttpClient
{
	using System.Collections.Generic;
	using System.Text.RegularExpressions;

	public class UrlScraper : IUrlScraper
	{
		public List<string> ScrapeUrls(string webPageContent)
		{
			var urls = new List<string>();
			
			MatchCollection m1 = Regex.Matches(webPageContent, @"<a\s+(?:[^>]*?\s+)?href=([""'])(.*?)\1", RegexOptions.Singleline);
			foreach (Match m in m1)
			{
				string value = m.Groups[2].Value;
				urls.Add(value);
				
			}

			return urls;
		}
	}
}