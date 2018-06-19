using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler.HttpClient
{
    public interface IWebPageLoader
    {
	    string Load(string url);
    }

	public class WebPageLoader : IWebPageLoader
	{
		public string Load(string url)
		{
			return null;
		}
	}
}
