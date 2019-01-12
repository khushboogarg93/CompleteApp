using System;

namespace AMVACChemical.Services
{
    public class TrackAboutAPIDateISOFormat
    {
        #region Enums declaration
        enum EnumDays
        {
            Id, second
        };
        #endregion

        #region ISO Date time format generic method
        /// <summary>
        /// Used this method for dynamic getting current and end DateTime in ISO Format
        /// </summary>
        public static void GetISOTimeStamp(out string isoStartDateTime, out string isoEndDateTime)
        {
            try
            {
                isoStartDateTime = DateTime.UtcNow.ToString(ServiceResource.ServiceResource.DateFormat);
                isoEndDateTime = DateTime.UtcNow.AddDays(Convert.ToDouble(EnumDays.second)).ToString(ServiceResource.ServiceResource.DateFormat);
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
