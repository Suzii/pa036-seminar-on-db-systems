using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Service.Data;
using Service.DTO;
using Shared.Filters;
using System.Threading.Tasks;
using Shared.Settings;

namespace RestApi.Controllers.Api
{
    public class StoresController : ApiController
    {
        private readonly IStoreService _storeService;

        public StoresController()
        {
            var dbSettings = new DbSettings() {UseSecondAppContext = false};
            _storeService = new StoreService(dbSettings);
        }

        // GET: api/Stores
        public async Task<IList<StoreDTO>> Get(int page = 1, int perPage = 10, string sortDir = "ASC", string sortField = "Id", string name = null)
        {
            // TODO if filters are applied, total count cannot be used
            var totalCount = await _storeService.TotalCountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / perPage);

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link("DefaultApi", new { controller = "Stores", action="Get", _page = page - 1, _perPage = perPage }) : "";
            var nextLink = page < totalPages - 1 ? urlHelper.Link("DefaultApi", new { controller = "Stores", action = "Get", _page = page + 1, _perPage = perPage }) : "";
            

            var paginationHeader = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PrevPageLink = prevLink,
                NextPageLink = nextLink
            };

            System.Web.HttpContext.Current.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));
            System.Web.HttpContext.Current.Response.Headers.Add("X-Total-Count", totalCount.ToString());

            var modifier = new StoreFilter()
            {
                Skip = (page-1) * perPage,
                Take = perPage,
                OrderDesc = sortDir == "DESC",
                OrderProperty = sortField,
                NameFilter = name
            };

            var result = await _storeService.GetAsync(modifier);
            return result;
        }

        // GET: api/Stores/5
        public async Task<HttpResponseMessage> GetStore(int id)
        {
            try
            {
                var result = await _storeService.GetAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // POST: api/Stores
        public async Task<HttpResponseMessage> Post([FromBody]StoreDTO store)
        {
            if (store == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read store from mssage body.");
            }

            try
            {
                var created = await _storeService.CreateAsync(store);
                return Request.CreateResponse(HttpStatusCode.Created, created);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        // PUT: api/Stores/5
        public async Task<HttpResponseMessage> Put(int id, [FromBody]StoreDTO store)
        {
            if (store == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read store from mssage body.");
            }
            
            try
            {
                store.Id = id;
                var updated = await _storeService.UpdateAsync(store);
                return Request.CreateResponse(HttpStatusCode.OK, updated);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE: api/Stores/5
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var existing = await _storeService.GetAsync(id);
            if (existing == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotModified, $"Store with {id} does not exist");
            }

            try
            {
                await _storeService.DeleteAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
