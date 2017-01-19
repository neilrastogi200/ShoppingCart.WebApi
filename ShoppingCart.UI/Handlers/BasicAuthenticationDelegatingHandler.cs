using System;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using Autofac;
using Autofac.Core;
using ShoppingCart.Common.Logging;
using ShoppingCart.UI.Authentication;
using TNF.Portal.WebAPI.Handlers;
using Container = System.ComponentModel.Container;

namespace ShoppingCart.UI.Handlers
{
    public class BasicAuthenticationDelegatingHandler : AuthenticationDelegatingHandler
    {
        private IUserAuthenticator _userAuthenticator;
        private readonly ILogger _logger;

        public BasicAuthenticationDelegatingHandler(HttpServer httpConfiguration, ILogger logger) : base(httpConfiguration, logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Return a UserAuthenticator instance. Using property injection rather construtor injection. 
        /// </summary>
        public IUserAuthenticator UserAuthenticator
        {
            get
            {
                return _userAuthenticator ?? (_userAuthenticator = DependencyConfig.Container.Resolve<IUserAuthenticator>());
            }
            set { _userAuthenticator = value; }
        }

        //// <summary>
        ///     Validate Basic Authentication scheme in the Authorization header
        /// </summary>
        protected override bool ValidateAuthorisationHeader(HttpRequestHeaders headers)
        {
            string authHeader = string.Empty;
            SchemeType = SchemeType.Basic;
            try
            {
                authHeader = headers.Authorization.Parameter;
                string scheme = headers.Authorization.Scheme;
                string decodedHeader = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader));

                if (scheme == SchemeType.Basic.ToString())
                {
                    string userName = decodedHeader.Substring(0, decodedHeader.IndexOf(":", StringComparison.Ordinal));
                    string password = decodedHeader.Substring(decodedHeader.IndexOf(":", StringComparison.Ordinal) + 1);
                    var success = UserAuthenticator.IsValid(userName, password);
                    return success;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.Log(string.Format(InvalidAuthorizationHeaderParameter, authHeader),LogLevel.Warn);
                return false;
            }
        }
    }
}