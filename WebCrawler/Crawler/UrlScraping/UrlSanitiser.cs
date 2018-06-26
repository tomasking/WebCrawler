namespace WebCrawler.Crawler.UrlScraping
{
	public class UrlSanitiser
	{
		public string Sanitise(string domain, string inputUrl)
		{
			var url = RemoveDomain(domain, inputUrl);

			url = RemoveTrailingForwardSlash(url);

			return url;
		}

		private static string RemoveDomain(string domain, string inputUrl)
		{
			string url = inputUrl.ToLower();
			url = url.Replace($"http://{domain}/", "/");
			url = url.Replace($"https://{domain}/", "/");
			url = url.Replace($"http://www.{domain}/", "/");
			url = url.Replace($"https://www.{domain}/", "/");
			return url;
		}

		private static string RemoveTrailingForwardSlash(string url)
		{
			if (url.EndsWith("/"))
			{
				url = url.Substring(0, url.Length - 1);
			}

			return url;
		}
	}
}