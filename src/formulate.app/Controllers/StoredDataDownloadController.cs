namespace formulate.app.Controllers
{

    // Namespaces.
    using Managers;
    using Models.Requests;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Hosting;
    using System.Web.Http;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi;
    using Umbraco.Web.WebApi.Filters;
    using ResolverConfig = Resolvers.Configuration;

    /// <summary>
    /// Controller for downloading stored file data.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate")]
    public class StoredDataDownloadController : UmbracoAuthorizedApiController
    {

        #region Properties

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config
        {
            get
            {
                return ResolverConfig.Current.Manager;
            }
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Returns a file download.
        /// </summary>
        /// <param name="request">
        /// The request for the file to download.
        /// </param>
        /// <returns>
        /// The file download.
        /// </returns>
        [HttpGet]
        public HttpResponseMessage DownloadFile([FromUri] GetFileDownloadRequest request)
        {

            // Variables.
            var basePath = HostingEnvironment.MapPath(Config.FileStoreBasePath);
            var fullPath = Path.Combine(basePath, request.PathSegment);
            var fileData = File.ReadAllBytes(fullPath);


            // Construct download result.
            var result = new HttpResponseMessage()
            {
                Content = new ByteArrayContent(fileData),
                StatusCode = HttpStatusCode.OK
            };
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = request.Filename
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");


            // Return download result.
            return result;

        }

        #endregion

    }

}