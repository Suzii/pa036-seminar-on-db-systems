using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
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
        public IEnumerable<ProductDTO> Get(int page = 0, int pageSize = 10)
        {
            var totalCount = _productService.TotalCount();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link("DefaultApi", new { controller = "Products", action="Get", page = page - 1, pageSize = pageSize }) : "";
            var nextLink = page < totalPages - 1 ? urlHelper.Link("DefaultApi", new { controller = "Products", action = "Get", page = page + 1, pageSize = pageSize }) : "";
            

            var paginationHeader = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PrevPageLink = prevLink,
                NextPageLink = nextLink
            };

            System.Web.HttpContext.Current.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

            var modifier = new ProductModifier()
            {
                Skip = page,
                Take = pageSize
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

            var existing = _productService.Get(id);
            if (existing == null || existing.id != product.id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotModified, $"Product with {id} does not exist");
            }
            
            try
            {
                product.id = id;
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
