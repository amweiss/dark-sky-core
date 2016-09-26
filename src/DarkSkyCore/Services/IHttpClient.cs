using System.Net.Http;
using System.Threading.Tasks;

namespace DarkSky.Services
{
	public interface IHttpClient
	{
		Task<HttpResponseMessage> HttpRequest(string requestString);
	}
}