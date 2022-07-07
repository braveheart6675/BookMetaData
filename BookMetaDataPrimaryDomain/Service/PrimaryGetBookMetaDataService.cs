using BookMetaDataApiDomain.Constant;
using BookMetaDataApiDomain.Interface;

namespace BookMetaDataPrimaryDomain.Service
{
    public class PrimaryGetBookMetaDataService : IPrimaryGetBookMetaDataService
    {
        #region Private Variable
        private static Dictionary<int, string> memoryCache = new Dictionary<int, string>();
        private static object syncLock = new object();
        private ISecondaryGetBookMetaDataService _secondGetBookMetaDataService;
        #endregion

        #region Constructor
        public PrimaryGetBookMetaDataService(ISecondaryGetBookMetaDataService secondGetBookMetaDataService)
        {
            _secondGetBookMetaDataService = secondGetBookMetaDataService;
        }
        #endregion

        #region Public Method
        public string PrimaryGetBookMetaData(int bookId)
        {
            var result = Constant.NOTFOUND;

            if (!IsExistInMemoryCache(bookId))
            {
                result = _secondGetBookMetaDataService.SecondaryGetBookMetaDataService(bookId);
                AddToMemoryCashe(bookId, result);
                //TOTO write in log file
                Console.WriteLine("AddToMemoryCashe");
            }
            else
            {
                result = GetFromMemoryCache(bookId);
                //TOTO write in log file
                Console.WriteLine("GetFromMemoryCache");
            }

            return result;
        }
        #endregion

        #region Private Methods

        private bool IsExistInMemoryCache(int bookId)
        {
            return memoryCache.ContainsKey(bookId);
        }

        private string GetFromMemoryCache(int bookId)
        {
            return memoryCache[bookId];
        }

        private void AddToMemoryCashe(int bookId, string contecnt)
        {
            if (!IsExistInMemoryCache(bookId))
            {
                //To ensure thread safety
                lock (syncLock)
                {
                    if (!IsExistInMemoryCache(bookId))
                    {
                        memoryCache.Add(bookId, contecnt);
                    }
                }
            }
        }
        #endregion
    }
}
