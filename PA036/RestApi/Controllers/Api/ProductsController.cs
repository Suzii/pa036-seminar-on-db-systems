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
using Shared.Enums;

namespace RestApi.Controllers.Api
{
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;

        public ProductsController()
        {
            var dbSettings = new DbSettings() { AppContext = AppContexts.Local };
            _productService = new ProductService(dbSettings);
        }

        // GET: api/Products
        public async Task<IList<ProductDTO>> Get(int page = 1, int perPage = 10, string sortDir = "ASC", string sortField = "Id", string name = null)
        {
            // TODO if filters are applied, total count cannot be used
            var totalCount = await _productService.TotalCountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / perPage);

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link("DefaultApi", new { controller = "Products", action="Get", _page = page - 1, _perPage = perPage }) : "";
            var nextLink = page < totalPages - 1 ? urlHelper.Link("DefaultApi", new { controller = "Products", action = "Get", _page = page + 1, _perPage = perPage }) : "";
            

            var paginationHeader = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PrevPageLink = prevLink,
                NextPageLink = nextLink
            };

            System.Web.HttpContext.Current.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));
            System.Web.HttpContext.Current.Response.Headers.Add("X-Total-Count", totalCount.ToString());

            var modifier = new ProductFilter()
            {
                Skip = (page-1) * perPage,
                Take = perPage,
                OrderDesc = sortDir == "DESC",
                OrderProperty = sortField,
                NameFilter = name
            };

            var result = await _productService.GetAsync(modifier);
            return result;
        }

        // GET: api/Products/5
        public async Task<HttpResponseMessage> GetProduct(int id)
        {
            try
            {
                var result = await _productService.GetAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // POST: api/Products
        public async Task<HttpResponseMessage> Post([FromBody]ProductDTO product)
        {
            if (product == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read product from mssage body.");
            }

            try
            {
                var created = await _productService.CreateAsync(product);
                return Request.CreateResponse(HttpStatusCode.Created, created);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        // PUT: api/Products/5
        public async Task<HttpResponseMessage> Put(int id, [FromBody]ProductDTO product)
        {
            if (product == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read product from mssage body.");
            }
            
            try
            {
                product.Id = id;
                var updated = await _productService.UpdateAsync(product);
                return Request.CreateResponse(HttpStatusCode.OK, updated);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE: api/Products/5
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var existing = await _productService.GetAsync(id);
            if (existing == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotModified, $"Product with {id} does not exist");
            }

            try
            {
                await _productService.DeleteAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
