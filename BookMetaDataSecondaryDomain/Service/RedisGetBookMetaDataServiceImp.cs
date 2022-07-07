using BookMetaDataApiDomain.DomainModel;
using BookMetaDataApiDomain.Interface;
using BookMetaDataSecondaryDomain.Utill;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BookMetaDataSecondaryDomain.Service
{
    public class RedisGetBookMetaDataServiceImp : ISecondaryGetBookMetaDataService
    {
        #region Private Variable
        private static Dictionary<int, byte[]> residCache = new Dictionary<int, byte[]>();
        private static object syncLock = new object();
        #endregion

        #region Public Method
        string ISecondaryGetBookMetaDataService.SecondaryGetBookMetaDataService(int bookId)
        {
            var result = "";

            if (!IsExistInRedisCache(bookId))
            {
                result = getBookMetaData(bookId);
                if (result != BookMetaDataApiDomain.Constant.Constant.NOTFOUND)
                {
                    AddToRedisCashe(bookId, result);
                    //TOTO write in log file
                    Console.WriteLine("AddToRedisCashe");
                }
            }
            else
            {
                result = GetFromRedisCache(bookId);
                //TOTO write in log file
                Console.WriteLine("GetFromRedisCache");
            }

            return result;
        }
        #endregion

        #region Private Methods
        private string getBookMetaData(int bookId)
        {
            string result = "";

            try
            {
                var url = Constant.Constant.URL + bookId;

                HttpWebRequest? request = WebRequest.Create(url) as HttpWebRequest;

                if (request != null)
                {
                    WebResponse response = request.GetResponse();
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                        result = reader.ReadToEnd();
                        if (string.IsNullOrEmpty(result))
                            result = BookMetaDataApiDomain.Constant.Constant.NOTFOUND;

                        //return book as output of API
                        var book = JsonConvert.DeserializeObject<Book>(result);
                    }
                }
            }
            catch (Exception e)
            {
                //TOTO write in log file
                Console.WriteLine(e.Message);
                throw;
            }

            return result;
        }

        private bool IsExistInRedisCache(int bookId)
        {
            return residCache.ContainsKey(bookId);
        }

        private string GetFromRedisCache(int bookId)
        {
            return Compression.Unzip(residCache[bookId]);
        }

        private void AddToRedisCashe(int bookId, string contecnt)
        {
            if (!IsExistInRedisCache(bookId))
            {
                //To ensure thread safety
                lock (syncLock)
                {
                    if (!IsExistInRedisCache(bookId))
                    {
                        residCache.Add(bookId, Compression.Zip(contecnt));
                    }
                }
            }
        }
        #endregion
    }
}
