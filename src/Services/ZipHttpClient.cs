namespace DarkSky.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    ///     An implementation of <see cref="IHttpClient" /> that uses
    ///     <see
    ///         cref="DecompressionMethods.GZip" />
    ///     and <see cref="DecompressionMethods.Deflate" />.
    /// </summary>
    public sealed class ZipHttpClient : IHttpClient
    {
        private readonly HttpClientHandler handler = new HttpClientHandler();
        private readonly HttpClient httpClient;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ZipHttpClient" /> class.
        /// </summary>
        public ZipHttpClient()
        {
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

            httpClient = new HttpClient(handler);
        }

        /// <summary>
        ///     Make a request to the <paramref name="requestString" />.
        /// </summary>
        /// <param name="requestString">
        ///     The actual URL after the root domain to make the request to.
        /// </param>
        /// <returns>The <see cref="HttpRequestMessage" /> from the URL.</returns>
        public async Task<HttpResponseMessage> HttpRequestAsync(string requestString) =>
            await httpClient.GetAsync(new Uri(requestString)).ConfigureAwait(false);

        /// <summary>
        ///     Destructor
        /// </summary>
        ~ZipHttpClient()
        {
            Dispose(false);
        }

        #region IDisposable Support

        private bool disposedValue; // To detect redundant calls

        /// <summary>
        ///     Dispose of resources used by the class.
        /// </summary>
        /// <param name="disposing">If the class is disposing managed resources.</param>
        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    handler.Dispose();
                    httpClient.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        ///     Public access to start disposing of the class instance.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}