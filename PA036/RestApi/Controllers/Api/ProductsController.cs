using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Service.Data;
using Service.DTO;
using Shared.Modifiers;

namespace RestApi.Controllers.Api
{
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;

        public ProductsController()
        {
            _productService = new ProductService();
        }

        // GET: api/Products
        public IList<ProductDTO> Get(int _page = 1, int _perPage = 10, string _sortDir = "ASC", string _sortField = "Id")
        {
            var totalCount = _productService.TotalCount();
            var totalPages = (int)Math.Ceiling((double)totalCount / _perPage);
            

            var urlHelper = new UrlHelper(Request);
            var prevLink = _page > 0 ? urlHelper.Link("DefaultApi", new { controller = "Products", action="Get", _page = _page - 1, _perPage = _perPage }) : "";
            var nextLink = _page < totalPages - 1 ? urlHelper.Link("DefaultApi", new { controller = "Products", action = "Get", _page = _page + 1, _perPage = _perPage }) : "";
            

            var paginationHeader = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PrevPageLink = prevLink,
                NextPageLink = nextLink
            };

            System.Web.HttpContext.Current.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));
            System.Web.HttpContext.Current.Response.Headers.Add("X-Total-Count", totalCount.ToString());

            int stockCount;
            int? unitCost;
            

            var modifier = new ProductModifier()
            {
                Skip = (_page-1) * _perPage,
                Take = _perPage,
                OrderDesc = _sortDir == "DESC",
                OrderProperty = _sortField,
            };

            var result = _productService.Get(modifier);
            return result;
        }

        // GET: api/Products/5
        public HttpResponseMessage GetProduct(int id)
        {
            try
            {
                var result = _productService.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // POST: api/Products
        public HttpResponseMessage Post([FromBody]ProductDTO product)
        {
            if (product == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read product from mssage body.");
            }

            try
            {
                var created = _productService.Create(product);
                return Request.CreateResponse(HttpStatusCode.Created, created);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        // PUT: api/Products/5
        public HttpResponseMessage Put(int id, [FromBody]ProductDTO product)
        {
            if (product == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read product from mssage body.");
            }
            
            try
            {
                product.Id = id;
                var updated = _productService.Update(product);
                return Request.CreateResponse(HttpStatusCode.OK, updated);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE: api/Products/5
        public HttpResponseMessage Delete(int id)
        {
            var existing = _productService.Get(id);
            if (existing == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotModified, $"Product with {id} does not exist");
            }

            try
            {
                _productService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
