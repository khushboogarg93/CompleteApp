using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;

namespace AMVACChemical.Interfaces.Services
{
    public class IHttpClientDispose : IDisposable
    {
        #region--Manage Garbage Collector
        /// <summary>
        /// Used this globally Dispose class which are managed and unmanaged garbage collector 
        /// </summary>
        bool disposed = false;
        private List<String> _theList = new List<String>();
        private IDictionary<String, Point> _theDictionary = new Dictionary<String, Point>();
        private HttpClient _client = new HttpClient();

        public void Dispose()
        {        
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if(disposing)
            {                
                _theList.Clear();
                _theDictionary.Clear();
                _client.Dispose();
                _client = null;
                _theList = null;
                _theDictionary = null;
            }             
            disposed = true;
        }
        ~IHttpClientDispose()
        {
            Dispose(false);
        }
        #endregion
    }

}
