namespace WebCrawler.Tests.AcceptanceTests.Fakes
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Threading;
	using System.Threading.Tasks;
	using HttpClient;

	public class FakeHttpClient : IHttpClientWrapper
	{
		private const string SeedUrl = "monzo.com";

		readonly Dictionary<string, string> _pages = new Dictionary<string, string>()
		{
			{ SeedUrl+ "/", "home.html"},
			{ SeedUrl+ "/about", "about.html"}
		};

		public async Task<string> Get(string url)
		{
			Thread.Sleep(100);
			if (_pages.TryGetValue(url, out string filename))
			{
				return await File.ReadAllTextAsync($"AcceptanceTests/Fakes/TestData/{filename}");
			}

			return String.Empty;
		}
	}
}