using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using ShoppingCart.Common.Logging;
using TNF.Portal.WebAPI.Handlers;

namespace ShoppingCart.UI.Handlers
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {
        private const string Realm = "realm=\"ShoppingCart.WebAPI\"";
        protected  const string InvalidAuthorizationHeaderParameter = "Invalid Authorization header parameter: {0}";
        private  ILogger _logger;


        public AuthenticationDelegatingHandler(HttpConfiguration httpConfiguration, ILogger logger)
        {
            _logger = logger;
            InnerHandler = new HttpControllerDispatcher(httpConfiguration);
        }

        public AuthenticationDelegatingHandler(HttpServer httpConfiguration, ILogger logger)
        {
            this._httpConfiguration = httpConfiguration;
            _logger = logger;
        }

        protected SchemeType SchemeType { get; set; }
        private HttpServer _httpConfiguration;

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization == null)
            {
                return await UnauthorisedHttpResponse(request);
            }

            var isValidHeader =  ValidateAuthorisationHeader(request.Headers);


            return !isValidHeader
                ? await UnauthorisedHttpResponse(request)
                : await base.SendAsync(request, cancellationToken);
        }


        /// <summary>
        ///     Generate a Unauthorised Http Response Message
        /// </summary>
        private Task<HttpResponseMessage> UnauthorisedHttpResponse(HttpRequestMessage request)
        {
            HttpResponseMessage responseMessage = request.CreateResponse(HttpStatusCode.Unauthorized);
            responseMessage.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(SchemeType.ToString(), Realm));
            return Task<HttpResponseMessage>.Factory.StartNew(() => responseMessage);
        }

        /// <summary>
        ///     Validate the Basic Authentication authorisation header
        /// </summary>
        protected virtual bool ValidateAuthorisationHeader(HttpRequestHeaders headers)
        {
            return false;
        }


    }
}