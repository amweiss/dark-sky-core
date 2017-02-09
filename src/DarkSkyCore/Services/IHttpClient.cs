namespace DarkSky.Services
{
	using System.Net.Http;
	using System.Threading.Tasks;

	public interface IHttpClient
	{
		Task<HttpResponseMessage> HttpRequest(string requestString);
	}
}