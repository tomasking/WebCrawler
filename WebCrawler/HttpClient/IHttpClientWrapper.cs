namespace WebCrawler.HttpClient
{
	using System.Threading.Tasks;

	public interface IHttpClientWrapper
	{
		Task<string> Get(string url);
	}
}