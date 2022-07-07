using BookMetaDataApiDomain.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMetaData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookMetaDataController : ControllerBase
    {
        #region Private Variable
        IPrimaryGetBookMetaDataService _primaryGetBookMetaDataService;
        #endregion

        #region Constructor
        public BookMetaDataController(IPrimaryGetBookMetaDataService primaryGetBookMetaDataService)
        {
            _primaryGetBookMetaDataService = primaryGetBookMetaDataService;
        }
        #endregion

        #region Public Member
        // GET: api/<BookMetaDataController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BookMetaDataController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return _primaryGetBookMetaDataService.PrimaryGetBookMetaData(id);//"value";
        }

        // POST api/<BookMetaDataController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BookMetaDataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookMetaDataController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion
    }
}
