using Aircon.My7LApi.Model;
using Aircon.My7LApi.RESTClient;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.My7LApi.Auth
{
    public partial class AuthApi : BaseApi
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthApi"/> class.
        /// </summary>
        /// <returns></returns>
        public AuthApi(String basePath) : base(basePath)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public AuthApi(Configuration configuration) : base(configuration)
        { }
        #endregion

        #region Public Methods
        public Configuration LogIn(string username, string password)
        {
            var cookieContainer = new CookieContainer();
            this.Configuration.ApiClient.RestClient.CookieContainer = cookieContainer;

            var auth = this.AuthLogin(new Credentials(username, password));
            //  Console.WriteLine("Logged In...");
            var configuration = new Configuration(this.Configuration.BasePath);
            configuration.AccessToken = auth.AccessToken;

            //share cookie container between API clients because we use different client for authentication and interaction with endpoint
            configuration.ApiClient.RestClient.CookieContainer = this.Configuration.ApiClient.RestClient.CookieContainer;
            configuration.ApiClient.RestClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(auth.AccessToken, "Bearer");
            return configuration;
        }
        /// <summary>
        /// Logs in to the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public AuthModel AuthLogin(Credentials credentials)
        {
            return AuthLoginWithHttpInfo(credentials);
        }
        /// <summary>
        /// Logs in to the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="credentials"></param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task AuthLoginAsync(Credentials credentials)
        {
            await AuthLoginAsyncWithHttpInfo(credentials);

        }

        /// <summary>
        /// Logs out from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <returns></returns>
        public void AuthLogout()
        {
            AuthLogoutWithHttpInfo();
        }
        /// <summary>
        /// Logs out from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task AuthLogoutAsync()
        {
            await AuthLogoutAsyncWithHttpInfo();

        }
        #endregion

        #region Implementation
        /// <summary>
        /// Logs in to the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="credentials"></param>
        /// <returns>ApiResponse of Object(void)</returns>
        protected AuthModel AuthLoginWithHttpInfo(Credentials credentials)
        {
            // verify the required parameter 'credentials' is set
            if (credentials == null)
                ThrowMissingParameter("AuthLogin", nameof(credentials));

            var localVarPath = "api/v1/login";

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, ComposeEmptyQueryParams(), ComposeBody(credentials), ComposeAcceptHeaders(HeaderContentType.None), ComposeEmptyFormParams(), ComposeEmptyFileParams(),
                ComposeEmptyPathParams(), ComposeContentHeaders(HeaderContentType.Json | HeaderContentType.Xml | HeaderContentType.WwwForm));

            VerifyResponse(localVarResponse, "AuthLogin");
            return DeserializeResponse<AuthDataModel>(localVarResponse).Data.AuthModel;
            //return GetAuthResponseHeaders(localVarResponse);
        }

        /// <summary>
        /// Logs in to the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="credentials"></param>
        /// <returns>Task of ApiResponse</returns>
        protected async System.Threading.Tasks.Task<ApiResponse<Object>> AuthLoginAsyncWithHttpInfo(Credentials credentials)
        {
            // verify the required parameter 'credentials' is set
            if (credentials == null)
                ThrowMissingParameter("AuthLogin", nameof(credentials));

            var localVarPath = "/entity/auth/login";

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.POST, ComposeEmptyQueryParams(), ComposeBody(credentials), ComposeAcceptHeaders(HeaderContentType.None), ComposeEmptyFormParams(), ComposeEmptyFileParams(),
                ComposeEmptyPathParams(), ComposeContentHeaders(HeaderContentType.Json | HeaderContentType.Xml | HeaderContentType.WwwForm));

            VerifyResponse(localVarResponse, "AuthLogin");

            return GetResponseHeaders(localVarResponse);
        }

        /// <summary>
        /// Logs out from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of Object(void)</returns>
        protected ApiResponse<Object> AuthLogoutWithHttpInfo()
        {
            var localVarPath = "/entity/auth/logout";

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, ComposeEmptyQueryParams(), null, ComposeAcceptHeaders(HeaderContentType.None), ComposeEmptyFormParams(), ComposeEmptyFileParams(),
                ComposeEmptyPathParams(), ComposeContentHeaders(HeaderContentType.None));

            VerifyResponse(localVarResponse, "AuthLogout");

            return GetResponseHeaders(localVarResponse);
        }

        /// <summary>
        /// Logs out from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of ApiResponse</returns>
        protected async System.Threading.Tasks.Task<ApiResponse<Object>> AuthLogoutAsyncWithHttpInfo()
        {
            var localVarPath = "/entity/auth/logout";

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.POST, ComposeEmptyQueryParams(), null, ComposeAcceptHeaders(HeaderContentType.None), ComposeEmptyFormParams(), ComposeEmptyFileParams(),
                ComposeEmptyPathParams(), ComposeContentHeaders(HeaderContentType.None));

            VerifyResponse(localVarResponse, "AuthLogout");

            return GetResponseHeaders(localVarResponse);
        }

        //protected AuthModel GetAuthResponseHeaders(IRestResponse response)
        //{
        //    int localVarStatusCode = (int)response.StatusCode;

        //    return new ApiResponse<AuthModel>(localVarStatusCode,
        //        response.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
        //        response.Content).Data;
        //}

        #endregion
    }

}
