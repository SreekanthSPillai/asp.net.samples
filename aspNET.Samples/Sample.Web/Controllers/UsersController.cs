using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using Sample.Data;
using Microsoft.Data.OData;

namespace Sample.Web.Controllers
{
    
    public class UsersController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        // GET: odata/Users
        public async Task<IHttpActionResult> GetUsers(ODataQueryOptions<User> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            // return Ok<IEnumerable<User>>(users);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // GET: odata/Users(5)
        public async Task<IHttpActionResult> GetUser([FromODataUri] int key, ODataQueryOptions<User> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            // return Ok<User>(user);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PUT: odata/Users(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<User> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(user);

            // TODO: Save the patched entity.

            // return Updated(user);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/Users
        public async Task<IHttpActionResult> Post(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(user);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/Users(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<User> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(user);

            // TODO: Save the patched entity.

            // return Updated(user);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/Users(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
