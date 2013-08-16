using System.Collections.Generic;
using System.Web.Http;
using Castle.Core.Logging;

namespace OPIA.API.Proxy.Controllers
{
    public class ValuesController : ApiController
    {

        public ILogger Logger { get; set; }

        // GET api/values
        public IEnumerable<string> Get()
        {
            Logger.Info("'GET' values method invoked");
            return new string[] {"value1", "value2"};
        }

        // GET api/values/5
        public string Get(int id)
        {
            Logger.InfoFormat("'GET' values method invoked with value {0}", id);
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }


        //public HttpResponseMessage PostPerson(Person person)
        //{
        //    //string personString = string.Format("{0} {1} {2:s}", person.FirstName, person.LastName, person.DateOfBirth);
        //    //Logger.InfoFormat("'POST' to PostPerson values method invoked with value object: {0}", personString);
        //    //var response = Request.CreateResponse(HttpStatusCode.Created, person);
        //    //System.Diagnostics.Debug.WriteLine(personString);
        //    //return response;
        //    throw new NotImplementedException();
        //}

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}