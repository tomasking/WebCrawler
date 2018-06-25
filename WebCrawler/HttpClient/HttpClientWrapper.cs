using System;

namespace WebCrawler.HttpClient
{
	using System.Net.Http;
	using System.Threading.Tasks;

	public class HttpClientWrapper : IHttpClientWrapper
	{
	    public async Task<string> Get(string url)
	    {
		    try
		    {
			    var client = new HttpClient();
			    HttpResponseMessage response = await client.GetAsync(url);
			    if (response.IsSuccessStatusCode)
			    {
				    return await response.Content.ReadAsStringAsync();
				}

			    // TODO: replace with logging framework
			    Console.WriteLine($"Error loading {url}, status code {response.StatusCode}");

		    }
		    catch (Exception e)
		    {
				// TODO: replace with logging framework
			    Console.WriteLine($"Error loading {url}, error {e}");
			}

		    return string.Empty;
	    }
	}
}
