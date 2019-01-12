using AMVACChemical.ViewModels.TrackAbout;
using AMVACChemical.ViewModels.TrackAbout.Assets;
using AMVACChemical.ViewModels.TrackAbout.RequestParam.AssetTransferLocation;
using AMVACChemical.ViewModels.TrackAbout.RequestParam.Customers;
using AMVACChemical.ViewModels.TrackAbout.RequestParam.Fill;
using AMVACChemical.ViewModels.TrackAbout.RequestParam.TransferAssetToCustomer;
using AMVACChemical.ViewModels.TrackAbout.ResponseParam;
using System.Threading.Tasks;

namespace AMVACChemical.Interfaces.Services
{
    public interface ITrackAboutServices
    {
        #region -- ASSETS
        /// <summary>
        /// Declaration for getting list of assets
        /// </summary>
        /// <returns></returns>
        Task<AssetsVM> GetAssetList();      

        /// <summary>
        /// Declaration for User Login 
        /// </summary>
        /// <returns></returns>
        Task<UspBaseSaveResult> Login(LoginVM model);

        /// <summary>
        /// Declaration for move asset to customer location
        /// </summary>
        /// <param name="deliveriesVM"></param>
        /// <returns></returns>
        Task<DeliveriesResponseVM> MoveAssetToCustomer(DeliveriesVM deliveriesVM);

        /// <summary>
        /// Declaration of MoveAssetLocation Method which is used to create asset as well as move assets from one location to another location except the customer. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AssetLocationResponseVM> MoveAssetLocation(AssetTransferVM model);

        /// <summary>
        /// Declaration fill method which is used to notify TrackAbout that an Asset (SmartCartridge) has been filled with a product (chemical). 
        /// </summary>
        /// <param name="fillsVM"></param>
        /// <returns></returns>
        Task<FillsResponseVM> Fill(FillsVM fillsVM);
        #endregion

        #region--LOCATION
        /// <summary>
        /// Declare for getting all location
        /// </summary>
        /// <returns></returns> 
        Task<ListofallLocation> GetListOfAllLocation();
        #endregion

        #region--CUSTOMERS
        /// <summary>
        /// Declare for getting all visible customers 
        /// </summary>
        /// <returns></returns>
        Task<CustomerAllVisibleList> GetListOfAllVisibleCustomer();
        #endregion
    }
}
