using AMVACChemical.Interfaces.Services;
using AMVACChemical.ViewModels.TrackAbout;
using AMVACChemical.ViewModels.TrackAbout.Assets;
using AMVACChemical.ViewModels.TrackAbout.RequestParam;
using AMVACChemical.ViewModels.TrackAbout.RequestParam.AssetTransferLocation;
using AMVACChemical.ViewModels.TrackAbout.RequestParam.Customers;
using AMVACChemical.ViewModels.TrackAbout.RequestParam.Fill;
using AMVACChemical.ViewModels.TrackAbout.RequestParam.TransferAssetToCustomer;
using AMVACChemical.ViewModels.TrackAbout.ResponseParam;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AMVACChemical.Services.TrackAbout
{
    #region Enums declaration
    enum EnumProjectTest { Id, Name };
    #endregion

    public class TrackAboutService : ITrackAboutServices
    {
        // Declare global configurations for inject
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _config;
        private readonly AMVAC_TrackaboutContext _trackAboutContext;
        private readonly string _username;
        private readonly string _password;
        private readonly string _apiKey;
        private HttpClient client = new HttpClient();


        // create constructor
        public TrackAboutService(IHostingEnvironment env, IConfigurationRoot config, AMVAC_TrackaboutContext trackAboutContext)
        {
            _env = env;
            _config = config;
            _trackAboutContext = trackAboutContext;
            _username = _config["TrackAboutApi:Username"];
            _password = _config["TrackAboutApi:Password"];
            _apiKey = _config["TrackAboutApi:ApiKey"];
        }

        #region-- ASSETS
        /// <summary>
        /// HttpHandlerMethod Method used to pass and create http client request for trackabout api's 
        /// </summary>
        public void HttpHandlerMethod()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                Credentials = new
                    System.Net.NetworkCredential(_username, _password)
            };
            try
            {
                string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_username + ":" + _password));
                client.DefaultRequestHeaders.Add(ServiceResource.ServiceResource.apikey, _apiKey);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(ServiceResource.ServiceResource.applicationjson));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(ServiceResource.ServiceResource.Basic, credentials);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// GetAssetList Method used to get list of assets from trackAbout Api
        /// </summary>
        /// <returns></returns>
        public async Task<AssetsVM> GetAssetList()
        {
            // Calling function for getting http-client response
            HttpHandlerMethod();
            try
            {
                var apiResponse = await client.GetAsync(ServiceResource.ServiceResource.assetsAPI).ConfigureAwait(false);
                var contentResult = await apiResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var assetResult = JsonConvert.DeserializeObject<AssetsVM>(contentResult);
                return assetResult;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// This method or used for login the Authenicated User
        /// </summary>
        /// <returns></returns>
        public async Task<UspBaseSaveResult> Login(LoginVM model)
        {
            // Used for calling http handler 3rd api calls
            HttpHandlerMethod();
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { model.Username, model.Username }
                };
                var loginParameters = new FormUrlEncodedContent(parameters);
                var apiResponse = await client.PostAsync(ServiceResource.ServiceResource.assetsAPI, loginParameters).ConfigureAwait(false);
                var contentResult = await apiResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var loginResult = JsonConvert.DeserializeObject<UspBaseSaveResult>(contentResult);
                return loginResult;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// MoveAssetToCUstomer() method is used to move asset to customer location
        /// </summary>
        /// <param name="deliveriesVM"></param>
        /// <returns></returns>
        public async Task<DeliveriesResponseVM> MoveAssetToCustomer(DeliveriesVM deliveriesVM)
        {
            DeliveriesResponseVM deliveriesResponseVM = new DeliveriesResponseVM();
            HttpHandlerMethod();
            try
            {
                string isoStartDateTime = string.Empty;
                string isoEndDateTime = string.Empty;
                TrackAboutAPIDateISOFormat.GetISOTimeStamp(out isoStartDateTime, out isoEndDateTime);
                if (deliveriesVM.assets.tagged.Count > 0)
                {
                    foreach (var item in deliveriesVM.assets.tagged)
                    {
                        item.timeEntered = isoStartDateTime;
                    }
                }
                deliveriesVM.userEntryStartDate = isoStartDateTime;
                deliveriesVM.userEntryEndDate = isoEndDateTime;

                string stringData = JsonConvert.SerializeObject(deliveriesVM);
                var contentData = new StringContent(stringData, Encoding.UTF8, ServiceResource.ServiceResource.applicationjson);
                var apiResponse = await client.PostAsync(ServiceResource.ServiceResource.assetToCustomerLocation, contentData).ConfigureAwait(false);
                var contentResult = await apiResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var assetTransfer = JsonConvert.DeserializeObject<DeliveriesResponseVM>(contentResult);
                return assetTransfer;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// MoveAssetLocation Method used to create asset as well as move assets from one location to another location except the customer. 
        /// </summary>
        /// <param name="assetTransferVM"></param>
        /// <returns></returns>
        public async Task<AssetLocationResponseVM> MoveAssetLocation(AssetTransferVM assetTransferVM)
        {
            HttpHandlerMethod();
            try
            {
                // This utility class method is used to get current and end datetime
                string isoStartDateTime = string.Empty;
                string isoEndDateTime = string.Empty;
                TrackAboutAPIDateISOFormat.GetISOTimeStamp(out isoStartDateTime, out isoEndDateTime);
                assetTransferVM.UserEntryStartDate = isoStartDateTime;
                assetTransferVM.UserEntryEndDate = isoEndDateTime;
                foreach (var item in assetTransferVM.assets.tagged)
                {
                    item.timeEntered = isoStartDateTime;
                }
                var stringDate = JsonConvert.SerializeObject(assetTransferVM);
                var contentData = new StringContent(stringDate, Encoding.UTF8, ServiceResource.ServiceResource.applicationjson);
                var apiResponse = await client.PostAsync(ServiceResource.ServiceResource.assetsLocationAPI + '/' + assetTransferVM.locationMId + '/' + ServiceResource.ServiceResource.assetApiChar, contentData).ConfigureAwait(false);
                var contentResult = await apiResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var assetTransfer = JsonConvert.DeserializeObject<AssetLocationResponseVM>(contentResult);
                return assetTransfer;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Fill method is used to notify TrackAbout that an Asset (SmartCartridge) has been filled with a product (chemical). 
        /// </summary>
        /// <param name="fillsVM"></param>
        /// <returns></returns>
        public async Task<FillsResponseVM> Fill(FillsVM fillsVM)
        {
            FillsResponseVM fillsResponseVM = new FillsResponseVM();
            HttpHandlerMethod();
            try
            {
                string isoStartDateTime = string.Empty;
                string isoEndDateTime = string.Empty;
                TrackAboutAPIDateISOFormat.GetISOTimeStamp(out isoStartDateTime, out isoEndDateTime);
                if (fillsVM.assets.tagged.Count > 0)
                {
                    foreach (var item in fillsVM.assets.tagged)
                    {
                        item.timeEntered = isoStartDateTime;
                    }
                }
                fillsVM.userEntryStartDate = isoStartDateTime;
                fillsVM.userEntryEndDate = isoEndDateTime;

                string stringData = JsonConvert.SerializeObject(fillsVM);
                var contentData = new StringContent(stringData, Encoding.UTF8, ServiceResource.ServiceResource.applicationjson);
                var apiResponse = await client.PostAsync(ServiceResource.ServiceResource.fillsApi, contentData).ConfigureAwait(false);
                var contentResult = await apiResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var fillResponse = JsonConvert.DeserializeObject<FillsResponseVM>(contentResult);
                return fillResponse;
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region--LOCATION
        /// <summary>
        /// GetListOfAllLocation Method used to Get a list of  all Location  from trackAbout Api
        /// </summary>
        /// <returns></returns>
        public async Task<ListofallLocation> GetListOfAllLocation()
        {
            //Caling funcation for getting http-client response
            HttpHandlerMethod();
            try
            {
                var apiResponse = await client.GetAsync(ServiceResource.ServiceResource.listofAllLocationAPI).ConfigureAwait(false);
                var contentResult = await apiResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var locationResultList = JsonConvert.DeserializeObject<ListofallLocation>(contentResult);
                return locationResultList;
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region--CUSTOMERS
        /// <summary>
        /// GetCustomerAllVisibleList Method is used to Get a list of all visible customers from trackAbout Api
        /// </summary>
        /// <returns></returns>
        public async Task<CustomerAllVisibleList> GetListOfAllVisibleCustomer()
        {
            //Caling funcation for getting http-client response
            HttpHandlerMethod();
            try
            {
                var apiResponse = await client.GetAsync(ServiceResource.ServiceResource.customersVisibleListAPI).ConfigureAwait(false);
                var contentResult = await apiResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var customerVisibleListResult = JsonConvert.DeserializeObject<CustomerAllVisibleList>(contentResult);
                return customerVisibleListResult;
            }
            catch
            {
                throw;
            }
            finally
            {
                var disposable = new IHttpClientDispose();
                if (disposable != null)
                    disposable.Dispose();
            }
        }
        #endregion
    }
}
