using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Aircon.My7LApi.RESTClient;
using RestSharp;

namespace Aircon.My7LApi.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public abstract class RootApi<TRequest,TResponse> : BaseApi 
    {

        public TRequest Request { get; set; }

        public TResponse Response { get; set; }

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RootApi"/> class.
        /// </summary>
        /// <returns></returns>
        public RootApi(String basePath) : base(basePath)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RootApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public RootApi(Configuration configuration) : base(configuration)
        { }
		#endregion

		#region Public Methods

		/// <summary>
		/// Returns the version of the Acumatica ERP instance and the endpoints available in this instance. 
		/// Available stating from 2019 R2 version of Acumatica ERP.
		/// </summary>
		/// <exception cref="ApiException">Thrown when fails to make API call</exception>
		/// <returns>VersionAndEndpoints</returns>
		public TResponse RootGet(TRequest request)
        {
            ApiResponse<TResponse> localVarResponse = RootGetWithHttpInfo(request);
            return localVarResponse.Data;
        }
        /// <summary>
        /// Returns the version of the Acumatica ERP instance and the endpoints available in this instance. 
        /// Available stating from 2019 R2 version of Acumatica ERP.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of VersionAndEndpoints</returns>
        public async System.Threading.Tasks.Task<TResponse> RootGetAsync(TRequest request)
        {
            ApiResponse<TResponse> localVarResponse = await RootGetAsyncWithHttpInfo(request);
            return localVarResponse.Data;

        }
        #endregion
        public abstract string GetEndpointPath();

        #region Implementation
        /// <summary>
        /// Returns the version of the Acumatica ERP instance and the endpoints available in this instance. 
        /// Available stating from 2019 R2 version of Acumatica ERP.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of VersionAndEndpoints</returns>
        protected ApiResponse<TResponse> RootGetWithHttpInfo(TRequest request)
        {
            var localVarPath = GetEndpointPath(); ;

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, ComposeQueryParams(request), null, ComposeAcceptHeaders(HeaderContentType.Xml | HeaderContentType.Json), ComposeEmptyFormParams(), ComposeEmptyFileParams(),
                ComposeEmptyPathParams(), ComposeContentHeaders(HeaderContentType.None));

            VerifyResponse(localVarResponse, "RootGet");

            return DeserializeResponse<TResponse>(localVarResponse);
        }

        /// <summary>
        /// Returns the version of the Acumatica ERP instance and the endpoints available in this instance. 
        /// Available stating from 2019 R2 version of Acumatica ERP.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of ApiResponse (VersionAndEndpoints)</returns>
        protected async System.Threading.Tasks.Task<ApiResponse<TResponse>> RootGetAsyncWithHttpInfo(TRequest request)
        {
            var localVarPath = "/entity";
           
            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, ComposeQueryParams(request), null, ComposeAcceptHeaders(HeaderContentType.Json | HeaderContentType.Xml), ComposeEmptyFormParams(), ComposeEmptyFileParams(),
                ComposeEmptyPathParams(), ComposeContentHeaders(HeaderContentType.None));
           
            VerifyResponse(localVarResponse, "RootGet");

            return DeserializeResponse<TResponse>(localVarResponse);
        }
        #endregion

        protected List<KeyValuePair<string, string>> ComposeQueryParams(TRequest request)
        {
            var queryParameters = ComposeEmptyQueryParams();
            //if (!String.IsNullOrEmpty(select)) queryParameters.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$select", select)); // query parameter
            //if (!String.IsNullOrEmpty(filter)) queryParameters.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$filter", filter)); // query parameter
            //if (!String.IsNullOrEmpty(expand)) queryParameters.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$expand", expand)); // query parameter
            //if (!String.IsNullOrEmpty(custom)) queryParameters.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$custom", custom)); // query parameter
            //if (skip != null) queryParameters.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$skip", skip)); // query parameter
            //if (top != null) queryParameters.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$top", top)); // query parameter
            Dictionary<string, object> myDict = request.GetType().GetProperties().ToDictionary(prop => prop.Name, prop => prop.GetValue(request, null));
            var qs = GetQueryString(request);
            qs.ForEach(x =>
            {
                queryParameters.Add(x);
            });
            return queryParameters;
        }

public List<KeyValuePair<string, string>> GetQueryString(object obj) {
  var properties = from p in obj.GetType().GetProperties()
                   where p.GetValue(obj, null) != null
                   select new KeyValuePair<string, string>( p.Name, p.GetValue(obj, null).ToString());


            return properties.ToList();
}
    }
}
