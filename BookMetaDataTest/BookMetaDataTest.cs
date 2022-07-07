using BookMetaData.Controllers;
using BookMetaDataApiDomain.Constant;
using BookMetaDataPrimaryDomain.Service;
using BookMetaDataSecondaryDomain.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using System.Web.Http.Results;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BookMetaDataTest
{
    [TestClass]
    public class Tests
    {
        #region UnitTests
        [TestMethod]
        public void GetMetaData_ShouldNotReturnData()
        {
            var mockRedisGetBookMeta = new RedisGetBookMetaDataServiceImp();
            var mockPrimaryGetBookMeta = new PrimaryGetBookMetaDataService(mockRedisGetBookMeta);

            var result = mockPrimaryGetBookMeta.PrimaryGetBookMetaData(0);
            Assert.Equals(result, Constant.NOTFOUND);
        }

        [TestMethod]
        public void GetMetaData_ShouldReturnCorrectData()
        {
            var mockRedisGetBookMeta = new RedisGetBookMetaDataServiceImp();
            var mockPrimaryGetBookMeta = new PrimaryGetBookMetaDataService(mockRedisGetBookMeta);
            var mockController = new BookMetaDataController(mockPrimaryGetBookMeta);

            var content = mockController.Get(7728);
            Assert.IsNotNull(content);
        }

        [TestMethod]
        public void GetMetaData_ShouldNotFindProduct()
        {
            var mockRedisGetBookMeta = new RedisGetBookMetaDataServiceImp();
            var mockPrimaryGetBookMeta = new PrimaryGetBookMetaDataService(mockRedisGetBookMeta);
            var mockController = new BookMetaDataController(mockPrimaryGetBookMeta);

            var content = mockController.Get(-1);
            Assert.IsInstanceOfType(content, typeof(NotFoundResult));
        }
        #endregion
    }
}